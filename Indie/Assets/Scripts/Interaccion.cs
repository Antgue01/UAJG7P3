using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UAJ;

public class Interaccion : MonoBehaviour
{

    public GameObject trazo;
    bool rangoInteraccion, interactuable;
    float cargaInteraccion = 0;
    public float tiempoInteraccion;
    public bool sePuedeInteractuarMasDeUnaVez;
    public string descripcion;
    public GameObject interaccion;
    movimiento player;
    [SerializeField] private string id;
    

    private void Start()
    {
        rangoInteraccion = false;
        interactuable = true;
        
    }
    //Cuando entra en el trigger el jugador el objeto se convierte en interactuable
    private void OnTriggerEnter2D(Collider2D other)
    {
        movimiento mov = other.gameObject.GetComponent<movimiento>();
        if (mov != null)
        {
            if (interactuable && interaccion.GetComponent<Coleccionable>())
            {
                trazo.GetComponent<SpriteRenderer>().color = Color.blue;
                player = mov;
                rangoInteraccion = true;
                Debug.Log("A rango");
                //Resetea la interacción
                cargaInteraccion = 0;
            }
            else
                trazo.GetComponent<SpriteRenderer>().color = Color.yellow;
            player = mov;
            rangoInteraccion = true;
            Debug.Log("A rango");
            //Resetea la interacción
            cargaInteraccion = 0;

        }
    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        //Se comprueba que es un jugador
        if (other.gameObject.GetComponent<movimiento>() != null)
        {
            //Ya no está a rango
            rangoInteraccion = false;
            //Se quita el brillo del objeto
            GameObject trazo = transform.GetChild(0).gameObject;
            trazo.SetActive(false);
            //Quita el texto de interacción
            GameManager.instance.EsInteractuable(false, "");
        }
    }

    private void Update()
    {

        if (interactuable && rangoInteraccion)
        {
            //Pone brillo al objeto
            GameObject trazo = transform.GetChild(0).gameObject;
            trazo.SetActive(true);
            //Aparece el texto descriptivo de la interacción
            GameManager.instance.EsInteractuable(true, descripcion);

            //Si se está interactuando se carga la interacción
            if (Input.GetButton("Interaccion"))
            {
                //Está interactuando
                player.ShouldMove(false);
                //Se avisa al gamemanager de la interacción
                GameManager.instance.Interactuando(tiempoInteraccion, true);
                cargaInteraccion += 1 * Time.deltaTime;

            }
            else if (Input.GetButtonUp("Interaccion"))
            {
                //Ya no está interactuando
                player.ShouldMove(true);
                
                //Se avisa al gamemanager de que no hay interacción
                GameManager.instance.Interactuando(tiempoInteraccion, false);
                //Resetea la carga
                cargaInteraccion = 0;
            }


            if (cargaInteraccion >= tiempoInteraccion)
            {
                //Realiza la acción
                player.ShouldMove(true);
                Accion();
                //Termina la interacción en el gamemanager
                GameManager.instance.Interactuando(tiempoInteraccion, false);
            }
        }

    }

    private void Accion()
    {
        Debug.Log("Interactuado");
        Tracker.TrackEvent(new UseObjectEvent(id));

        interaccion.SetActive(true);
        //Resetea la carga
        cargaInteraccion = 0;
        //El objeto deja de ser interactuable (opcional)
        if (!sePuedeInteractuarMasDeUnaVez)
        {
            interactuable = false;

            //Quita el brillo
            GameObject trazo = transform.GetChild(0).gameObject;
            trazo.SetActive(false);
            //Quita el texto
            GameManager.instance.EsInteractuable(false, "");
        }
    }

    public void MarcaTrazo(bool activo)
    {
        GameObject trazo = transform.GetChild(0).gameObject;
        trazo.SetActive(activo);
        if (activo) trazo.GetComponent<SpriteRenderer>().color = Color.red;
        else trazo.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    public bool EnRango()
    {
        return rangoInteraccion;
    }

    public bool Interactuable()
    {
        return interactuable;
    }
}
