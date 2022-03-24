using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persecucion : MonoBehaviour {

    private int estado;
    public float velocidad;
    private Rigidbody2D rb;
    private Vector2 direccionPersecucion;
    public Transform playerTransform;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        estado = 0;
        direccionPersecucion = new Vector2(0f, 0f);
	}
	
	void Update () {
        if (estado == 1)
        {
            direccionPersecucion = playerTransform.position - transform.position;
        }


	}

    void FixedUpdate()
    {
        if (estado == 1)
        {
            rb.velocity = direccionPersecucion;
        }
    }

    public void CambiaEstado(int nuevoEstado)
    {
        estado = nuevoEstado;
    }
}
