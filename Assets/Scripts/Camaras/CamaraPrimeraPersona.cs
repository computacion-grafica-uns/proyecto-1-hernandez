using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraPrimeraPersona : MonoBehaviour
{
    private Vector3 posicion;

    // Ángulos esféricos
    private float theta; // vertical
    private float phi;   // horizontal

    private const float ALTURA_CAMARA = 1.5f;

    public CamaraPrimeraPersona(Vector3 posicionInicial)
    {
        posicion = posicionInicial;
        theta = Mathf.PI / 2f; 
        phi = 0f;
    }

    public Matrix4x4 CalcularMatrizVista(
        float deltaPhi,
        float deltaTheta,
        float inputAvance,
        float inputLateral)
    {
        ActualizarAngulos(deltaPhi, deltaTheta);

        Vector3 direccion = CalcularDireccionMirada();
        Vector3 direccionPlano = ProyectarEnPlanoXZ(direccion);
        Vector3 derecha = CalcularVectorDerecha(direccion);

        ActualizarPosicion(direccionPlano, derecha, inputAvance, inputLateral);

        Vector3 objetivo = posicion + direccion;

        return MatrixVP.CreateViewMatrix(posicion, objetivo, Vector3.up);
    }

   
    private void ActualizarAngulos(float dPhi, float dTheta)
    {
        phi += dPhi;
        theta += dTheta;

        theta = Mathf.Clamp(theta, 0.05f, Mathf.PI - 0.05f);

    
    }

    private Vector3 CalcularDireccionMirada()
    {
        return new Vector3(
            Mathf.Sin(theta) * Mathf.Cos(phi),
            Mathf.Cos(theta),
            Mathf.Sin(theta) * Mathf.Sin(phi)
        ).normalized;
    }

    private Vector3 ProyectarEnPlanoXZ(Vector3 v)
    {
        return new Vector3(v.x, 0f, v.z).normalized;
    }

    private Vector3 CalcularVectorDerecha(Vector3 direccion)
    {
        return Vector3.Cross(direccion, Vector3.up).normalized;
    }

    private void ActualizarPosicion(
        Vector3 forward,
        Vector3 right,
        float avance,
        float lateral)
    {
       // posicion += forward * avance;
    //    posicion += right * lateral;

        Vector3 movimiento = forward * avance + right * lateral;
posicion += movimiento;

        // mantener altura fija
        posicion.y = ALTURA_CAMARA;
    }
}