using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canyon : MonoBehaviour
{
    //no se usa
    //VARIABLES
    public float vm; //Velocidad inicial

    public float alpha; //EJE Y
    public float gamma; //EJE X

    public float L; //longitud cañon
    public float yb; //Altura cañon

    float vmx;
    float vmy;
    float vmz;
    float Lx;
    float Lz;
    float Ly;

    float posx;
    float posy;
    float posz;

    public List<GameObject> BalasActivas = new List<GameObject>();

    public GameObject balas; //prefab
    float timeAux; //para lanar balas cada 1 segundo

    private void Awake()
    {
        gamma = gamma * Mathf.PI / 180;
        alpha = alpha * Mathf.PI / 180;
    }
    void Start()
    {
        timeAux = Time.time;
       
        float b = L * Mathf.Cos(1.57f - alpha); //proyeccion cañon

        //COMPONENTES DE L
        Lx = b * Mathf.Sin(gamma);
        Ly = L * Mathf.Cos(alpha);
        Lz = b* Mathf.Cos(gamma);

        float cos0x = Lx / L;
        float cos0y = Ly / L;
        float cos0z = Lz / L;

        vmx = cos0x * vm;
        vmy = cos0y * vm;
        vmz = cos0z * vm;

    }

    public Vectores simulacion(Balacanyon bala, float tiempo)
    {
        Vectores r = new Vectores();
        bala.Tinc += tiempo;

        r.x = posx + Lx + vmx * bala.Tinc;
        r.y = (posy + Ly) + vmy * bala.Tinc - (bala.g * Mathf.Pow(bala.Tinc, 2) / 2);
        r.z = posz +Lz + vmz * bala.Tinc;
        return r;
    }

    void Update()
    {
        float timeDif = Time.time - timeAux;
        if ( Input.GetKeyDown(KeyCode.Space) && timeDif > 1f) //DISPARAR BALAS CADA 1 SEGUNDO AL PRESIONAR SPACE
        {
            Instanciar();
            timeAux = Time.time;
        }
        for (int i = 0; i < BalasActivas.Count; i++)
        {
            GameObject bullet = BalasActivas[i];
            Vectores newpos = simulacion(bullet.GetComponent<Balacanyon>(), Time.deltaTime);
            bullet.transform.position = newpos.toVector3();
        }

    }

    public void Instanciar()
    {
        GameObject Balas1 = Instantiate(balas, transform.position, transform.rotation);
        Balas1.SetActive(true);
        BalasActivas.Add(Balas1);


        posx = transform.position.x;
        posy = transform.position.y;
        posz = transform.position.z;

    }
}
