using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Utilidad estatica para construir matrices de modelado manualmente.
/// M = T x Rz x Ry x Rx x S
/// Igual que en clase practica 3.
/// </summary>
public static class ModelMatrix
{
    /// <summary>
    /// Construye la Model Matrix completa.
    /// position : traslacion en metros (1 unidad = 1 metro)
    /// rotation : rotacion en GRADOS (se convierte internamente a radianes)
    /// scale    : escala
    /// </summary>
    public static Matrix4x4 Create(Vector3 position, Vector3 rotationDegrees, Vector3 scale)
    {
        Vector3 rot = rotationDegrees * Mathf.Deg2Rad;

        Matrix4x4 T  = Translation(position);
        Matrix4x4 Rx = RotationX(rot.x);
        Matrix4x4 Ry = RotationY(rot.y);
        Matrix4x4 Rz = RotationZ(rot.z);
        Matrix4x4 S  = Scale(scale);

        return T * Rz * Ry * Rx * S;
    }

    // ── Matrices individuales ──────────────────────────────────────────
    public static Matrix4x4 Translation(Vector3 t)
    {
        Matrix4x4 m = new Matrix4x4(
            new Vector4(1f, 0f, 0f, t.x),
            new Vector4(0f, 1f, 0f, t.y),
            new Vector4(0f, 0f, 1f, t.z),
            new Vector4(0f, 0f, 0f, 1f)
        );
        return m.transpose;
    }

    public static Matrix4x4 RotationX(float radians)
    {
        float c = Mathf.Cos(radians), s = Mathf.Sin(radians);
        Matrix4x4 m = new Matrix4x4(
            new Vector4(1f, 0f,  0f, 0f),
            new Vector4(0f,  c,  -s, 0f),
            new Vector4(0f,  s,   c, 0f),
            new Vector4(0f, 0f,  0f, 1f)
        );
        return m.transpose;
    }

    public static Matrix4x4 RotationY(float radians)
    {
        float c = Mathf.Cos(radians), s = Mathf.Sin(radians);
        Matrix4x4 m = new Matrix4x4(
            new Vector4( c, 0f,  s, 0f),
            new Vector4(0f, 1f, 0f, 0f),
            new Vector4(-s, 0f,  c, 0f),
            new Vector4(0f, 0f, 0f, 1f)
        );
        return m.transpose;
    }

    public static Matrix4x4 RotationZ(float radians)
    {
        float c = Mathf.Cos(radians), s = Mathf.Sin(radians);
        Matrix4x4 m = new Matrix4x4(
            new Vector4( c, -s, 0f, 0f),
            new Vector4( s,  c, 0f, 0f),
            new Vector4(0f, 0f, 1f, 0f),
            new Vector4(0f, 0f, 0f, 1f)
        );
        return m.transpose;
    }

    public static Matrix4x4 Scale(Vector3 s)
    {
        Matrix4x4 m = new Matrix4x4(
            new Vector4(s.x, 0f,  0f,  0f),
            new Vector4(0f,  s.y, 0f,  0f),
            new Vector4(0f,  0f,  s.z, 0f),
            new Vector4(0f,  0f,  0f,  1f)
        );
        return m.transpose;
    }
}
