using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vater : MonoBehaviour {
    public string sonido;
    public float Caos;
    public void OnEnable()
    {
        GameManager.instance.GenerarCaos(Caos);
        GameManager.instance.ReproducirSonido(sonido);
    }
}
