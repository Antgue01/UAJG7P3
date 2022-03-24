using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronMov : MonoBehaviour {
    public Transform[] puntos;
    public Transform PuntoInstancia;
    public GameObject playerGO;
    EnemyMovement enemyMovement;
    RaycastHit2D hit;
    int i = 0;
    TipoEnemigo tipo;

    //Inicializacion de componentes
    void Start() 
    {
        tipo = GetComponent<TipoEnemigo>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyMovement.CambiarDireccion(puntos[i % puntos.Length]);
        Physics2D.queriesStartInColliders = false;
    }
    
    void Update()
    {
        switch (enemyMovement.EstadoActual())
        {
            case Estados.patrulla:
                if (Vector2.Distance(transform.position, puntos[i % puntos.Length].position) < 0.3)
                {
                    if (tipo.TipoDeEnemigo() == Disfraz.lead) enemyMovement.CambiarEstado(Estados.finTrayecto);

                    i++;
                }

                enemyMovement.CambiarDireccion(puntos[i % puntos.Length]);                              //Cambia la direccion al siguiente punto del recorrido
                break;

            case Estados.persecucion:
                hit = Physics2D.Raycast(transform.position, playerGO.transform.position - transform.position, Vector2.Distance(playerGO.transform.position, transform.position));   //Raycast
                Debug.DrawRay(transform.position, playerGO.transform.position - transform.position, Color.blue);        //Debug del raycast

                if (hit.transform.tag != "Player") enemyMovement.CambiarEstado(Estados.volviendo);    //En caso de que player no sea el primer GO, va al estado volviendo
                else
                {
                    enemyMovement.CambiarDireccion(playerGO.transform);       //Si no, persigue al player
                }
                break;

            case Estados.volviendo:
                hit = Physics2D.Raycast(transform.position, puntos[i % puntos.Length].position - transform.position, Vector2.Distance(puntos[i % puntos.Length].position, transform.position));   //Raycast
                Debug.DrawRay(transform.position, puntos[i % puntos.Length].position - transform.position);             //Debug Raycast

                if (hit.transform != null) i++;                                                         //Si hay algun obstaculo en el camino, cambia el punto de destino

                if (Vector2.Distance(transform.position, puntos[i % puntos.Length].position) < 0.3)     //Comprueba si está pegado al destino
                    enemyMovement.CambiarEstado(Estados.patrulla);                                      //Cambia de estado a patrulla
                
                else enemyMovement.CambiarDireccion(puntos[i % puntos.Length]);                         //Si todavia no ha llegado actualiza la direccion por si se ha desviado
                break;

            case Estados.finTrayecto:
                enemyMovement.Parar();
                break;
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<movimiento>())
        {
            Vector2 puntoNuevo = new Vector2(collision.GetContact(0).normal.x + 1, collision.GetContact(0).normal.y + 1);
            
         //GameObject punto=Instantiate<GameObject>(PuntoInstancia,puntoNuevo,)
            //enemyMovement.CambiarDireccion();
        }
    }*/
}
