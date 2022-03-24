using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetivoCompletado : MonoBehaviour {

    public string sonidoVictoria;
    public GameObject parent;
	
    void OnEnable()
    {
        GameManager.instance.ObjetivoCumplido();
        GameManager.instance.ReproducirSonido(sonidoVictoria);
        GameManager.instance.SubirPlanta();
        parent.SetActive(false);
    }

   
}
