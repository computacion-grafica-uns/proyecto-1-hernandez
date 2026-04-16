using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneManagerMonoambiente : MonoBehaviour
{
    public Shader shader;
     private GameObject cameraObject;

    void Start()
    {   
        
        // CREAR MUEBLES
        CrearObjeto("bed1", new Vector3(2,0,3), new Vector3(0,90,0));
        CrearObjeto("table", new Vector3(-2,0,1), Vector3.zero);
        CrearObjeto("chair1", new Vector3(-1,0,1), new Vector3(0,45,0));
        //CrearObjeto("fridge", new Vector3(4,0,-2), Vector3.zero);
 
          CrearObjeto("placard", new Vector3(-1,0,1), new Vector3(0,45,0));


        // CREAR ESCENA
        CrearObjeto("floor", Vector3.zero, Vector3.zero);
       // CrearObjeto("walls", Vector3.zero, Vector3.zero);
        //CrearObjeto("ceiling", new Vector3(0,3,0), Vector3.zero);
        //CrearObjeto("floor", Vector3.zero, Vector3.zero);
       // CrearObjeto("floor", new Vector3(0,3,0), Vector3.zero); // techo

        //CrearObjeto("wall_back", Vector3.zero, Vector3.zero);
        //CrearObjeto("wall_front", Vector3.zero, Vector3.zero);
        //CrearObjeto("wall_left", Vector3.zero, Vector3.zero);
        //CrearObjeto("wall_right", Vector3.zero, Vector3.zero);

        CreateCamera();
       
    }

   /*void CreateCamera()
    {
        cameraObject = new GameObject("MainCamera");
        Camera cam = cameraObject.AddComponent<Camera>();
        cam.orthographic = true;
        cam.orthographicSize = 1.5f;
        cam.backgroundColor = Color.white;
        //cam.clearFlags = CameraClearFlags.SolidColor;
        cameraObject.transform.position = new Vector3(0f, 0f, -10f);
        cameraObject.transform.rotation = Quaternion.identity;
    }*/


    void CreateCamera()
{
    cameraObject = new GameObject("MainCamera");
    Camera cam = cameraObject.AddComponent<Camera>();

    cam.backgroundColor = Color.white;

    
    cameraObject.transform.position = new Vector3(0f, 6f, -10f);
    cameraObject.transform.LookAt(Vector3.zero);
}

    void CrearObjeto(string nombreOBJ, Vector3 posicion, Vector3 rotacion)
    {
        OBJParser1 parser = new OBJParser1();

        Mesh mesh = parser.LoadOBJ(nombreOBJ);

        if (mesh == null) return;

        GameObject obj = new GameObject(nombreOBJ);

        MeshFilter mf = obj.AddComponent<MeshFilter>();
        MeshRenderer mr = obj.AddComponent<MeshRenderer>();

        mf.mesh = mesh;
        mr.material = new Material(shader);

        obj.transform.position = posicion;
        obj.transform.eulerAngles = rotacion;
    }
}