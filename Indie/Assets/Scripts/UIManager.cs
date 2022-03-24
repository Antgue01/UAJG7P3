using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject menuPerder, Pause, finDemo, recuadroInstrucciones, objetivoCumplido, Caos,avisoPuertas, panelDesc;

    public Image[] RolesPausa;
    int tarjetasAdquiridas = 2;//debería ir en el GM
    public Image artistas, artistasBrilli, diseñadores, diseñadoresBrilli, programadoresBrilli, programadores, personal, personalBrilli, barraInteraccion, caosRelleno, x;
    public Text coleccionables;
    float activacionPuertas;
    bool notifPuertas = false;
    bool visualizarcaos = false;
    bool interactuando = false;
    bool objetivoCompletado = false;
    float vicaos,viObjetivoCumplido;
    public float tiempovistacaos;

    public Image recuadroTextos;
    public Text textos;
    public Animator animadorRecuadroTextos;
    public Animator animadorTextos;
    //AJ
    public Text Interactuar, Descripcion;
    //FinAJ
    float maxInteraccion, procesoInteraccion;

    /*Pongo invisibles paneles que solo se verán en determinados momentos y aviso al GameManager de mi existencia*/
    private void Start()
    {
        recuadroInstrucciones.SetActive(true);
        GameManager.instance.AvisoUI(this.gameObject.GetComponent<UIManager>());
        //objetivoCumplido.SetActive(false);
        x.gameObject.SetActive(false);
        Detección(GameManager.instance.DisfrazJugador());
        ColeccionableRecogido(GameManager.instance.ColeccionablesTotales());
        GameManager.instance.ActualizarDisfraz();
    }

    /*Este método activa y desactiva los iconoc de aquellos npcs que te detectan t de los que no, lo hace teniendo en cuenta el disfraz que le llega como parámetro*/
    public void Detección(Disfraz disfrazActual)
    {
        if (disfrazActual == Disfraz.ninguno)
        {
            artistas.enabled = artistasBrilli.enabled = true;
            diseñadores.enabled = diseñadores.enabled = true;
            programadores.enabled = programadoresBrilli.enabled = true;
            personal.enabled = personalBrilli.enabled = true;
        }
        else if (disfrazActual == Disfraz.programador)
        {
            artistas.enabled = artistasBrilli.enabled = false;
            diseñadores.enabled =diseñadoresBrilli.enabled = true;
            programadores.enabled = programadoresBrilli.enabled = true;
            personal.enabled = personal.enabled = true;
        }
        else if (disfrazActual == Disfraz.artista)
        {
            artistas.enabled = artistasBrilli.enabled = true;
            diseñadores.enabled = diseñadoresBrilli.enabled = true;
            programadores.enabled = programadoresBrilli.enabled = false;
            personal.enabled = personalBrilli.enabled = true;
        }
        else if (disfrazActual == Disfraz.diseñador)
        {
            artistas.enabled = artistasBrilli.enabled = true;
            diseñadores.enabled = diseñadoresBrilli.enabled = true;
            programadores.enabled = programadoresBrilli.enabled = false;
            personal.enabled = personalBrilli.enabled = true;
        }
        else if (disfrazActual == Disfraz.personal)
        {
            artistas.enabled = artistasBrilli.enabled = false;
            diseñadores.enabled = diseñadoresBrilli.enabled = false;
            programadores.enabled = programadoresBrilli.enabled = false;
            personal.enabled = personalBrilli.enabled = true;
        }
    }
    private void Update()
    {
        if (Time.time>= activacionPuertas+2 &&avisoPuertas.activeInHierarchy==true)
        {
            avisoPuertas.SetActive(false);
        }
        if (interactuando) //Si se interactúa la barra aumenta lo necesario para llegar al 100% en el tiempo de interaccion maximo
        {
           
            procesoInteraccion += (1 / maxInteraccion) * Time.deltaTime;
            barraInteraccion.fillAmount = procesoInteraccion;
        }
       
        if (objetivoCompletado && viObjetivoCumplido + tiempovistacaos < Time.time)  //si es true pero ya han pasado X segundos significa a que ya no hará falta ver más el panel de "Has completado el objetivo, vuelve al ascensor"
        {
            objetivoCompletado = false;
            objetivoCumplido.SetActive(false);
        }

    }

    public void Interactuando(float tiempoInteraccion, bool interaccion)
    {
        interactuando = interaccion;
        maxInteraccion = tiempoInteraccion;
        if (!interaccion) NoInteractuando();
    }

    public void NoInteractuando() //Si no se interactua se resetea
    {
        barraInteraccion.fillAmount = 0;
        procesoInteraccion = 0;
    }

    /*Le llega la cantidad exacta de caos que hay en el nivel y muestra la barra con la longitud específica*/
    public void AumentaCaos(float valorCaos)
    {
        visualizarcaos = true;
        Caos.SetActive(true);

        caosRelleno.fillAmount = valorCaos / 100;
        vicaos = Time.time;
    }

    //AJ
    public void EsInteractuable(bool activarODesactivar, string descripción)
    {
        if (activarODesactivar)
        {
            Descripcion.text = descripción;
            panelDesc.SetActive(true);
        }
        else
        {
            Descripcion.text = " ";
            panelDesc.SetActive(false);
        }

        x.gameObject.SetActive(activarODesactivar);
    }

    public void Pausar()
    {//se paran el tiempo, se activa el menú de pausa y se activan los disfraces que tiene el jugador
        if (Pause.activeSelf) FinPausa();
        else
        {
            Time.timeScale = 0;
            Pause.SetActive(true);
            for (int i = 0; i < tarjetasAdquiridas; i++)
            {
                RolesPausa[i].gameObject.SetActive(true);
            }
        }
    }
    public void Perder()
    {
        Time.timeScale = 0;
        menuPerder.SetActive(true);
    }
    public void FinPerder()
    {
        Time.timeScale = 1;
        menuPerder.SetActive(false);
        GameManager.instance.ResetScene();
    }

    public void FinPausa()
    {
        Time.timeScale = 1;
        Debug.Log("finpausa");
        Pause.SetActive(false);
        for (int i = 0; i < tarjetasAdquiridas; i++)
        {
            RolesPausa[i].gameObject.SetActive(false);
        }
    }
    //FinAJ
    /*Muestra un panel que indica que se ha completado el objetivo del nivel, comportamiento identico al de la visualización del caos*/
    public void ObjetivoCumplido()
    {
        objetivoCompletado = true;
        viObjetivoCumplido = Time.time;
        objetivoCumplido.SetActive(true);
    }
    public void Cambiaescena(int escena) {
        GameManager.instance.ActualizarEscena(escena);
        GameManager.instance.CargarEscena(escena);
        switch (escena)
        {
            case 1:
                GameManager.instance.CambioDisfrazJugador(Disfraz.ninguno);
                break;
            case 2:
                GameManager.instance.CambioDisfrazJugador(Disfraz.programador);
                break;
            case 3:
                GameManager.instance.CambioDisfrazJugador(Disfraz.artista);
                break;
        }
        Time.timeScale = 1;
    }
    public void FinDemo()
    {
        finDemo.SetActive(true);
        Time.timeScale = 0;
    }

    public void ColeccionableRecogido(int n)
    {
        coleccionables.text = "X " + n;
    }
    public void SigNivel()
    {
        GameManager.instance.SigNivel();
    }
    public void GuardarySalir()
    {
        GameManager.instance.GuardaPartida();
        Cambiaescena(0);
    }
    public void AvisoPuertas(bool activate)
    {
        avisoPuertas.SetActive(activate);
        notifPuertas = !notifPuertas;
        activacionPuertas = Time.time;
    }

    public void MostrarTexto(float delay, string text)
    {
        animadorRecuadroTextos.SetBool("Mostrar", true);
        animadorTextos.SetBool("Mostrar", true);
        textos.text = text;
        Invoke("Escondertexto", delay);
    }
    public void Escondertexto()
    {        
        animadorRecuadroTextos.SetBool("Esconder", true);
        animadorTextos.SetBool("Esconder", true);
        Invoke("PrepararParaNuevoTexto",1f);          //Para que pueda terminar la animación 

    }
    public void PrepararParaNuevoTexto()
    {
        animadorRecuadroTextos.SetBool("Esconder", false);
        animadorTextos.SetBool("Esconder", false);
        animadorRecuadroTextos.SetBool("Mostrar", false);
        animadorTextos.SetBool("Mostrar", false);
    }
}