
using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class SceneManagerIV : MonoBehaviour
{
    public Shader shader;
    private GameObject cameraObject;
    private CO orbital;


    void Start()
        {float ancho = 8f;
                float profundidad = 6f;
                float alto = 3f;
        // CREAR MUEBLES
        /* CrearObjeto(string nombreOBJ,
            Vector3 posicion,
            Vector3 rotacionGrados,
            Vector3 escala,
            Color color)*
            
        CrearObjeto("bed1",    new Vector3(2, -1f, 1.5f),    new Vector3(0, 90, 0), Vector3.one,new Color(0.7f, 0.7f, 0.7f));
       
       Bounds piso = CrearObjeto("floor", new Vector3(0, -1.5f, 0), Vector3.zero, Vector3.one, new Color(0.96f, 0.87f, 0.70f));

       // Después de crear el piso: techo
CrearObjeto("floor", new Vector3(0, 1.5f, 0), Vector3.zero, Vector3.one, new Color(0.9f, 0.9f, 0.9f));
      
        Color paredColor = new Color(0.98f, 0.92f, 0.84f);
        CrearObjeto("wall_back1",
    new Vector3(0, 0, -profundidad / 2f),
    Vector3.zero,
    Vector3.one,
    paredColor);
        
        CrearObjeto("wall_front1",
        new Vector3(0, 0, profundidad / 2f),
        Vector3.zero,
        Vector3.one,
        paredColor);
        
    
        
        
        CrearObjeto("wall_left1",
        new Vector3(-ancho / 2f, 0, 0),
        Vector3.zero,
        Vector3.one,
        paredColor);
        
        
          CrearObjeto("wall_right1",
        new Vector3(ancho / 2f, 0, 0),
        new Vector3(0, 180, 0),
        Vector3.one,
        paredColor);
        
        

    CrearObjeto("wallB1", new Vector3(-0.8f, 0f, -1.04f), Vector3.zero, Vector3.one,paredColor);
CrearObjeto("wallB2", new Vector3(-2.8f, 0f, 1.99f), Vector3.zero, Vector3.one,paredColor);

        //CrearObjeto("floor", new Vector3(0, 3, 0), Vector3.zero, Vector3.one); // reusamos floor como techo
//      Bounds piso = CrearObjeto("floor", Vector3.zero, Vector3.zero, Vector3.one);
        CrearCamara();
    }

    void CrearCamara()
{
    GameObject camObj = new GameObject("MainCamera");
    Camera cam = camObj.AddComponent<Camera>();

    cam.backgroundColor = new Color(0.53f, 0.81f, 0.98f);
    cam.clearFlags = CameraClearFlags.SolidColor;

    // ✅ GUARDAR referencias correctamente
    cameraObject = camObj;
    orbital = camObj.AddComponent<CO>();

    // ✅ Configurar
    orbital.objetivo = new Vector3(0f, 1.5f, 3f);

    camObj.transform.position = new Vector3(0f, 8f, -10f);
}




//carga el objeto
  Bounds CrearObjeto(string nombreOBJ, Vector3 posicion, Vector3 rotacionGrados, Vector3 escala, Color color)
{
    //carga el obj
    OBJParser1 parser = new OBJParser1();
    Mesh mesh = parser.LoadOBJ(nombreOBJ);

    //asginar color a vertices --> crea array de colores del tamaño del mesh ---> todos los vertices del mismo color
Color[] colors = new Color[mesh.vertexCount];
for (int i = 0; i < colors.Length; i++)
{
    colors[i] = color;
}
mesh.colors = colors;

    if (mesh == null) return new Bounds();

    // Convertir grados a radianes
    Vector3 rotacionRad = rotacionGrados * Mathf.Deg2Rad;

    // Crear la matriz modelo
    Matrix4x4 modelMatrix = MatrixVP.CreateModelMatrix(posicion, rotacionRad, escala);

    // NO se aplica a los vértices, se manda al shader
    mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 10000f);

    GameObject obj = new GameObject(nombreOBJ);
    obj.AddComponent<MeshFilter>().mesh = mesh;

    Material mat = new Material(shader);
    mat.SetColor("_MaterialColor", color);

    //MATRIZ DE MODELO 
    mat.SetMatrix("_ModelMatrix", modelMatrix); // <-- la matriz va al shader

    obj.AddComponent<MeshRenderer>().material = mat;

    objetosInstanciados.Add(obj); // necesitás esta lista también

    return mesh.bounds; 

    //BOUNDS PARA SABER SI DIBUJAR EL OBJETO --> SINO ME DESAPARECEN LOS OBJ
}

private List<GameObject> objetosInstanciados = new List<GameObject>();

void Update()
{
    /*Matrix4x4 view = MVP.CreateViewMatrix(
        cameraObject.transform.position,
        orbital.objetivo,
        Vector3.up
    );*/
    Matrix4x4 view = orbital.viewMatrix;

    foreach (GameObject obj in objetosInstanciados)
    {
        obj.GetComponent<MeshRenderer>().material.SetMatrix("_ViewMatrix", view);
    }
}
}