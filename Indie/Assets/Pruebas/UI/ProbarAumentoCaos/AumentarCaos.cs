using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AumentarCaos : MonoBehaviour {
    public float caos;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {  
            GameManager.instance.GenerarCaos(caos);
    }
}
