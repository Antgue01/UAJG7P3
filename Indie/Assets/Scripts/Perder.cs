using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UAJ;

public class Perder : MonoBehaviour
{

    void Start()
    {

    }
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy") && this.enabled)       //Solo muero si me choco con un enemigo y mi script de perder está activado
        {

            GameManager.instance.Perder();
            Tracker.TrackEvent(new DeathEvent(transform.position, collision.transform.parent.gameObject.name));


        }
    }
}
