using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaLead : MonoBehaviour
{

    public GameObject Abierta, Cerrada, Lead;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("colision");
        if (collision.gameObject == Lead)
        {
            Debug.Log("lead");
            Abierta.SetActive(true);
            Cerrada.SetActive(false);
        }
    }
}
