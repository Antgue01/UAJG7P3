using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desactivar : MonoBehaviour {



    private void OnTriggerStay2D(Collider2D collision)
    {
        MovimientoEnemigo move = collision.gameObject.GetComponent<MovimientoEnemigo>();
        if (move&&move.Hellegado())
        {
            
            collision.gameObject.SetActive(false);
        }
    }
}
