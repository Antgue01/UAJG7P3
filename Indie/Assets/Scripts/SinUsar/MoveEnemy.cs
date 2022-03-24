using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour {

    public float velocidadInicial, velocidadPersecucion;
    public GameObject playerGO, exclamationPrefab;
    public Transform[] puntos;
    public Estados estadoInicial;
    Estados estado;
    //public Transform PuntoInstancia;
    float velocidad;
    GameObject exclamationSprite;
    Rigidbody2D rb;
    RaycastHit2D hit;
    Vector2 direction;
    //TipoEnemigo tipo;


    void Start () {
        rb = GetComponent<Rigidbody2D>();
        //tipo = GetComponent<TipoEnemigo>();
        Physics2D.queriesStartInColliders = false;

        velocidad = velocidadInicial;
        estado = estadoInicial;
    }
    
    //Sigue una ruta de puntos
    public void Patrulla(ref int i)
    {
        if (Vector2.Distance(transform.position, puntos[i % puntos.Length].position) < 0.3)
        {
            /*if (tipo.TipoDeEnemigo() == Disfraz.lead)
                CambiarEstado(Estados.finTrayecto);*/
            i++;
        }
        CambiarDireccion(puntos[i % puntos.Length]);        //Cambia la direccion al siguiente punto del recorrido
    }
    
    //Persigue al jugador hasta perderlo de vista
    public void Persecucion()
    {
        if (exclamationSprite != null)
            exclamationSprite.transform.position = transform.position + new Vector3(0f, 1.2f, 0f);      //GO de la exclamación siempre sigue al enemy

        hit = Physics2D.Raycast(transform.position, playerGO.transform.position - transform.position, Vector2.Distance(playerGO.transform.position, transform.position));   //Raycast
        Debug.DrawRay(transform.position, playerGO.transform.position - transform.position, Color.red);        //Debug del raycast

        if (hit.transform.tag != "Player")
            CambiarEstado(Estados.volviendo);               //En caso de que player no sea el primer GO, va al estado volviendo
        else
            CambiarDireccion(playerGO.transform);           //Si no, persigue al player
    }
    
    //Vuelve a su ruta de patrulla o a su posición inicial
    public void Volviendo(ref int i)
    {
        hit = Physics2D.Raycast(transform.position, puntos[i % puntos.Length].position - transform.position, Vector2.Distance(puntos[i % puntos.Length].position, transform.position));
        Debug.DrawRay(transform.position, puntos[i % puntos.Length].position - transform.position);             //Debug Raycast

        
        if (hit.transform != null)                          //Si hay algun obstaculo en el camino, cambia el punto de destino
            i++;

        if (Vector2.Distance(transform.position, puntos[i % puntos.Length].position) < 0.3)     //Comprueba si está pegado al destino
        {
            if (estadoInicial == Estados.finTrayecto)       //Comprueba si el enemigo debe estar quieto o no
            {
                if (i % puntos.Length == puntos.Length - 1) //Si debe estar quieto, comprueba si está en el último punto de la ruta
                    CambiarEstado(Estados.finTrayecto);     //Si lo está, cambia de estado a finTrayecto 
                else
                {
                    i++;                                    //Si no está en el último, pasa al siguiente punto
                    CambiarDireccion(puntos[i % puntos.Length]);
                }
            }
            else
                CambiarEstado(Estados.patrulla);            //Si no debe estar quieto, cambia de estado a patrulla
        }
        else
            CambiarDireccion(puntos[i % puntos.Length]);    //Si todavia no ha llegado, actualiza la direccion por si se ha desviado
    }
    
    //Metodo para cambiar el estado del NPC y la velocidad de este
    public void CambiarEstado(Estados nuevoEstado)      
    {
        estado = nuevoEstado;

        if (nuevoEstado == Estados.persecucion)
        {
            exclamationSprite = Instantiate<GameObject>(exclamationPrefab, transform);      //Instancia el GO de la exclamación
            exclamationSprite.transform.position += new Vector3(0f, 1.2f, 0f);              //Sitúa el GO sobre la cabeza del enemy
            velocidad = velocidadPersecucion;
        }
        else
        {
            Destroy(exclamationSprite);
            if (nuevoEstado == Estados.finTrayecto)
                velocidad = 0;
            else
                velocidad = velocidadInicial;
        }
    }
    
    //El NPC siempre se mueve en la direccion de la variable
    private void FixedUpdate()
    {
        rb.velocity = direction.normalized * velocidad;
    }
    
    //Metodo para cambiar la direccion del enemigo según el destino dado
    public void CambiarDireccion(Transform nuevoDestino)    
    {
        direction = new Vector2(nuevoDestino.position.x - transform.position.x, nuevoDestino.position.y - transform.position.y);
    }

    //Metodo de lectura de la direccion actual
    public Vector3 DireccionActual()        
    {
        return direction;
    }
    
    //Metodo de lectura del estado actual
    public Estados EstadoActual()           
    {
        return estado;
    }
    public void CambioPatron(Transform [] puntosnuevos)
    {
        puntos = puntosnuevos;
    }
    public bool Hellegado()
    {
        if (Vector2.Distance(transform.position, new Vector2( puntos[puntos.Length-1].position.x,puntos[puntos.Length-1].position.y)) < 1f)
        {
            return true;
        }
        else return false;

    }
    /*private void Update()
    {
        Debug.Log(direction.x + " " + direction.y);
    }*/
}
