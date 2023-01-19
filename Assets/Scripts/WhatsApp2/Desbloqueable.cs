using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Desbloqueable : MonoBehaviour
{
    public WhatsApp2_SO whatsapp2;
    public GameObject jugar;
    public Button boton;
    public Text textoboton;
    public Text textoexplicacion;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()//si consigues 25 tienes whatsapp 2 
    {
        if (Character.puntuacion >= 25)
        {
            whatsapp2.WhatsApp2 = true;
        }

        if (whatsapp2.WhatsApp2 && boton != null)
        {
            boton.enabled = true;
            textoboton.text = "WhatsApp 2";
            textoexplicacion.text = "";
        }
    }
}
