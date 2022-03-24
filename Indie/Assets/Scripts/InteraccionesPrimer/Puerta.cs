using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour {

    public GameObject Abierta, Cerrada, Player;
    public Disfraz necesario;

    public void OnEnable()
    {
        if (Player.GetComponent<CambioDisfraz>().miDisfraz == necesario)
        {
            Abierta.SetActive(true);
            Cerrada.SetActive(false);
        }
        else
        {
            GameManager.instance.AvisoPuertas();
            this.gameObject.SetActive(false);
        }
    }
}
