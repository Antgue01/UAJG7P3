using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float velocidadInicial, velocidadPersecucion;
    public GameObject playerGO, exclamationPrefab;
    GameObject exclamationSprite;
    float velocidad;
    Rigidbody2D rb;
    Vector2 direction;
    Estados estado;
    RaycastHit2D hit;


    void Start () {
        rb = GetComponent<Rigidbody2D>();                 
        estado = Estados.patrulla;      //Comienza en estado patrulla
        velocidad = velocidadInicial;
        Physics2D.queriesStartInColliders = false;
    }

    private void Update()
    {
        if(estado == Estados.persecucion)
        {
            if (exclamationSprite != null)
            {
                exclamationSprite.transform.position = transform.position + new Vector3(0f, 1.2f, 0f);
                Debug.Log("yaay");
            }       //GO de la exclamación siempre sigue al enemigo                         
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = direction.normalized * velocidad;     //El NPC siempre se mueve en la direccion de la variable
    }

    public void CambiarDireccion(Transform nuevoDestino)    //Metodo para cambiar la direccion
    {
        direction = new Vector2(nuevoDestino.position.x - transform.position.x, nuevoDestino.position.y - transform.position.y);
    }

    public void CambiarEstado(Estados nuevoEstado)      //Metodo para cambiar el estado del NPC y la velocidad de éste
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
            velocidad = velocidadInicial;
        }
    }

    public Estados EstadoActual()           //Metodo de lectura del estado actual
    {
        return estado;
    }

    public Vector2 DireccionActual()        //Metodo de lectura de la direccion actual
    {
        return direction;
    }

    public void Parar()
    {
        velocidad = 0;
    }
}
