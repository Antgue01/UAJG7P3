using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour {
  
    
    public CircleCollider2D jugador;
    public GameObject imagenCheat;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("GodMode") )
        {
            if (jugador.enabled)
            {               
                
                jugador.enabled = false;
                imagenCheat.SetActive(true);
            }
            else
            {              
                jugador.enabled = true;
                imagenCheat.SetActive(false);
            }
        }        
	}

   
}
