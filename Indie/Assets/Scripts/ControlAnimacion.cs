using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnimacion : MonoBehaviour {

    public int tiempoDeEspera;
    public GameObject go;
    private float time;

    private void Start()
    {
        //go.SetActive(false);
        time = Time.time + tiempoDeEspera;
    }

    private void Update()
    {
        if (Time.time <= time)
        {
            go.SetActive(true);
            Destroy(this);
        }
    }
}
