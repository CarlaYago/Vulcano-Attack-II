using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Vectores
{
    #region Variables

    public float x;
    public float y;
    public float z;

    public float Angulox;
    public float Anguloy;
    public float Anguloz;
    public float modulo1;

    #endregion Variables

    #region Constructores

    public Vectores()
    {
        x = 0;
        y = 0;
        z = 0;
        
    }

    public Vectores(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;

    }

    public void Angulos(float ax, float ay, float az, float modulo)
    {
        if ((Mathf.Cos(ax) + Mathf.Cos(ay) + Mathf.Cos(az)) == 1)
        {
            Angulox = ax;
            Anguloy = ay;
            Anguloz = az;
            modulo1 = modulo;
        }
        else if ((Mathf.Cos(Angulox * Angulox) + Mathf.Cos(Anguloy * Anguloy) + Mathf.Cos(Anguloz * Anguloz)) != 1) throw new System.ArgumentException("No sirve");
    }

    public Vectores Normalize()
    {
        float magn = Magnitude();

        if (magn == 0)
        {
            Debug.Log("Error, no se puede dividir entre cero");
            return this;
        }
        else
        {
            return new Vectores(x / magn, y / magn, z / magn);
        }
        
    }

    public Vectores Reverse()
    {
        return new Vectores(-x, -y, -z);
    }

    public float Magnitude()
    {
        float magnitud = Mathf.Sqrt(x * x + y * y + z * z);
        return magnitud;
    }


    //Operaciones 

    public static Vectores operator +(Vectores v, Vectores u) //Suma
    {
        return new Vectores(v.x + u.x, v.y + u.y, v.z + u.z);
    }

    public static Vectores operator -(Vectores v, Vectores u) //Resta
    {
        return new Vectores(v.x - u.x, v.y - u.y, v.z - u.z);
    }

    public static Vectores operator *(Vectores v, float x) //Multiplicación con un número
    {
        return new Vectores(v.x * x, v.y * x, v.z * x);
    }

    public static Vectores operator /(Vectores v, float x) //División
    {
        return new Vectores(v.x / x, v.y / x, v.z / x);
    }

    public static float operator %(Vectores v, Vectores u) //Producto Escalar
    {
        return (v.x * u.x + v.y * u.y + v.z * u.z);
    }

    public static Vectores operator ^(Vectores u, Vectores v) //Producto Vectorial
    {
        return new Vectores(u.y * v.z - u.z * v.y, u.z * v.x - u.x * v.z, u.x * v.y - u.y * v.x);
    }

    public override string ToString() //Representación en string
    {
        return "(" + x + ", " + y + ", " + z + ")";
    }

    public Vectores Triple(Vectores u, Vectores v, Vectores w) //Extra escalar triple
    {
        return new Vectores(u.x * (v.y * w.z - v.z * w.y), u.y * (v.z * w.x - v.x * w.z), u.z * (v.x * w.y - v.y * w.x));
    }

    public Vector3 toVector3()
    {
        return new Vector3(x, y, z);
    }


    #endregion Constructores
}
