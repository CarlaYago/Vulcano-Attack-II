using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movEstEsp : MonoBehaviour
{
    public float velocidadEstacionEspacial;



    void Update()
    {
        transform.Translate(velocidadEstacionEspacial * Time.deltaTime, 0, 0);
    }
}