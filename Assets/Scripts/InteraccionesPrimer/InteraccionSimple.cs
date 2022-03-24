using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteraccionSimple : MonoBehaviour {

    public string sonido;
    public float caos;
	
    public void OnEnable()
    {
        GameManager.instance.GenerarCaos(caos);
        GameManager.instance.ReproducirSonido(sonido);

    }
}
