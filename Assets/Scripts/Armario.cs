using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armario : MonoBehaviour {

    public GameObject jugador;
    public string sonido;

    private void OnEnable()
    {
        jugador.SetActive(false);
        GameManager.instance.ReproducirSonido(sonido);
    }
    void Update () {
        if (Input.GetButtonDown("Interaccion"))

        {
            jugador.SetActive(true);
            this.gameObject.SetActive(false);
        }
	}
}
