using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sombra : MonoBehaviour
{
    public float tiempoVida;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        TipoEnemigo enemy = other.gameObject.GetComponent<TipoEnemigo>();
        if (enemy && enemy.TipoDeEnemigo() == Disfraz.guardia)
        {
            Destroy(this.gameObject,tiempoVida);
        }
    }

   
   
}
