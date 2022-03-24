using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coleccionable : MonoBehaviour {

    public GameObject parent;
    public string sonido;

    public void OnEnable()
        {
            Debug.Log("Coleccionable recogido");
            GameManager.instance.ColeccionableRecogido();
            GameManager.instance.ReproducirSonido(sonido);

            Destroy(parent.gameObject);
        }
    }

