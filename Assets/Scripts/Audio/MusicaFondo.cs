using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaFondo : MonoBehaviour
{
    public string sonido;
  private void Start()
    {
        GameManager.instance.ReproducirSonido("Liszt");
        //ReproducirSonido("Liszt");

    }
    private void Update()
    {
        
    }
}
