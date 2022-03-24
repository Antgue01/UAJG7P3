using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerConos : MonoBehaviour {

    Transform conoLuz;
    ConoDeVision deteccion;
    Disfraz disfrazActual;
    Disfraz disfrazNuevo;


    void Start()
    {
        disfrazActual = GameManager.instance.DisfrazJugador();
    }

    void Update()
    {
        disfrazNuevo = GameManager.instance.DisfrazJugador();
        if(disfrazActual != disfrazNuevo)
        {
            disfrazActual = disfrazNuevo;
            ActualizaConos();
        }
    }

    void ActualizaConos()
    {
        this.gameObject.SetActive(false);
        this.gameObject.SetActive(true);
    }

    // Si el cono de visión entra en el trigger se activa su renderer para que se vea el sprite
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<MovimientoEnemigo>())
        {
            deteccion = other.gameObject.GetComponentInChildren<ConoDeVision>();

            if (deteccion.CompruebaDeteccion())
            {
                conoLuz = other.transform.GetChild(1);
                conoLuz.gameObject.SetActive(true);
            }
        }
    }

    //Si está dentro del rango de visión de conos pero el jugador se cambia de disfraz, se actualiza
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<MovimientoEnemigo>())
        {
            deteccion = other.gameObject.GetComponentInChildren<ConoDeVision>();

            if (deteccion.CompruebaDeteccion())
            {
                conoLuz = other.transform.GetChild(1);
                conoLuz.gameObject.SetActive(true);
            }
            else
            {
                conoLuz = other.transform.GetChild(1);
                conoLuz.gameObject.SetActive(false);
            }
        }
    }

    // Si sale se desactiva
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<MovimientoEnemigo>())
        {
            conoLuz = other.transform.GetChild(1);
            conoLuz.gameObject.SetActive(false);
        }
    }
}
