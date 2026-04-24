using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Scenegit : MonoBehaviour
{
    public Shader shader;

    private GameObject cameraObject;
    private CO orbital;
    private CamaraPrimeraPersona fpCam;

    private bool usarFP = true;
    private bool mostrarTecho = true;
    private bool mostrarParedes = true;
    bool usarNeblina = true;

    private GameObject techo;
    private List<GameObject> paredes = new List<GameObject>();
    private List<GameObject> objetosInstanciados = new List<GameObject>();

    void Start()
    {
        fpCam = new CamaraPrimeraPersona(new Vector3(0f, 1.5f, 0f));

        // RoomBuilder se encarga de crear todo
        CrearHabitacion roomBuilder = gameObject.AddComponent<CrearHabitacion>();
        roomBuilder.shader = shader;

        CrearHabitacion.RoomData data = roomBuilder.Construir(); //porque??
        paredes = data.paredes;
        techo = data.techo;
        objetosInstanciados = data.todosLosObjetos;

        CrearCamara();
    }

    void CrearCamara()
    {
        cameraObject = new GameObject("MainCamera");
        Camera cam = cameraObject.AddComponent<Camera>();
        cam.backgroundColor = new Color(0.53f, 0.81f, 0.98f);
        cam.clearFlags = CameraClearFlags.SolidColor;

        orbital = cameraObject.AddComponent<CO>();
        orbital.objetivo = Vector3.zero;
        cameraObject.transform.position = new Vector3(0f, 10f, -15f);
    }

    void ManejarInputNeblina()
{
    if (Input.GetKeyDown(KeyCode.N))
    {
        usarNeblina = !usarNeblina;
    }
}

    void Update()
    {
        ManejarInputCamara();
        ManejarInputVisibilidad();
        ManejarInputNeblina();
        ActualizarShaders();
    }

    void ManejarInputCamara()
    {
        if (Input.GetKeyDown(KeyCode.C))
            usarFP = !usarFP;
    }

    Matrix4x4 ObtenerMatrizVista()
    {
        if (usarFP)
        {
            float deltaPhi    = Input.GetAxis("Mouse X") * 0.01f;
            float deltaTheta  = -Input.GetAxis("Mouse Y") * 0.01f;
            float inputAvance  = Input.GetAxis("Vertical") * 0.1f;
            float inputLateral = Input.GetAxis("Horizontal") * 0.1f;

            return fpCam.CalcularMatrizVista(deltaPhi, deltaTheta, inputAvance, inputLateral);
        }
        else
        {
            return orbital.viewMatrix;
        }
    }

    void ManejarInputVisibilidad()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            mostrarTecho = !mostrarTecho;
            techo.SetActive(mostrarTecho);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            mostrarParedes = !mostrarParedes;
            foreach (GameObject p in paredes)
                p.SetActive(mostrarParedes);
        }
    }

    void ActualizarShaders()
    {
        Matrix4x4 view = ObtenerMatrizVista();

        float aspect = Screen.width / (float)Screen.height; //para que no pierda y se vea mas cerca


        Matrix4x4 projection = MatrixVP.CrearPerspective(60f, aspect, 2f, 100f);
        projection = GL.GetGPUProjectionMatrix(projection, true);

        float fogDensity = usarNeblina ? 0.05f : 0f;


        foreach (GameObject obj in objetosInstanciados)
        {
            var mat = obj.GetComponent<MeshRenderer>().material;
            mat.SetMatrix("_ViewMatrix", view);
            mat.SetMatrix("_ProjectionMatrix", projection);

            //neblina
            mat.SetColor("_FogColor", new Color(0.53f, 0.81f, 0.98f));
            mat.SetFloat("_FogDensity", fogDensity);
        }
    }
}
