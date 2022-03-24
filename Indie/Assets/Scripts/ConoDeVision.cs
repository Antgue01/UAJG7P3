using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConoDeVision : MonoBehaviour {

    MovimientoEnemigo movEnemigo;
    TipoEnemigo tipoEnemigo;
    RaycastHit2D ray;
    GameObject playerGO;

    private void Start()
    {
        movEnemigo = GetComponentInParent<MovimientoEnemigo>();
        tipoEnemigo = GetComponentInParent<TipoEnemigo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.GetComponent<Perder>().enabled)
        {
            playerGO = collision.gameObject;
            ray = Physics2D.Raycast(transform.position, playerGO.transform.position - transform.position, Vector2.Distance(playerGO.transform.position, transform.position));  //Raycast
            Debug.DrawRay(transform.position, playerGO.transform.position - transform.position, Color.red);    //Debug del raycast

            if (ray.collider.tag == "Player" && CompruebaDeteccion()&& !movEnemigo.fueraZona)
            {
                Debug.Log("Player detected");
                movEnemigo.CambiaEstado(Estados.persecucion);
                GameManager.instance.ReproducirSonido("Exclamacion");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.GetComponent<Perder>().enabled)
        {
            playerGO = collision.gameObject;
            ray = Physics2D.Raycast(transform.position, playerGO.transform.position - transform.position, Vector2.Distance(playerGO.transform.position, transform.position));  //Raycast
            Debug.DrawRay(transform.position, playerGO.transform.position - transform.position, Color.red);    //Debug del raycast

            if (ray.collider.tag == "Player" && CompruebaDeteccion())
            {
                Debug.Log("Player detected");
                movEnemigo.CambiaEstado(Estados.persecucion);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.GetComponent<Perder>().enabled)
        {
            Debug.Log("Player lost");
            movEnemigo.PlayerPerdido();
        }
    }

    public bool CompruebaDeteccion()
    {
        return tipoEnemigo.TipoDeEnemigo() == Disfraz.programador && (GameManager.instance.DisfrazJugador() == Disfraz.programador || GameManager.instance.DisfrazJugador() == Disfraz.ninguno)
            || tipoEnemigo.TipoDeEnemigo() == Disfraz.artista && (GameManager.instance.DisfrazJugador() == Disfraz.artista || GameManager.instance.DisfrazJugador() == Disfraz.diseñador || GameManager.instance.DisfrazJugador() == Disfraz.ninguno)
            || tipoEnemigo.TipoDeEnemigo() == Disfraz.diseñador && (GameManager.instance.DisfrazJugador() == Disfraz.diseñador || GameManager.instance.DisfrazJugador() == Disfraz.programador || GameManager.instance.DisfrazJugador() == Disfraz.artista || GameManager.instance.DisfrazJugador() == Disfraz.ninguno)
            || tipoEnemigo.TipoDeEnemigo() == Disfraz.personal || tipoEnemigo.TipoDeEnemigo() == Disfraz.lead || tipoEnemigo.TipoDeEnemigo() == Disfraz.guardia;
    }
}
