using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarTextoUI : MonoBehaviour {

    public string texto;
    public float duracion;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.MostrarTextoEnPantalla(duracion, texto);
            Destroy(this.gameObject);
        }
    }
}
