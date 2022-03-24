using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZonaClave : MonoBehaviour {

    public GameObject enemy;
    MovimientoEnemigo movimiento;

	void Start ()
    {
        Physics2D.queriesHitTriggers = false;               // Para que el raycast no detecte a este trigger
    }

    // Si el enemigo se sale del trigger deja de perseguir al jugador
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == enemy)
        {
            Debug.Log("abandono");
            movimiento = other.GetComponent<MovimientoEnemigo>();
            movimiento.AbandonoZona();
            movimiento.fueraZona = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == enemy)
        {
            for (float i = 0; i < 2; i = (i + (1 * Time.deltaTime))){ }

            movimiento = other.GetComponent<MovimientoEnemigo>();
            movimiento.fueraZona = false;

        }
    }
}
