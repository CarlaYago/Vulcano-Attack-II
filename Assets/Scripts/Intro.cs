using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public GameObject Jugar;
    public GameObject Controles;
    public GameObject Interfaz;

    bool jugarActivo = false;
    bool controlesActivos = false;
    bool interfazActiva = false;


    void Start()
    {
        Jugar.SetActive(false);
        Controles.SetActive(false);
        Interfaz.SetActive(false);
    }

    public void activarJugar() //boton jugar 
    {
        if (jugarActivo == false)
        {
            jugarActivo = true;
            Jugar.SetActive(true);
        }
        else
        {
            jugarActivo = false;
            Jugar.SetActive(false);
        }

        if (controlesActivos == true || interfazActiva == true) //desactiva otros si estan activados al dar al botón de jugar
        {
            controlesActivos = false;
            interfazActiva = false;
            Controles.SetActive(false);
            Interfaz.SetActive(false);
        }
    }


    public void activarControles()
    {
        if (controlesActivos == false)
        {
            controlesActivos = true;
            Controles.SetActive(true);
        }
        else
        {
            controlesActivos = false;
            Controles.SetActive(false);
        }

        if (jugarActivo == true || interfazActiva == true)
        {
            jugarActivo = false;
            interfazActiva = false;
            Jugar.SetActive(false);
            Interfaz.SetActive(false);
        }
    }


    public void ActivarInterfaz()
    {
        if (interfazActiva == false)
        {
            interfazActiva = true;
            Interfaz.SetActive(true);
        }
        else
        {
            interfazActiva = false;
            Interfaz.SetActive(false);
        }

        if (jugarActivo == true || controlesActivos == true)
        {
            jugarActivo = false;
            controlesActivos = false;
            Jugar.SetActive(false);
            Controles.SetActive(false);
        }
    }


    public void SalirDelJuego()
    {
        Application.Quit();
    }

    public void Comenzar()
    {
        SceneManager.LoadScene("MenuDificultad");
    }
}