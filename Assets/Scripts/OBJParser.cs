using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Parser de archivos OBJ.
/// - Lee vertices (v), normales (vn) y caras (f)
/// - Soporta caras de 3 Y 4 vertices (triangulos y quads)
/// - Centra el modelo en el origen del mundo despues de cargarlo
/// </summary>
public static class OBJParser
{
    /// <summary>
    /// Carga un archivo OBJ desde la carpeta StreamingAssets y devuelve una Mesh de Unity.
    /// Uso: Mesh mesh = OBJParser.Load("muebles/silla.obj");
    /// </summary>
    public static Mesh Load(string relativePath)
    {
        string fullPath = Path.Combine(Application.streamingAssetsPath, relativePath);

        if (!File.Exists(fullPath))
        {
            Debug.LogError("[OBJParser] Archivo no encontrado: " + fullPath);
            return null;
        }

        string[] lines = File.ReadAllLines(fullPath);
        return ParseLines(lines);
    }

    /// <summary>
    /// Carga un OBJ desde un TextAsset (arrastrado al inspector o en Resources).
    /// Uso: Mesh mesh = OBJParser.LoadFromText(myTextAsset);
    /// </summary>
    public static Mesh LoadFromText(TextAsset textAsset)
    {
        if (textAsset == null)
        {
            Debug.LogError("[OBJParser] TextAsset nulo.");
            return null;
        }
        string[] lines = textAsset.text.Split('\n');
        return ParseLines(lines);
    }

    // ------------------------------------------------------------------ //
    // Parseo interno
    // ------------------------------------------------------------------ //
    private static Mesh ParseLines(string[] lines)
    {
        // Listas temporales del archivo OBJ
        List<Vector3> objVertices = new List<Vector3>();
        List<Vector3> objNormals  = new List<Vector3>();

        // Listas finales para Unity (un entry por vertice unico v/vn)
        List<Vector3> meshVertices = new List<Vector3>();
        List<Vector3> meshNormals  = new List<Vector3>();
        List<int>     meshTriangles = new List<int>();

        // Cache para no duplicar vertices identicos
        Dictionary<string, int> vertexCache = new Dictionary<string, int>();

        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();
            if (string.IsNullOrEmpty(line) || line.StartsWith("#")) continue;

            string[] parts = line.Split(new char[]{' ', '\t'},
                                        System.StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) continue;

            switch (parts[0])
            {
                case "v":   // vertice geometrico
                    objVertices.Add(ParseVector3(parts));
                    break;

                case "vn":  // normal
                    objNormals.Add(ParseVector3(parts));
                    break;

                case "f":   // cara (3 o 4 vertices)
                    ParseFace(parts, objVertices, objNormals,
                              meshVertices, meshNormals,
                              meshTriangles, vertexCache);
                    break;
            }
        }

        if (meshVertices.Count == 0)
        {
            Debug.LogWarning("[OBJParser] El archivo no contiene vertices.");
            return null;
        }

        // Centramos el modelo en el origen
        CenterMesh(meshVertices);

        Mesh mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32; // soporta >65k vertices
        mesh.vertices  = meshVertices.ToArray();
        mesh.triangles = meshTriangles.ToArray();

        if (meshNormals.Count == meshVertices.Count)
            mesh.normals = meshNormals.ToArray();
        else
            mesh.RecalculateNormals();

        mesh.RecalculateBounds();
        return mesh;
    }

    // ── Parseo de una cara ─────────────────────────────────────────────
    // Formatos soportados por vertice en una cara:
    //   v          → solo posicion
    //   v/vt       → posicion + UV (UV se ignora)
    //   v//vn      → posicion + normal
    //   v/vt/vn    → posicion + UV + normal
    // Los indices en OBJ son 1-based.
    private static void ParseFace(string[] parts,
                                   List<Vector3> objVerts,
                                   List<Vector3> objNormals,
                                   List<Vector3> meshVerts,
                                   List<Vector3> meshNormals,
                                   List<int>     meshTris,
                                   Dictionary<string, int> cache)
    {
        // Obtenemos los indices de cada vertice de la cara (partes 1..n)
        int faceVertCount = parts.Length - 1;
        int[] faceIndices = new int[faceVertCount];

        for (int i = 0; i < faceVertCount; i++)
        {
            string token = parts[i + 1];
            string cacheKey = token;

            if (cache.TryGetValue(cacheKey, out int cachedIndex))
            {
                faceIndices[i] = cachedIndex;
                continue;
            }

            // Parseamos el token "v/vt/vn" o variantes
            string[] sub = token.Split('/');
            int vi  = int.Parse(sub[0]) - 1;                                   // vertice
            int vni = (sub.Length == 3 && sub[2] != "") ?
                       int.Parse(sub[2]) - 1 : -1;                             // normal

            Vector3 pos    = (vi  >= 0 && vi  < objVerts.Count)   ? objVerts[vi]   : Vector3.zero;
            Vector3 normal = (vni >= 0 && vni < objNormals.Count)  ? objNormals[vni]: Vector3.up;

            int newIndex = meshVerts.Count;
            meshVerts.Add(pos);
            meshNormals.Add(normal);
            cache[cacheKey] = newIndex;
            faceIndices[i]  = newIndex;
        }

        // Triangulamos la cara por fan (funciona para convexos de 3 o 4 lados)
        // Quad (4 verts) → 2 triangulos: (0,1,2) y (0,2,3)
        for (int i = 1; i < faceVertCount - 1; i++)
        {
            meshTris.Add(faceIndices[0]);
            meshTris.Add(faceIndices[i]);
            meshTris.Add(faceIndices[i + 1]);
        }
    }

    // ── Centra la malla en el origen ───────────────────────────────────
    // Calcula el centro del bounding box y resta ese offset a todos los vertices.
    private static void CenterMesh(List<Vector3> verts)
    {
        Vector3 min = verts[0];
        Vector3 max = verts[0];

        foreach (Vector3 v in verts)
        {
            min = Vector3.Min(min, v);
            max = Vector3.Max(max, v);
        }

        Vector3 center = (min + max) * 0.5f;

        for (int i = 0; i < verts.Count; i++)
            verts[i] -= center;
    }

    // ── Helpers ────────────────────────────────────────────────────────
    private static Vector3 ParseVector3(string[] parts)
    {
        float x = float.Parse(parts[1], System.Globalization.CultureInfo.InvariantCulture);
        float y = float.Parse(parts[2], System.Globalization.CultureInfo.InvariantCulture);
        float z = float.Parse(parts[3], System.Globalization.CultureInfo.InvariantCulture);
        return new Vector3(x, y, z);
    }
}