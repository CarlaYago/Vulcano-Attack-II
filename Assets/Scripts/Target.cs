using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //VARIABLES 
    [Header ("Prefabs")]
    public GameObject PrefabTarget;
    public GameObject PrefabPowerUp;
    public GameObject PrefabEnergia;
    public GameObject PrefabEnergiaPlus;
    public GameObject PrefabVidaPlayer;

    [Header("Otros")]
    public int RandApMinMax;
    int RandAp;
    public float Tiempo;

    [Header("Probabilidad(de 0 a 100)")]
    public int ProbabilidadVida;
    public int ProbabilidadEnergia;
    public int ProbabilidadEnergiaPlus;
    public int ProbabilidadVidaPlayer;



    // DIFICULTAD: Facil: 2f | Normal: 1.25F | Dificil: 0.75f //

    void Start()
    {
        InvokeRepeating("InstanciarTarget", 2f, Tiempo);
    }



    public void InstanciarTarget()
    {
        int dado = Random.Range(0, 100);
        RandAp = Random.Range(-RandApMinMax, RandApMinMax);

        //vida de la nave(power-up azul)
        if (dado < ProbabilidadVida)
        {
            GameObject PowerUp1 = Instantiate(PrefabPowerUp, transform.position, transform.rotation);
            PowerUp1.transform.position = new Vector3(transform.position.x + RandAp, transform.position.y, transform.position.z + RandAp);
            PowerUp1.transform.rotation = Quaternion.Euler(-25f, 0f, 0f);
        }

        //ENERGIA (power-up morado)
        else if ( dado > ProbabilidadVida && dado < ProbabilidadEnergia + ProbabilidadVida )
        {
            int dado2 = Random.Range(0, 100); //otro dado para ver si es plus
            
            if (dado2 <= ProbabilidadEnergiaPlus) // energia plus
            {
                GameObject Energia2 = Instantiate(PrefabEnergiaPlus, transform.position, transform.rotation);
                Energia2.transform.position = new Vector3(transform.position.x + RandAp, transform.position.y, transform.position.z + RandAp);
            }

            else
            {
                GameObject Energia1 = Instantiate(PrefabEnergia, transform.position, transform.rotation);
                Energia1.transform.position = new Vector3(transform.position.x + RandAp, transform.position.y, transform.position.z + RandAp);
            }
        }
        // vida al jugador (power-up verde)
        else if(dado < ProbabilidadEnergia + ProbabilidadVida + ProbabilidadVidaPlayer && dado > ProbabilidadVida + ProbabilidadEnergia)
        {
            GameObject PowerUpVidaPlayer = Instantiate(PrefabVidaPlayer, transform.position, transform.rotation);
            PowerUpVidaPlayer.transform.position = new Vector3(transform.position.x + RandAp, transform.position.y, transform.position.z + RandAp);
            PowerUpVidaPlayer.transform.rotation = Quaternion.Euler(-25f, 0f, 0f);
        }

        //ROCAS NORMALES
        else
        {
            GameObject Target1 = Instantiate(PrefabTarget, transform.position, PrefabTarget.transform.rotation);
            Target1.transform.position = new Vector3(transform.position.x + RandAp, transform.position.y, transform.position.z + RandAp);
        }
    } 
}