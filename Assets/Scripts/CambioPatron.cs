using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CambioPatron : MonoBehaviour
{
    int caos;
    bool cambio1Vez,llegado,cambio2vez;
    public Transform[] nuevoPuntos, fin;
    MovimientoEnemigo enemigo;
    public int caosConelqueCambia;
    public GameObject[] zonas;

    private void Start()
    {
        enemigo = GetComponent<MovimientoEnemigo>();
        cambio1Vez = false;
        llegado = false;
        cambio2vez = false;
    }

    private void Update()
    {
        caos = Convert.ToInt32(GameManager.instance.CaosActual());

        if (caos>=caosConelqueCambia && !cambio1Vez)
        {
            if (zonas.Length > 0) zonas[0].SetActive(false);
            enemigo.CambioPatron(nuevoPuntos);
            cambio1Vez = true;
        }

        if (cambio1Vez && !cambio2vez)
        {
            llegado = enemigo.Hellegado();

            if (llegado)
            {
                if (zonas.Length != 0) zonas[0].SetActive(true);
                enemigo.CambioPatron(fin);
                cambio2vez = true;
            }
        }
    }
}

