using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeteccionCamara : MonoBehaviour {

    public GameObject sombra;
    public GameObject enemigo;
    public Transform[]rutaPatrulla;
    MovimientoEnemigo movEnemy;

    void Start () {
        movEnemy = enemigo.GetComponent<MovimientoEnemigo>();
        enemigo.SetActive(false);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, other.transform.position - transform.position);
            Debug.DrawRay(transform.position, other.transform.position - transform.position, Color.red, 10f);
            Debug.Log(ray.transform.tag);

                if (!enemigo.activeSelf && ray.transform.tag == "Player")
                {
                    GameObject poolSombras = GameObject.Find("SombrasPool");
                    GameObject silueta = Instantiate<GameObject>(sombra, other.transform.position, other.transform.rotation, poolSombras.transform);
                    Transform aux = silueta.transform;
                GameManager.instance.ReproducirSonido("Seguridad");

                    Transform[] rutaNueva = new Transform[rutaPatrulla.Length + 1];
                    for (int i = 0; i < rutaPatrulla.Length; i++)
                    {
                        rutaNueva[i] = rutaPatrulla[i];
                    }
                    rutaNueva[rutaNueva.Length - 1] = aux;

                    enemigo.SetActive(true);
                    movEnemy.CambioPatron(rutaNueva);
                }
   
        }
    }
    public void GuardarVuelta(Transform[] enemigo)
    {
        rutaPatrulla = enemigo;
        for (int i = 0; i < rutaPatrulla.Length / 2; i++)
        {
            Swap(ref rutaPatrulla[i], ref rutaPatrulla[rutaPatrulla.Length - i - 1]);
        }
    }

    void Swap(ref Transform a, ref Transform b)
    {
        Transform temp = b;
        b = a;
        a = temp;
    }

    public void ComprobarNoNull(Transform[] rutaPatrulla)
    {
        if (rutaPatrulla[0] == null)
        {
            rutaPatrulla[0] = rutaPatrulla[1];
        }
        else if (rutaPatrulla[rutaPatrulla.Length - 1]==null)
        {
            rutaPatrulla[rutaPatrulla.Length - 1] = rutaPatrulla[rutaPatrulla.Length - 2];
        }
    }
}
