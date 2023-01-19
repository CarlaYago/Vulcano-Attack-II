using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    //public float gravedad = -9.8f;
    //public float masa;
    public float Cd; // Coeficiente de resistencia del aire
    public float Cw; // Coeficiente de fuerza del viento

    public float vw; // Velocidad del viento
    float vwx; // la velocidad del viento no afecta a la dirección y
    float vwz;

    public float alpha; //EJE Y
    public float gamma; //EJE X

    public GameObject Nave;

    public Balacanyon Balacanyon_Script;
    public float potenciaBala;
    public float vm;

    float timeAux; //para lanzar balas cada 1 segundo

    public GameObject balas; //prefab

    public Image image_Potencia;

    float NuevoGamma;

    float Red;
    float Green;
    float cambio1;
    float cambio2;

    public AudioSource sonidoDeDisparo;


    void Start()
    {
        timeAux = Time.time;
        vm = -0.1f;
        image_Potencia.fillAmount = 0;
        Green = 1;
    }



    void Update()
    {
        float timeDif = Time.time - timeAux;
        if (Input.GetKey(KeyCode.Space))
        {
            if (vm >= -10) // sumar potencia 
            {
                vm = vm - potenciaBala * Time.deltaTime;
                image_Potencia.fillAmount = vm / -10; // dividido entre 10 para que sea de 0 a 1 (para que funcione la barra de potencia)
                image_Potencia.color = new Color(Red, Green, 0f);

                if (Red < 1) // la barra va cambiando de color a amarillo sumándole rojo
                {
                    cambio1 = vm / -10 * 2; // va el doble de rápido para que cambie a amarillo en la mitad de la barra
                    Red = Mathf.Lerp(0, 1, cambio1);
                }

                
                if (Red == 1) // cuando ya está en amarillo sube  a rojo bajando el verde para que se quede en rojo solo 
                {
                    cambio2 = (vm / -10 * 2) - cambio1;
                    Green = Mathf.Lerp(1, 0, cambio2);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && timeDif > 0.25f) // coge vm e instancia la bala, resetea la barra de potencia y su color y la velocidad 
        {
            sonidoDeDisparo.Play();
            Instanciar(vm);
            vm = -0.1f;
            timeAux = Time.time;
            image_Potencia.fillAmount = 0;
            Green = 1;
            Red = 0;
        }
    }
    


    public void Instanciar(float vm)
    {
        Quaternion VectorDirBala = Quaternion.Euler(Nave.transform.rotation.x, Nave.transform.rotation.y, Nave.transform.rotation.z);
        GameObject Balas1 = Instantiate(balas, transform.position, VectorDirBala);
        Balas1.SetActive(true);
        Balas1.GetComponent<Balacanyon>().vm = vm; // coge la velocidad y se la pasa al script de balacanyon
    }
}