using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Character : MonoBehaviour
{
    public Shoot ShootScript;

    //Sobre el movimiento del personaje
    public int velocidad = 10;
    public float movhoriz;
    public float movvert;

    public static float Vidas;
    public static float VidaJugador;
    public static float Energia;

    public Text Vida_text;
    public GameObject GameOver_text;
    public Text Puntuacion_text;

    public GameObject CargarEscena;
    public GameObject BotonVolverAlMenu;

    public static float puntuacion;

    public float speed;

    public Image Shield_Image;
    public Image Health_Image;
    public Image Energy_Image;


    Quaternion originalRotation;

    float rotationX = 0f;
    float rotationY = 0f;


    void Awake()
    {
        Vidas = 10;
        VidaJugador = 100;
        Energia = 100;
        GameOver_text.SetActive(false); // se desactiva el menú de game over en el start
        CargarEscena.SetActive(false);
        Time.timeScale = 1;
        puntuacion = 0;
        BotonVolverAlMenu.SetActive(false);
    }


    void Start()
    {
     //   Vector3 rot = transform.localRotation.eulerAngles;
     //   rotationX = rot.x;
        Cursor.visible = false;
    }


    // Update is called once per frame
    void Update()
    {
        Shield_Image.fillAmount = Vidas / 10;
        Health_Image.fillAmount = VidaJugador / 100;
        Energy_Image.fillAmount = Energia / 100;


        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocidad = 20;
        }

        else
        {
            velocidad = 10;
        }

        transform.position += new Vector3(movhoriz, movvert, 0.0f) * Time.deltaTime * velocidad;
      
        if (Vidas <= 0 || VidaJugador <= 0) //cuando mueres GAME OVER
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameOver_text.SetActive(true);
            CargarEscena.SetActive(true);
            BotonVolverAlMenu.SetActive(true);
            Time.timeScale = 0;

      
        }

        else 
        {
            rotationX += Input.GetAxis("Mouse X") * speed; //rotación en x, pilla el axis del ratón (está limitada con clamp)
            rotationX = Mathf.Clamp(rotationX, -90, 90);
           // Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up); //no se usa

            rotationY += Input.GetAxis("Mouse Y") * speed;
            rotationY = Mathf.Clamp(rotationY, -90, 90);
            //Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.up); //no se usa

            Quaternion localRotation = Quaternion.Euler(rotationY, rotationX + 180, 0f);
            transform.rotation = localRotation;

            ShootScript.gamma = (rotationX) * Mathf.PI / 180;
            //Debug.Log(ShootScript.gamma);
            ShootScript.alpha = (-rotationY - 90) * Mathf.PI / 180;

            Cursor.lockState = CursorLockMode.Locked;

            movvert = Input.GetAxis("Vertical");
            movhoriz = Input.GetAxis("Horizontal");
        }

        Vida_text.text = ""+Vidas;// ahora se usa la barra de vida
        Puntuacion_text.text = ""+puntuacion;
    }



    public void VolverAJugarFacil()
    {
        SceneManager.LoadScene("Facil");
    }



    public void VolverAJugarNormal()
    {
        SceneManager.LoadScene("Media");
    }


    public void VolverAJugarDificil()
    {
        SceneManager.LoadScene("Dificil");
    }

    public void VolverAJugarWhatsApp2()
    {
        SceneManager.LoadScene("WhatsApp2");
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("IntroAlJuego");
    }
}