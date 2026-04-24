using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearHabitacion : MonoBehaviour


{
    public Shader shader;
    private List<GameObject> todosLosObjetos = new List<GameObject>();

    // Clase que agrupa lo que necesita SceneManager para que es necesario? 
    public class RoomData
    {
        public List<GameObject> paredes = new List<GameObject>();
        public GameObject techo;
        public List<GameObject> todosLosObjetos = new List<GameObject>();
    }

//es un metodo o que ??
    public RoomData Construir()
    {
        ConstruirMuebles();
        ConstruirSillon();
        ConstruirCocina();
        ConstruirBaño();
        ConstruirExterior();

        RoomData data = new RoomData();
        data.paredes = ConstruirParedes();
        data.techo = ConstruirTecho();
        data.todosLosObjetos = todosLosObjetos;

        return data;
    }

    // ─────────────────────────────────────────
    //  MUEBLES DORMITORIO
    // ─────────────────────────────────────────
    private void ConstruirMuebles()
    {
        // Cama
        CrearObjeto("bed1",
            new Vector3(3f, -1f, -5.8f),
            new Vector3(0, 180, 0),
            Vector3.one,
            new Color(0.7f, 0.7f, 0.7f));

        CrearObjeto("almohada",
            new Vector3(3f, -0.8f, -5.9f),
            new Vector3(0, 90, 0),
            new Vector3(0.5f, 0.5f, 0.5f),
            new Color(0f, 0f, 0f));

        // Placard / perchas
        CrearObjeto("muebl",
            new Vector3(0.9f, -0.2f, -9.2f),
            new Vector3(0, 180, 0),
            Vector3.one,
            new Color(0.38f, 0.23f, 0.15f));

        CrearObjeto("barra",
            new Vector3(0.69f, 0.2f, -6.8f),
            new Vector3(0, 180, 0),
            Vector3.one,
            new Color(0.5f, 0.5f, 0.52f));

        CrearObjeto("barra",
            new Vector3(-0.3f, 0.4f, -6.8f),
            new Vector3(0, 180, 0),
            Vector3.one,
            new Color(0.5f, 0.5f, 0.52f));

        // Perchas con ropa
        CrearObjeto("percha1", new Vector3(0f,    0.23f, -7.0f), new Vector3(0, 180, 0), Vector3.one, Color.black);
        CrearObjeto("percha1", new Vector3(-0.4f, 0.23f, -7.0f), new Vector3(0, 180, 0), Vector3.one, Color.black);
        CrearObjeto("percha1", new Vector3(1f,    0f,    -7.0f), new Vector3(0, 180, 0), Vector3.one, Color.black);
        CrearObjeto("percha1", new Vector3(0.5f,  0f,    -7.0f), new Vector3(0, 180, 0), Vector3.one, Color.black);


        // Ropa colgada
        CrearObjeto("ropa", new Vector3(-0.4f, 1.0f, -7.0f), new Vector3(0, 90, 0), new Vector3(0.6f, 0.1f, 0.5f), new Color(0.1f, 0.2f, 0.5f));
        CrearObjeto("ropa", new Vector3(-0.4f, 0.9f, -7.0f), new Vector3(0, 90, 0), new Vector3(0.6f, 0.1f, 0.5f), new Color(0.6f, 0.2f, 0.15f));
        CrearObjeto("ropa", new Vector3(-0.4f, 0.8f, -7.0f), new Vector3(0, 90, 0), new Vector3(0.6f, 0.1f, 0.5f), new Color(0.35f, 0.4f, 0.2f));
    }

    // ─────────────────────────────────────────
    //  SILLON
    // ─────────────────────────────────────────
    private void ConstruirSillon()
    {
        CrearObjeto("sillo",
            new Vector3(2.1f, -0.8f, 6.7f),
            new Vector3(0, 240, 0),
            new Vector3(0.5f, 0.5f, 0.5f),
            new Color(0.15f, 0.25f, 0.35f));

        CrearObjeto("almohada",
            new Vector3(2.5f, -0.8f, 6.6f),
            new Vector3(0, 0, 0),
            new Vector3(0.5f, 0.5f, 0.5f),
            Color.black);

        CrearObjeto("almohada",
            new Vector3(1.5f, -0.8f, 6.6f),
            new Vector3(0, 90, 0),
            new Vector3(0.5f, 0.5f, 0.5f),
            Color.black);

        // mesa el sillón
        CrearObjeto("cube_base",
            new Vector3(3f, -1f, 4.5f),
            Vector3.zero,
            new Vector3(1f, 0.5f, 0.5f),
           new Color(0.65f, 0.55f, 0.45f));

        CrearObjeto("soportetv",
            new Vector3(1f, -1.5f, 0.2f),
            Vector3.zero,
            new Vector3(0.5f, 0.5f, 0.5f),
            new Color(0.1f, 0.1f, 0.1f));

             CrearObjeto("tvsinsoporte",
            new Vector3(1f, -0.5f, 0.2f),
            Vector3.zero,
                new Vector3(0.5f, 0.5f, 0.5f),

            new Color(0.1f, 0.1f, 0.1f));




    }

    // ─────────────────────────────────────────
    //  COCINA
    // ─────────────────────────────────────────
    private void ConstruirCocina()
    {
        CrearObjeto("fridge",
            new Vector3(-3.3f, -0.5f, 3.8f),
            new Vector3(0, 180, 0),
            Vector3.one,
            new Color(0.75f, 0.75f, 0.78f));

        // Línea separadora de la heladera
        CrearObjeto("cube_base",
            new Vector3(-2.9f, 0f, 3.8f),
            new Vector3(0,90,0),
            new Vector3(0.88f, 0.02f, 0.07f),
            new Color(0.1f, 0.1f, 0.1f));

        CrearObjeto("KitchenCabinetRounded",
            new Vector3(-2.5f, -0.9f, 6.1f),
            Vector3.zero,
            Vector3.one,
           new Color(0.4f, 0.42f, 0.45f));

        CrearObjeto("bacha",
            new Vector3(-2f, -0.3f, 6.5f),
            new Vector3(0, 180, 0),
            Vector3.one,
            new Color(0f, 0f, 0.7f));
    }

    // ─────────────────────────────────────────
    //  BAÑO
    // ─────────────────────────────────────────
    private void ConstruirBaño()
    {
        CrearObjeto("sink",
            new Vector3(-2f, -1f, -1.5f),
            new Vector3(0, 90, 0),
            Vector3.one,
            new Color(0.7f, 0.7f, 0.7f));

        CrearObjeto("shower",
            new Vector3(-3.4f, -0.5f, -6.2f),
            new Vector3(0, 90, 0),
            Vector3.one,
            new Color(0.4f, 0.7f, 0.85f));

        CrearObjeto("toilet4",
            new Vector3(-3.4f, -1f, -4.5f),
            new Vector3(-90, 0, 0),
            new Vector3(0.0015f,0.0015f,0.0015f),
           new Color(0.25f, 0.25f, 0.28f));
    }

    // ─────────────────────────────────────────
    //  PAREDES
    // ─────────────────────────────────────────
    private List<GameObject> ConstruirParedes()
    {
        List<GameObject> paredes = new List<GameObject>();
        Color paredColor = new Color(1.0f, 0.9f, 0.82f);
        float ancho = 8f;
        float profundidad = 15f;

        paredes.Add(CrearObjeto("wall_back1",  new Vector3(0, 0, -profundidad / 2f), Vector3.zero,          Vector3.one, paredColor));
        paredes.Add(CrearObjeto("wall_front1", new Vector3(0, 0,  profundidad / 2f), Vector3.zero,          Vector3.one, paredColor));
        paredes.Add(CrearObjeto("wall_left1",  new Vector3(-ancho / 2f, 0, 0),       Vector3.zero,          Vector3.one, paredColor));
        paredes.Add(CrearObjeto("wall_right1", new Vector3( ancho / 2f, 0, 0),       new Vector3(0, 180, 0),Vector3.one, paredColor));

        // Paredes interiores / divisiones
        paredes.Add(CrearObjeto("wallB1", new Vector3(-0.8f, 0f, -4.0f), Vector3.zero, Vector3.one, paredColor));
        paredes.Add(CrearObjeto("wallB2", new Vector3(-2.4f, 0f, -0.5f), Vector3.zero, Vector3.one, paredColor));

        // Pared detrás del mueble / ventana
        paredes.Add(CrearObjeto("paredmueble", new Vector3(3.9f, 0f, -0.5f), Vector3.zero,          Vector3.one, new Color(0.9f, 0.7f, 0.7f)));
        paredes.Add(CrearObjeto("paredmueble", new Vector3(0.7f, 0f, -0.5f), new Vector3(0, 90, 0), Vector3.one, new Color(0f, 0.7f, 0.7f)));

        // Ventana
        CrearObjeto("ventana", new Vector3(3.97f, 0f, 4.5f), Vector3.zero, Vector3.one, Color.black);

        // Manijas de ventana
        CrearObjeto("cube_base", new Vector3(3.9f, 0.2f, 4.2f), new Vector3(0, 90, 0), new Vector3(0.3f, 0.02f, 0.05f), new Color(0.1f, 0.1f, 0.1f));
        CrearObjeto("cube_base", new Vector3(3.9f, 0.2f, 4.7f), new Vector3(0, 90, 0), new Vector3(0.3f, 0.02f, 0.05f), new Color(0.1f, 0.1f, 0.1f));

        return paredes;
    }

    // ─────────────────────────────────────────
    //  TECHO Y PISO
    // ─────────────────────────────────────────
    private GameObject ConstruirTecho()
    {
        CrearObjeto("floor", new Vector3(0, -1.5f, 0), Vector3.zero, Vector3.one, new Color(0.96f, 0.70f, 0.45f)); // piso

        GameObject techo = CrearObjeto("floor", new Vector3(0, 1.5f, 0), Vector3.zero, Vector3.one, new Color(0.9f, 0.9f, 0.9f));
        return techo;
    }

    // ─────────────────────────────────────────
    //  EXTERIOR
    // ─────────────────────────────────────────
    private void ConstruirExterior()
    {
        CrearObjeto("cesped",
            new Vector3(0f, -1.5f, 9.5f),
            Vector3.zero,
            Vector3.one,
            new Color(0.3f, 0.5f, 0.2f));
    }

    // ─────────────────────────────────────────
    //  MÉTODO BASE (igual que tu CrearObjeto original)
    // ─────────────────────────────────────────
    private GameObject CrearObjeto(string nombreOBJ, Vector3 posicion, Vector3 rotacionGrados, Vector3 escala, Color color)
    {
       // 1. lee el obj para generar malla de vertices y triangulos
        OBJParser1 parser = new OBJParser1();
        Mesh mesh = parser.LoadOBJ(nombreOBJ);
        if (mesh == null) return null;

//recorre todos los vertices del obj y le asigna el color
        Color[] colors = new Color[mesh.vertexCount];
        for (int i = 0; i < colors.Length; i++)
            colors[i] = color;
        mesh.colors = colors;

        mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 10000f); // ??

//convierte los grados de tacion a radianes y luego crea la matriz modelo
        Vector3 rotacionRad = rotacionGrados * Mathf.Deg2Rad;
        Matrix4x4 modelMatrix = MatrixVP.CreateModelMatrix(posicion, rotacionRad, escala);

        GameObject obj = new GameObject(nombreOBJ);
        obj.AddComponent<MeshFilter>().mesh = mesh;

        Material mat = new Material(shader);
        mat.SetColor("_MaterialColor", color);//le mando al shadr el color 
        mat.SetMatrix("_ModelMatrix", modelMatrix); //le mando al shader la posicion final calculada por la matriz modelo
        obj.AddComponent<MeshRenderer>().material = mat; //renderer encargado de procesary se dibbuje e ala pantalla
        //materia=mat le asigno a renderer el amteriar que construi antes 

        todosLosObjetos.Add(obj); //añado el objeto a la lista de crearhabitacion
        return obj;
    }
}
