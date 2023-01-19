using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructor : MonoBehaviour
{
    public GameObject Player;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Target")
        {
            Character.Vidas--;
            Destroy(collision.gameObject);
        }


        if (collision.gameObject.tag == "PowerUp")
        {
            Destroy(collision.gameObject);
        }


        if (collision.gameObject.tag == "EnergyPlus")
        {
            Destroy(collision.gameObject);
        }


        if (collision.gameObject.tag == "Energy")
        {
            Destroy(collision.gameObject);
        }


        if (collision.gameObject.tag == "VidaPlayer")
        {
            Destroy(collision.gameObject);
        }
    }
}