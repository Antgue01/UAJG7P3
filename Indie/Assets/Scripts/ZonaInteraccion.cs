using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaInteraccion : MonoBehaviour {

    // Si el cono de visión entra en el trigger se activa su renderer para que se vea el sprite
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Interaccion>() && !other.gameObject.GetComponent<Interaccion>().EnRango() && other.gameObject.GetComponent<Interaccion>().Interactuable())
        {
            other.GetComponentInChildren<Interaccion>().MarcaTrazo(true);
        }
    }

    // Si sale se desactiva
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Interaccion>())
        {
            other.GetComponentInChildren<Interaccion>().MarcaTrazo(false);
        }
    }
}
