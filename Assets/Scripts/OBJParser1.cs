using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class OBJParser1 : MonoBehaviour


{
    private Vector3[] vertices;
    private int[] triangles;

    // FUNCIÓN PRINCIPAL: carga el OBJ y devuelve Mesh
    public Mesh LoadOBJ(string fileName)
    {
        string path = "Assets/Modelos3d " + fileName + ".obj";

        if (!File.Exists(path))
        {
            Debug.LogError("No se encontró el archivo: " + path);
            return null;
        }

        string fileData = File.ReadAllText(path);

        ParseOBJ(fileData);
        CenterVertices();

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }

       private void ParseOBJ(string fileData)
    { //lista oara los vertices --> un arreglo 
        List<Vector3> tempVertices = new List<Vector3>();
        List<int> tempTriangles = new List<int>();

        string[] lines = fileData.Split('\n'); //separo el arcvhi en lineas

        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();

            // VERTICES
            if (line.StartsWith("v "))  //aca detecto vertices
            {
                string[] parts = line.Split(' '); //separo los valores parts[0]="v" parts[1]= 1.0 ... 

                float x = float.Parse(parts[1], System.Globalization.CultureInfo.InvariantCulture);
                float y = float.Parse(parts[2], System.Globalization.CultureInfo.InvariantCulture);
                float z = float.Parse(parts[3], System.Globalization.CultureInfo.InvariantCulture);

                tempVertices.Add(new Vector3(x, y, z)); //guardo vertices
            }

            // CARAS que son las f 1 2 3
            else if (line.StartsWith("f "))
            {
                string[] parts = line.Split(' ');

                int i0 = ParseFaceIndex(parts[1]); 
                int i1 = ParseFaceIndex(parts[2]);
                int i2 = ParseFaceIndex(parts[3]);

                tempTriangles.Add(i0); //creo los triangulos 
                tempTriangles.Add(i1);
                tempTriangles.Add(i2);

                // Si tiene 4 vértices --> crear segundo triángulo
                if (parts.Length == 5)
                {
                    int i3 = ParseFaceIndex(parts[4]);

                    tempTriangles.Add(i0);
                    tempTriangles.Add(i2);
                    tempTriangles.Add(i3);
                }
            }
        }

        vertices = tempVertices.ToArray();
        triangles = tempTriangles.ToArray();

       // Debug.Log("Vertices: " + vertices.Length);
       // Debug.Log("Triángulos: " + triangles.Length / 3);
    }

  
    // EXTRAER INDICE DE CARA
    
    private int ParseFaceIndex(string token)
    {
        string indexStr = token.Split('/')[0]; // solo el primer nro
        return int.Parse(indexStr) - 1; //-1 pq obj empieza en 1 y unity en 0
    }

   
    private void CenterVertices()
    {
        if (vertices == null || vertices.Length == 0) return;

        Vector3 min = vertices[0];
        Vector3 max = vertices[0];

        foreach (Vector3 v in vertices)
        {
            min = Vector3.Min(min, v);
            max = Vector3.Max(max, v);
        }

        Vector3 center = (min + max) / 2f;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] -= center;
        }
    }
}