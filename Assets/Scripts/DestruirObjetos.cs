using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirObjetos : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("TiempoDeDestruccion");
    }

    IEnumerator TiempoDeDestruccion()
    {
        yield return new WaitForSeconds(45f);
        Destroy(gameObject);
    }
}