using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarSprite : MonoBehaviour {

    public GameObject objetoQueCambio;
    public GameObject objetoNuevo;
    public float caos;
    public string sonido;

    public void OnEnable()
    {
        GameManager.instance.GenerarCaos(caos);
        objetoQueCambio.SetActive(false);
        objetoNuevo.SetActive(true);
        GameManager.instance.ReproducirSonido(sonido);
    }
}
