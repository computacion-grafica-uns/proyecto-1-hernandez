
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
        CrearObjeto("bed1",    new Vector3(2, -1f, 1.5f),    new Vector3(0, 90, 0), Vector3.one,new Color(0.7f, 0.7f, 0.7f));
        //CrearObjeto("table",   new Vector3(-2, 0, 1),   Vector3.zero,          Vector3.one);
        //CrearObjeto("chair1",  new Vector3(-1, 0, 1),   new Vector3(0, 45, 0), Vector3.one);
        //CrearObjeto("placard", new Vector3(-1, 0, 1),   new Vector3(0, 45, 0), Vector3.one);

        // CREAR ESCENA - piso
        //CrearObjeto("floor", Vector3.zero, Vector3.zero, Vector3.one);
      //Bounds piso = CrearObjeto("floor", Vector3.zero, Vector3.zero, Vector3.one,new Color(0.96f, 0.87f, 0.70f));
      //ESTE SINO Bounds piso = CrearObjeto("floor", Vector3.zero, Vector3.zero, Vector3.one,new Color(0.96f, 0.87f, 0.70f));
       Bounds piso = CrearObjeto("floor", new Vector3(0, -1.5f, 0), Vector3.zero, Vector3.one, new Color(0.96f, 0.87f, 0.70f));

       
        // PAREDES
        // Pared trasera: ya está en XY centrada en X, la movemos a Z = -3
        //CrearObjeto("wall_back1",  new Vector3(0, 0, -3), Vector3.zero,         Vector3.one);
        
        Color paredColor = new Color(0.98f, 0.92f, 0.84f);
         //CrearObjeto("wall_back1", new Vector3(0, 0, -piso.extents.z), Vector3.zero, Vector3.one,paredColor);
        CrearObjeto("wall_back1",
    new Vector3(0, 0, -profundidad / 2f),
    Vector3.zero,
    Vector3.one,
    paredColor);
        
        // Pared frontal: la movemos a Z = +3
      
        //CrearObjeto("wall_front1", new Vector3(0, 0, 3),  Vector3.zero,         Vector3.one);
       // CrearObjeto("wall_front1", new Vector3(0, 0, piso.extents.z), Vector3.zero, Vector3.one,paredColor);
        CrearObjeto("wall_front1",
        new Vector3(0, 0, profundidad / 2f),
        Vector3.zero,
        Vector3.one,
        paredColor);
        
        // Pared izquierda: está en el plano ZY con X=0, la movemos a X = -4
        // y desplazamos Z para que quede alineada con Z = -3
        
        //CrearObjeto("wall_left1",  new Vector3(-4, 0, -3), Vector3.zero,        Vector3.one);
        //CrearObjeto("wall_left1", new Vector3(-piso.extents.x, 0, 0), Vector3.zero, Vector3.one,paredColor);
        CrearObjeto("wall_left1",
        new Vector3(-ancho / 2f, 0, 0),
        Vector3.zero,
        Vector3.one,
        paredColor);
        
        // Pared derecha: igual pero en X = +4, rotada 180° en Y para que la normal mire al interior
        //CrearObjeto("wall_right1", new Vector3(4, 0, -3),  new Vector3(0, 180, 0), Vector3.one);
       // CrearObjeto("wall_right1", new Vector3(piso.extents.x, 0, 0), new Vector3(0, 180, 0), Vector3.one,paredColor);
          CrearObjeto("wall_right1",
        new Vector3(ancho / 2f, 0, 0),
        new Vector3(0, 180, 0),
        Vector3.one,
        paredColor);
        
        // TECHO
       
       /* CrearObjeto("wallB1",
    new Vector3(0f, 0f, -1.04f),
    Vector3.zero,
    Vector3.one);

    CrearObjeto("wallB2",
    new Vector3(-1.66f, 0, 1.99f),
    Vector3.zero,
    Vector3.one);*/

    CrearObjeto("wallB1", new Vector3(-0.8f, 0f, -1.04f), Vector3.zero, Vector3.one,paredColor);
CrearObjeto("wallB2", new Vector3(-2.8f, 0f, 1.99f), Vector3.zero, Vector3.one,paredColor);

        //CrearObjeto("floor", new Vector3(0, 3, 0), Vector3.zero, Vector3.one); // reusamos floor como techo
//      Bounds piso = CrearObjeto("floor", Vector3.zero, Vector3.zero, Vector3.one);
        CrearCamara();
    }

    /*void CreateCamera()
    {
        cameraObject = new GameObject("MainCamera");
        Camera cam = cameraObject.AddComponent<Camera>();
        cam.backgroundColor = Color.white;
        cameraObject.transform.position = new Vector3(0f, 6f, -10f);
        cameraObject.transform.LookAt(Vector3.zero);
    }*/
/*       void CrearCamara()
{
    // 1. Crear el objeto de la cámara
    GameObject camObj = new GameObject("MainCamera");
    Camera cam = camObj.AddComponent<Camera>();
    
    // 2. Configuración visual básica
    cam.backgroundColor = new Color(0.53f, 0.81f, 0.98f); // cielo
    cam.clearFlags = CameraClearFlags.SolidColor;

    // 3. Añadir el script de Cámara Orbital
    camaraOrbital orbital = camObj.AddComponent<camaraOrbital>();

    // 4. Configurar los parámetros del script
    // Definimos el punto al que queremos que mire (el centro del monoambiente)
    Vector3 centroObjetivo = new Vector3(0f, 1.5f, 3f);
    orbital.objetivo = centroObjetivo;

    // 5. Posición inicial
    // Colocamos la cámara físicamente en su lugar inicial
    camObj.transform.position = new Vector3(0f, 8f, -10f);
    
    // Importante: El método Start() de OrbitalCamera usará esta posición 
    // para calcular automáticamente el yaw, pitch y la distancia inicial.
}*/
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





  Bounds CrearObjeto(string nombreOBJ, Vector3 posicion, Vector3 rotacionGrados, Vector3 escala, Color color)
{
    OBJParser1 parser = new OBJParser1();
    Mesh mesh = parser.LoadOBJ(nombreOBJ);
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
    mat.SetMatrix("_ModelMatrix", modelMatrix); // <-- la matriz va al shader

    obj.AddComponent<MeshRenderer>().material = mat;

    objetosInstanciados.Add(obj); // necesitás esta lista también

    return mesh.bounds;
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