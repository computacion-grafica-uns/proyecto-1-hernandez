using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneManager2 : MonoBehaviour
{
    public Shader shader;
    private GameObject cameraObject;

    void Start()
    {
        // CREAR MUEBLES
        CrearObjeto("bed1",    new Vector3(2, 0, 3),    new Vector3(0, 90, 0), Vector3.one);
        CrearObjeto("table",   new Vector3(-2, 0, 1),   Vector3.zero,          Vector3.one);
        CrearObjeto("chair1",  new Vector3(-1, 0, 1),   new Vector3(0, 45, 0), Vector3.one);
        CrearObjeto("placard", new Vector3(-1, 0, 1),   new Vector3(0, 45, 0), Vector3.one);

        // CREAR ESCENA - piso
        CrearObjeto("floor", Vector3.zero, Vector3.zero, Vector3.one);

        // PAREDES
        // Pared trasera: ya está en XY centrada en X, la movemos a Z = -3
        CrearObjeto("wall_back",  new Vector3(0, 0, -3), Vector3.zero,         Vector3.one);

        // Pared frontal: la movemos a Z = +3
        CrearObjeto("wall_front", new Vector3(0, 0, 3),  Vector3.zero,         Vector3.one);

        // Pared izquierda: está en el plano ZY con X=0, la movemos a X = -4
        // y desplazamos Z para que quede alineada con Z = -3
        CrearObjeto("wall_left",  new Vector3(-4, 0, -3), Vector3.zero,        Vector3.one);

        // Pared derecha: igual pero en X = +4, rotada 180° en Y para que la normal mire al interior
        CrearObjeto("wall_right", new Vector3(4, 0, -3),  new Vector3(0, 180, 0), Vector3.one);

        // TECHO
        CrearObjeto("floor", new Vector3(0, 3, 0), Vector3.zero, Vector3.one); // reusamos floor como techo

        CreateCamera();
    }

    void CreateCamera()
    {
        cameraObject = new GameObject("MainCamera");
        Camera cam = cameraObject.AddComponent<Camera>();
        cam.backgroundColor = Color.white;
        cameraObject.transform.position = new Vector3(0f, 6f, -10f);
        cameraObject.transform.LookAt(Vector3.zero);
    }

    void CrearObjeto(string nombreOBJ, Vector3 posicion, Vector3 rotacion, Vector3 escala)
    {
        OBJParser1 parser = new OBJParser1();
        Mesh mesh = parser.LoadOBJ(nombreOBJ);
        if (mesh == null) return;

        // ── Aplicar Model Matrix a los vértices ──────────────────────────
        Matrix4x4 modelMatrix = ModelMatrix.Create(posicion, rotacion, escala);

        Vector3[] verts = mesh.vertices;
        for (int i = 0; i < verts.Length; i++)
        {
            // Multiplicamos cada vértice por la matriz modelo
            verts[i] = modelMatrix.MultiplyPoint3x4(verts[i]);
        }
        mesh.vertices = verts;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        // ─────────────────────────────────────────────────────────────────

        GameObject obj = new GameObject(nombreOBJ);
        MeshFilter mf   = obj.AddComponent<MeshFilter>();
        MeshRenderer mr = obj.AddComponent<MeshRenderer>();

        mf.mesh = mesh;
        mr.material = new Material(shader);

        // El transform de Unity queda en el origen — la posición ya está
        // "quemada" en los vértices por la Model Matrix
        obj.transform.position    = Vector3.zero;
        obj.transform.eulerAngles = Vector3.zero;
    }
}