using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    public GameObject[] arrayRocas;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject roca in arrayRocas)
        {
            Rigidbody RBroca = roca.GetComponent<Rigidbody>();
            RBroca.AddForce(transform.up * 10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
