using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CO  : MonoBehaviour{
    public Vector3 objetivo;
    private float rho;       // Distancia al objetivo

    private float theta;     // angulo polar
    private float phi;       

   

    void Start()
{
    Vector3 offset = transform.position - objetivo;
    rho = offset.magnitude;

    theta = Mathf.PI / 4f;
    phi = 0f;
}

    public Matrix4x4 CalcularMatrizVista(float deltaPhi, float deltaTheta){
        phi = phi + deltaPhi;
        theta = theta + deltaTheta; 
        float limiteMinimo = 0.05f;
        float limiteMaximo = (Mathf.PI / 2f) - 0.05f;
        theta = Mathf.Clamp(theta, limiteMinimo, limiteMaximo);
        // Usamos coordenadas esfericas
        float x = rho * Mathf.Sin(theta) * Mathf.Cos(phi);
        float y = rho * Mathf.Cos(theta);
        float z = rho * Mathf.Sin(theta) * Mathf.Sin(phi);

        Vector3 nuevaPosicion = new Vector3(
            objetivo.x + x, 
            objetivo.y + y, 
            objetivo.z + z
        );

        return MatrixVP.CreateViewMatrix(nuevaPosicion, objetivo, Vector3.up);
 
 
    }
public Matrix4x4 viewMatrix;

void Update()
{
    float velocidad = 2f;

    float deltaPhi = Input.GetAxis("Mouse X") * velocidad * Time.deltaTime;
    float deltaTheta = -Input.GetAxis("Mouse Y") * velocidad * Time.deltaTime;

    viewMatrix = CalcularMatrizVista(deltaPhi, deltaTheta);
}

}
