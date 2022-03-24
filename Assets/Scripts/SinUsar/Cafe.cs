using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cafe : MonoBehaviour {

    public string sonido;
    public float caos;
    public void OnEnable()
    {
        GameManager.instance.GenerarCaos(caos);
        GameManager.instance.ReproducirSonido(sonido);

    }
}
