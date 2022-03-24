using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisfrazJugador : MonoBehaviour
{
    CambioDisfraz cambioDisfraz;
    public Disfraz disfrazNuevo;
    public GameObject parent;
    GameObject player;


    
    public void OnEnable()
    {
        player = GameObject.Find("Jugador1");
        //solo se cambia de disfraz el player
            cambioDisfraz = player.GetComponent<CambioDisfraz>();
            cambioDisfraz.MeCambio(disfrazNuevo);
        GameManager.instance.ReproducirPitchAleatorio("Cremallera");
            Destroy(parent.gameObject);
    }
}
