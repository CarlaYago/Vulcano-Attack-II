using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlienCam : MonoBehaviour
{
    [Header("Player (cambiar referencia)")]
    public Character jugador;
    bool iniciarrestaenergia = true;
    public int energiaarestar;
    public float tiempoentreresta;
    public int energiaparaabrirtrampilla;
    public GameObject NavePlayer;

    [Header("Daño causado")]
    public int danyo;
    public float tiempoentredanyos;
    bool iniciardanyo = true;

    [Header("Posiciones")]
    public List<RectTransform> listaposicionesUI;
    int posicionactual = 0;
    public List<Transform> listaposicionesnave;
    public GameObject alien;
    GameObject alieninstanciado;
    public List<GameObject> listaOjos;


    [Header("Imágenes UI")]
    public GameObject simbolo_alien;
    RectTransform pos_alien;
    public GameObject camarasdesactivadas;
    public GameObject tecla_mataralien;
    public TextMeshProUGUI GameOver_texto_real;

    [Header("Probabilidad Aparición (de 0 a 1)")]
    public float probabilidadap;
    public float tiempoentreprobabilidadesap;
    bool iniciaraparicion = true;

    [Header("Probabilidad Movimiento (de 0 a 1)")]
    public float probabilidadmov;
    public float probabilidadirporplayer;
    public float tiempoentreprobabilidadesmov;
    public float tiempopararetroceso;
    bool iniciarmovimiento, retroceder;
    float contador;

    [Header("Lista Cámaras")]
    public List<GameObject> listacamaras;
    int camaraactiva = 0;
    bool mirarcamaras = false;
    bool mirarcamarasdesactivadas = true;

    [Header("Audio")]
    public AudioSource audioScreamer;
    bool iniciarScreamer = true;
    private Animator animator;
    public AudioSource sonidoCambioDeSala;
    public AudioSource sonidoAlarma;

    [Header("WhatsApp 2")]
    public bool wsp2;

    void Start()
    {
        pos_alien = simbolo_alien.GetComponent<RectTransform>();
    }

    void Update()
    {
        #region mirar camaras
        if (Input.GetKeyDown(KeyCode.X) && Character.Vidas > 0 && Character.VidaJugador > 0) // si das a x y sigues vivo
        {
            mirarcamaras = !mirarcamaras;
        }

        if (Character.Energia > 0)
        {
            camarasdesactivadas.SetActive(false); // No mostrar el mensaje de cámaras desactivadas si la energía del jugador está por encima de 0
            listacamaras[camaraactiva].SetActive(mirarcamaras); // Mostrar por pantalla la cámara activa si el jugador está mirando las cámaras
            listaOjos[camaraactiva].SetActive(mirarcamaras);
            mirarcamarasdesactivadas = mirarcamaras;

            if (Input.GetAxis("Mouse ScrollWheel") < 0 && Character.Vidas > 0 && Character.VidaJugador > 0) // rueda del ratón activa camaras
            {
                mirarcamaras = true;
                if (camaraactiva < listacamaras.Count - 1)
                {
                    camaraactiva++;
                    listacamaras[camaraactiva - 1].SetActive(false);
                    listacamaras[camaraactiva].SetActive(true);

                    listaOjos[camaraactiva - 1].SetActive(false);
                    listaOjos[camaraactiva].SetActive(true);
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0 && Character.Vidas > 0 && Character.VidaJugador > 0)
            {
                mirarcamaras = true;
                if (camaraactiva > 0)
                {
                    camaraactiva--;
                    listacamaras[camaraactiva + 1].SetActive(false);
                    listacamaras[camaraactiva].SetActive(true);

                    listaOjos[camaraactiva + 1].SetActive(false);
                    listaOjos[camaraactiva].SetActive(true);
                }
            }
        }
        else // energia = 0
        {
            foreach (GameObject camara in listacamaras)
            {
                camara.SetActive(false);
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0 && Character.Vidas > 0 && Character.VidaJugador > 0) //activa el mensaje de camaras apagadas
            {
                mirarcamarasdesactivadas = true;
            }

            camarasdesactivadas.SetActive(mirarcamarasdesactivadas);
            mirarcamaras = false;
        }
        #endregion mirar camaras

        #region resta energia
        if (mirarcamaras)
        {
            if (iniciarrestaenergia)
            {
                InvokeRepeating("RestarEnergia", tiempoentreresta, tiempoentreresta);
                iniciarrestaenergia = false;
            }
        }
        else
        {
            CancelInvoke("RestarEnergia");
            iniciarrestaenergia = true;
        }
        #endregion resta energia

        #region posicion alien 
        pos_alien.anchoredPosition = listaposicionesUI[posicionactual].anchoredPosition; // Posición UI

        if (alieninstanciado != null) // Posición del gameobject
        {
            alieninstanciado.transform.position = listaposicionesnave[posicionactual].position;
            alieninstanciado.transform.rotation = listaposicionesnave[posicionactual].rotation;
        }
        #endregion posicion alien 

        #region daño
        if (posicionactual == 5 && iniciardanyo) //si el alien está en la posición del generador te causa daño
        {
            animator.SetBool("matarJugador", true);
            sonidoAlarma.Play();
            InvokeRepeating("CausarDanyo", tiempoentredanyos, tiempoentredanyos);
            iniciardanyo = false;
        }

        if (posicionactual == 4)
        {
            if (iniciarScreamer)
            {
                StartCoroutine("Screamer");
            }
        }
        #endregion daño

        #region aparicion
        if (iniciaraparicion)
        {
            InvokeRepeating("Aparecer", tiempoentreprobabilidadesap, tiempoentreprobabilidadesap);
            iniciaraparicion = false;
        }
        #endregion aparicion

        #region movimiento
        if (iniciarmovimiento)
        {
            InvokeRepeating("Movimiento", tiempoentreprobabilidadesmov, tiempoentreprobabilidadesmov);
            iniciarmovimiento = false;
        }

        /*if ((camaraactiva == 4 - posicionactual) && mirarcamaras || (camaraactiva == 0 && posicionactual == 5) && mirarcamaras) // Si el jugador está mirando al alien, hacer que retroceda
        {
            iniciarretroceso = true;
        }*/

        if (retroceder && Character.Energia > 0)
        {
            if ((camaraactiva == 4 - posicionactual) && mirarcamaras || (camaraactiva == 0 && posicionactual == 5) && mirarcamaras)// si estás mirando a la cámara donde está el alien
            {
                if (posicionactual != 5) // Iniciar el proceso de retroceder si el alien no está en el generador
                {
                    CancelInvoke("Movimiento");
                    contador += Time.deltaTime;

                    if (contador >= tiempopararetroceso && posicionactual != 0) // Si el jugador mira al alien durante "tiempopararetroceso" segundos, hacer que retroceda
                    {
                        posicionactual--;
                        retroceder = false;
                    }
                }
                else // Si el alien está en el generador, matarlo al pulsar E (perdiendo energía extra) y reiniciar la probabilidad de que aparezca un nuevo alien
                {
                    tecla_mataralien.SetActive(true);

                    if (Input.GetKey(KeyCode.E))
                    {
                        #region detener comportamiento
                        CancelInvoke("Movimiento");
                        CancelInvoke("CausarDanyo");
                        sonidoAlarma.Stop();
                        #endregion detener comportamiento

                        #region destruir alien
                        Destroy(alieninstanciado);
                        simbolo_alien.SetActive(false);
                        #endregion destruir alien

                        #region stats player
                        Character.Energia -= energiaparaabrirtrampilla; // Restar energía
                        Character.puntuacion += 10;
                        #endregion stats player

                        #region preparar respawn de alien
                        posicionactual = 0;
                        iniciaraparicion = true;
                        iniciardanyo = true;
                        #endregion preparar respawn de alien

                        retroceder = false;
                    }
                }
            }
            else // Si el jugador cambia de cámara antes de que termine la cuenta atrás para el retroceso del alien, cancelar el retroceso
            {
                retroceder = false;
            }
        }
        else // Cuando el alien haya retrocedido o se canceló el retroceso, si el alien sigue en la nave, que reinicie su movimiento 
        {
            if (contador != 0)
            {
                if (!iniciaraparicion)
                {
                    iniciarmovimiento = true;
                }

                contador = 0;
            }

            tecla_mataralien.SetActive(false);
        }

        #endregion movimiento
    }

    IEnumerator Screamer()
    {
        CancelInvoke("Movimiento");
        iniciarScreamer = false;
        animator.SetBool("matarJugador", true);
        audioScreamer.Play();
        if (wsp2)
        {
            GameOver_texto_real.text = "descargaste";
        }
        yield return new WaitForSeconds(3f);
        Character.VidaJugador = 0;
    }

    void CausarDanyo()
    {
        Character.VidaJugador = Character.VidaJugador - danyo;
    }

    void Aparecer()
    {
        float calculo = Random.Range(0f, 1f);

        if (calculo <= probabilidadap)
        {
            sonidoCambioDeSala.Play();

            simbolo_alien.SetActive(true);
            iniciarmovimiento = true;

            alieninstanciado = Instantiate(alien, listaposicionesnave[posicionactual].position, listaposicionesnave[posicionactual].rotation);
            alieninstanciado.transform.SetParent(NavePlayer.transform);

            animator = alieninstanciado.GetComponent<Animator>();

            CancelInvoke("Aparecer");
        }
    }

    void Movimiento()
    {
        if (((camaraactiva == 4 - posicionactual) && mirarcamaras || (camaraactiva == 0 && posicionactual == 5) && mirarcamaras)) // Si el jugador está mirando al alien, hacer que retroceda
        {
            retroceder = true;
        }
        else // Si el alien no está en retroceso, hacer que avance
        {
            float calculo = Random.Range(0f, 1f);

            if (calculo <= probabilidadmov && posicionactual < listaposicionesUI.Count - 1)
            {
                sonidoCambioDeSala.Play();
                if (posicionactual != 2 && posicionactual != 4) // La posición 2 es la intersección de caminos, la posición 4 la cabina del jugador
                {
                    posicionactual++;
                }
                else if (posicionactual == 2)
                {
                    float calculo2 = Random.Range(0f, 1f);

                    if (calculo2 <= probabilidadirporplayer)
                    {
                        posicionactual++; // va a por ti 
                    }
                    else
                    {
                        posicionactual = 5; // La posición 5 es el generador de la nave
                    }
                }
            }
        }
    }

    void RestarEnergia()
    {
        if ((Character.Energia - energiaarestar) > 0) // Si la resta de energía no da un número negativo, restar energía
        {
            Character.Energia -= energiaarestar;
        }
        else
        {
            Character.Energia = 0;
        }
    }
}
