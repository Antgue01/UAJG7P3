using System.Collections;
using System.Collections.Generic;
using UAJ;
using UnityEngine;


public class Perder : MonoBehaviour {

    void Start()
    {
        DeathEvent e = new DeathEvent(2);
        ServerPersistance p = new ServerPersistance("81.32.92.63:1337", new JSONSerializer());
        p.Send(e);
        p.Flush();
    }
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy") && this.enabled)       //Solo muero si me choco con un enemigo y mi script de perder está activado
        {

            GameManager.instance.Perder();
        }
    }
}
