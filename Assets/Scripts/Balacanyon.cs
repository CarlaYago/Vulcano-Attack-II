using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balacanyon : MonoBehaviour
{
    //VARIABLES
    [Header("VARIABLES")]

    public float Tinc; //Tiempo incremental
    public float g; //Gravedad

    public Shoot Arma;
    public GameObject Player;

    public float alphaBala; //EJE Y
    public float gammaBala; //EJE X

    public float L; //longitud cañon
    public float yb; //Altura cañon

    public float vm; //Velocidad inicial

    public float vmx;
    public float vmy;
    public float vmz;

    float Lx;
    float Lz;
    float Ly;

    public float masa;

    float Cd; // coeficiente resistencia del aire experimental
    float Cw; // coeficiente de fuerza del viento
    float vw; // velocidad de viento 

    public float gammaViento; // ángulo de incidencia del viento

    [Header("ENERGIA")]

    public int masEnergia;
    public int masEnergiaPlus;
    public int masVidaPlayer;

    void Start()
    {
        // recoge el apuntado del ratón (rotación de la nave)
        alphaBala = Arma.alpha; 
        gammaBala = Arma.gamma;

        // están puestas en start para no consumir más, pero se cogen de arma por si se fuesen a cambiar en el propio juego
        Cd = Arma.Cd;
        Cw = Arma.Cw;
        vw = Arma.vw;

        float b = L * Mathf.Cos(1.57f - alphaBala); //proyeccion cañon

        //COMPONENTES DE L
        Lx = b * Mathf.Sin(gammaBala);
        Ly = L * Mathf.Cos(alphaBala);
        Lz = b * Mathf.Cos(gammaBala);

        //ángulos de los componentes de L (ángulos directores)
        float cos0x = Lx / L;
        float cos0y = Ly / L;
        float cos0z = Lz / L;

        vmx = cos0x * vm;
        vmy = cos0y * vm;
        vmz = cos0z * vm;

        gammaViento = gammaViento * Mathf.PI / 180;

        StartCoroutine("DestruirBala");
    }


    void Update()
    {
        Vectores newpos = simulacion(Time.deltaTime);
        transform.position = newpos.toVector3();
    }


    public Vectores simulacion(float tiempo)
    {
        Vectores r = new Vectores();
        Tinc += tiempo;
        //marca la dirección de la bala en cada momento, con las fórmulas de dinámica
        r.x = transform.position.x + (((masa / Cd) * Mathf.Exp((-Cd / masa) * Tinc)) * ((-(Cw * vw * Mathf.Sin(gammaViento)) / Cd) - vmx) - (((Cw * vw * Mathf.Sin(gammaViento)) / Cd) * Tinc)) - ((masa / Cd) * ((-(Cw * vw * Mathf.Sin(gammaViento)) / Cd) - vmx));
        r.y = transform.position.y + (-(vmy + ((masa * g) / Cd)) * (masa / Cd) * Mathf.Exp((-Cd / masa) * Tinc) - ((masa * g * Tinc) / Cd)) + ((masa / Cd) * (((masa * g) / Cd) + vmy));
        r.z = transform.position.z + (((masa / Cd) * Mathf.Exp((-Cd / masa) * Tinc)) * ((-(Cw * vw * Mathf.Cos(gammaViento)) / Cd) - vmz) - (((Cw * vw * Mathf.Cos(gammaViento)) / Cd) * Tinc)) - ((masa / Cd) * ((-(Cw * vw * Mathf.Cos(gammaViento)) / Cd) - vmz));

        return r;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target") //rocas normales
        {
            Character.puntuacion++;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "PowerUp") // vida de la nave
        {
            if (Character.Vidas < 10)
            {
                Character.Vidas++;
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Energy") //energia
        {
            if (Character.Energia + masEnergia < 100)
            {
                Character.Energia += masEnergia;
            }
            else if (Character.Energia + masEnergia >= 100)
            {
                Character.Energia = 100;
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "EnergyPlus")
        {
            if (Character.Energia + masEnergiaPlus < 100)
            {
                Character.Energia += masEnergiaPlus;
            }
            else if (Character.Energia + masEnergiaPlus >= 100)
            {
                Character.Energia = 100;
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "VidaPlayer") //vida jugador
        {
            if (Character.VidaJugador + masVidaPlayer < 100)
            {
                Character.VidaJugador += masVidaPlayer;
            }
            else if (Character.VidaJugador + masVidaPlayer >= 100)
            {
                Character.VidaJugador = 100;
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    IEnumerator DestruirBala()
    {
        yield return new WaitForSeconds(20f);
        Destroy(gameObject);
    }
}