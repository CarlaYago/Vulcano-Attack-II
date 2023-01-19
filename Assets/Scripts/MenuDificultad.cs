using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDificultad : MonoBehaviour
{
    public List<GameObject> listaTutorial;
    public int indice;
    public GameObject siguiente;
    public GameObject anterior;
    public GameObject saltarTutorialBoton;

    private void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {

        if (indice == 0) 
        {
            anterior.SetActive(false);
        }
        else
        {
            anterior.SetActive(true);
        }

        if (indice == listaTutorial.Count - 1)
        {
            saltarTutorialBoton.SetActive(false);
            siguiente.SetActive(false);
        }
        else
        {
            saltarTutorialBoton.SetActive(true);
            siguiente.SetActive(true);
        }
    }


    public void Next() // pasar pag
    {
        indice++;
        listaTutorial[indice - 1].SetActive(false);
        listaTutorial[indice].SetActive(true);
    }


    public void NoNext() //volver a la anterior pag
    {
        indice--;
        listaTutorial[indice + 1].SetActive(false);
        listaTutorial[indice].SetActive(true);
    }


    public void SaltarTutorial() // desactiva todas y activa la de elegir nivel
    {
        indice = listaTutorial.Count - 1;
        listaTutorial[0].SetActive(false);
        listaTutorial[1].SetActive(false);
        listaTutorial[2].SetActive(false);
        listaTutorial[3].SetActive(true);
    }


    public void VolverAlMenu()
    {
        SceneManager.LoadScene("IntroAlJuego");
    }


    public void Facil()
    {
        SceneManager.LoadScene("Facil");
    }


    public void Normal()
    {
        SceneManager.LoadScene("Media");
    }


    public void Dificil()
    {
        SceneManager.LoadScene("Dificil");
    }

    public void WhatsApp2()
    {
        SceneManager.LoadScene("video_WhatsApp2");
    }
}