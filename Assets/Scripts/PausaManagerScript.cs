using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaManagerScript : MonoBehaviour
{
    public GameObject menuDePausa;
    bool escapePulsado = false;
    public Character player;
    public AlienCam alienScript;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//pausa con esc se quita con continuar y con esc de nuevo
        {
            escapePulsado = !escapePulsado;
            if (escapePulsado == true)
            {
                menuDePausa.SetActive(true);
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                player.enabled = false;
                alienScript.enabled = false;
            }
            else
            {
                Continuar();
            }
        }
    }



    public void Continuar()
    {
        player.enabled = true;
        alienScript.enabled = true;
        escapePulsado = false;
        menuDePausa.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public void CambiarDeDificultad()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuDificultad");
    }


    public void VolverAlMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("IntroAlJuego");
    }


    public void SalirDelJuego()
    {
        Application.Quit();
    }
}