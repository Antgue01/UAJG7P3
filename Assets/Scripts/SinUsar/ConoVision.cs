using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConoVision : MonoBehaviour
{
    public GameObject enemyGO;
    MoveEnemy enemyMov;
    TipoEnemigo TipoEnemigo;
    SpriteRenderer render;

    private void Start()
    {
        TipoEnemigo = enemyGO.GetComponent<TipoEnemigo>();
        enemyMov = gameObject.GetComponentInParent<MoveEnemy>();
        render = GetComponent<SpriteRenderer>();
    }

    /*private void LateUpdate()
    {
        //transform.position = enemyGO.transform.position;
        transform.right = enemyMov.DireccionActual();
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //grafo de detecciones
            if (TipoEnemigo.TipoDeEnemigo() == Disfraz.programador && (GameManager.instance.DisfrazJugador() == Disfraz.programador || GameManager.instance.DisfrazJugador() == Disfraz.ninguno)
            || TipoEnemigo.TipoDeEnemigo() == Disfraz.artista && (GameManager.instance.DisfrazJugador() == Disfraz.artista || GameManager.instance.DisfrazJugador() == Disfraz.diseñador || GameManager.instance.DisfrazJugador() == Disfraz.ninguno)
            || TipoEnemigo.TipoDeEnemigo() == Disfraz.diseñador && (GameManager.instance.DisfrazJugador() == Disfraz.diseñador || GameManager.instance.DisfrazJugador() == Disfraz.programador || GameManager.instance.DisfrazJugador() == Disfraz.artista || GameManager.instance.DisfrazJugador() == Disfraz.ninguno)
            || TipoEnemigo.TipoDeEnemigo() == Disfraz.personal)
            {
                Debug.Log("Player detected");
                enemyMov.CambiarEstado(Estados.persecucion);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && enemyMov.EstadoActual() == Estados.persecucion)
        {
            Debug.Log("Player lost");
            enemyMov.CambiarEstado(Estados.volviendo);
        }
    }

    // Activa el sprite
    public void ActivarRender()
    {
        render.enabled = true;
    }

    // Desactiva el sprite
    public void DesactivarRender()
    {
        render.enabled = false;
    }
}
