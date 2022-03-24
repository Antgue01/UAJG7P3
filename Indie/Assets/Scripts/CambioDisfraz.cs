using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class CambioDisfraz : MonoBehaviour {

    public Disfraz miDisfraz;
    Disfraz disfrazAnterior;
    public GameObject artistaBoton, programadorBoton, personalBoton, diseñadorBoton;
    int disfraz;
    Animator anim;
    GameObject poolDisfraz;
    //SpriteRenderer rend;
    

    void Start() {
        //inicia el juego sin disfraz, asigna pooldisfraz (gameObject vacío)
        miDisfraz = GameManager.instance.DisfrazJugador();
        anim = GetComponent<Animator>();
        poolDisfraz = GameObject.Find("PoolDisfraz");
        //rend = GetComponent<SpriteRenderer>();
        DisfrazarmeInicioEscena();

    }

    //Al principio de la escena o al reiniciar el nivel el jugador pregunta por el disfraz a gamemanager y se lo pone (para actualizar la skin)
    void DisfrazarmeInicioEscena()
    {
        Disfraz nuevoDisfraz = GameManager.instance.DisfrazJugador() ;
        switch (nuevoDisfraz)
        {
            case Disfraz.ninguno:
                //rend.color = Color.white;
                anim.SetInteger("Disfraz", 0);
                break;
            case Disfraz.artista:
                //rend.color = Color.red;
                anim.SetInteger("Disfraz", 1);

                break;
            case Disfraz.diseñador:
                //rend.color = Color.green;
                anim.SetInteger("Disfraz", 2);

                break;
            case Disfraz.personal:
                //rend.color = Color.gray;
                anim.SetInteger("Disfraz", 3);

                break;
            case Disfraz.programador:
                //rend.color = Color.cyan;
                anim.SetInteger("Disfraz", 4);

                break;
          
        }
    }

    public void MeCambio(Disfraz nuevoDisfraz)
    {
        //el disfraz que llevaba puesto pasa a ser el anterior, el del suelo pasa a ser el actual(ahora lo lleva puesto)
        disfrazAnterior = miDisfraz;
        miDisfraz = nuevoDisfraz;
        GameManager.instance.CambioDisfrazJugador(nuevoDisfraz);
        //si llevaba anteriormente algún disfraz, este se instancia al lado de player


        switch (nuevoDisfraz)
        {
            case Disfraz.ninguno:
                //rend.color = Color.white;
                anim.SetInteger("Disfraz", 0);
                Debug.Log("ni");
                break;
            case Disfraz.artista:
                //rend.color = Color.red;
                anim.SetInteger("Disfraz", 1);
                anim.SetInteger("Disfraz", 1);
                Debug.Log("art");


                break;
            case Disfraz.diseñador:
                //rend.color = Color.green;
                anim.SetInteger("Disfraz", 2);
                Debug.Log("dis");


                break;
            case Disfraz.personal:
                //rend.color = Color.gray;
                anim.SetInteger("Disfraz", 3);
                Debug.Log("per");


                break;
            case Disfraz.programador:
                //rend.color = Color.cyan;
                anim.SetInteger("Disfraz", 4);
                Debug.Log("pro");


                break;


        }
        Debug.Log("aaaa " + anim.GetInteger("Disfraz"));

        switch (disfrazAnterior)
        {
            case Disfraz.artista:
                Instantiate<GameObject>(artistaBoton, new Vector2(transform.position.x, transform.position.y), transform.rotation, poolDisfraz.transform);
                break;
            case Disfraz.diseñador:
                Instantiate<GameObject>(diseñadorBoton, new Vector2(transform.position.x, transform.position.y), transform.rotation, poolDisfraz.transform);
                break;
            case Disfraz.personal:
                Instantiate<GameObject>(personalBoton, new Vector2(transform.position.x, transform.position.y), transform.rotation, poolDisfraz.transform);
                break;
            case Disfraz.programador:
                Instantiate<GameObject>(programadorBoton, new Vector2(transform.position.x, transform.position.y), transform.rotation, poolDisfraz.transform);
                break;

            case Disfraz.ninguno:
                break;

            }
        Debug.Log("cccc " + anim.GetInteger("Disfraz"));


    }


}

