using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    //constante por si queremos añadir más niveles
    const int TOTAL_NIVELES = 3;
    public static GameManager instance = null;
    float volumen;
    public float Caos, maxCaos;
    bool subirAscensor;
    public bool reproducirAnimacionTarjeta;
    public Disfraz disfrazActual;
    UIManager uIManager;
    AudioManager audioManager;

   public int coleccionables;
    int coleccionablesConLosQueEmpezamos;

    //Al pasar de escena se debe sumar 1 a esta variable
    int CurrentScene;

    //Discutir solución
    MoveEnemy Lead;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        disfrazActual = Disfraz.ninguno;
        volumen = 0.5f;
    }

    private void Start()
    {
        Caos = 0;
        maxCaos = 100;
        subirAscensor = false;
        reproducirAnimacionTarjeta = true;
        coleccionables = 0;
        coleccionablesConLosQueEmpezamos = 0;

    }

    void Update()
    {
        //Debug.Log(disfrazActual);
        if (Input.GetButtonDown("Pause"))
        {
            uIManager.Pausar();
        }


    }
    //Es llamado al comienzo de la escena por el AudioManager
    public void AvisoAudioManager(AudioManager audio)
    {
        audioManager = audio;
    }

    //Es llamado al principio de la escena por el UIManager para que sepamos de su existencia
    public void AvisoUI(UIManager UI)
    {
        uIManager = UI;
    }

    //Es llamado al principio de la escena por el Lead para que sepamos de su existencia
    public void AvisoLead(MoveEnemy lead)
    {
        Lead = lead;
    }

    //Llega una nueva cantidad de caos generado, guardamos el valor actual lo mandamos al UIManager para que lo muestre
    public void GenerarCaos(float nuevocaosgenerado)
    {
        
        Caos = Caos + nuevocaosgenerado;
        uIManager.AumentaCaos(Caos);
        //AJ
        if (Caos >= maxCaos)
        {
            Lead.enabled = true;
           
        }
    }

    //Avisa al gamemanager del estado de la interacción
    public void Interactuando(float tiempoInteraccion, bool interaccion)
    {
        uIManager.Interactuando(tiempoInteraccion, interaccion);
    }

    //Le llegá el nuevo disfraz del jugador, se lo guarda y avisa al Uimanager para que cambie los iconos
    public void CambioDisfrazJugador(Disfraz jugador)
    {
        Debug.Log("llega " + jugador);
        disfrazActual = jugador;
       

        uIManager.Detección(disfrazActual);
    }
    //Muestra el trazo, resalta la palabra interactuar y muestra una descripción de lo que hace el objeto
    public void EsInteractuable(bool activarODesactivar, string descripción)
    {
        uIManager.EsInteractuable(activarODesactivar, descripción);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(CurrentScene);
        Caos = 0;
        coleccionables = coleccionablesConLosQueEmpezamos;
        subirAscensor = false;
        switch (CurrentScene)
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
    }

    public Disfraz DisfrazJugador()   //Le da al jugador su disfraz actual. Útil para cuando cambias de escena y el jugador ya tenia un disfraz, el GameManager lo guarda entre escenas
    {
        return disfrazActual;
    }

    public float CaosActual()
    {
        return Caos;
    }

    public void ReproducirSonido(string nombreSonido)
    {
        audioManager.Play(nombreSonido);
    }

    /*Hay que avisar al UI de que muestre el panel de objetivo cumplido*/
    public void ObjetivoCumplido()
    {
        uIManager.ObjetivoCumplido();
    }

    public void Perder()
    {
        Debug.Log("Perdiste");
        uIManager.Perder();
    }

    public void CargarEscena(int escenaBuild)
    {   SceneManager.LoadScene(escenaBuild);
        CurrentScene = escenaBuild;
    
        subirAscensor = false;
        reproducirAnimacionTarjeta = true;
       

        Debug.Log("escena: " + CurrentScene);


        Caos = 0;



    }
    public void ActualizarDisfraz()
    {
        Debug.Log("original: " + disfrazActual);


        switch (CurrentScene)
        {
            case 1:
                GameManager.instance.CambioDisfrazJugador(Disfraz.ninguno);

                break;
            case 2:
                GameManager.instance.CambioDisfrazJugador(Disfraz.programador);
                break;
            case 3:
                GameManager.instance.CambioDisfrazJugador(Disfraz.artista);
                Debug.Log(disfrazActual);
                break;
        }
        Debug.Log("nuevo: " + disfrazActual);
    }
    public void ActualizarEscena(int escena)
    {
        CurrentScene = escena;
        Caos = 0;
    }
    public void SubirPlanta()
    {
        subirAscensor = true;
    }
    public bool HasGanado()
    {
        return subirAscensor;
    }
    public void MenuGanar()
    {
        ReproducirSonido("Ascensor");
        subirAscensor = false;
    }
    public void AjustarVolumen(float vol)
    {
        volumen = vol;
    }
    public float DameVolumen()
    {
        return volumen;
    }

    public void ColeccionableRecogido()
    {
        coleccionables++;
        uIManager.ColeccionableRecogido(coleccionables);
    }
    public void SigNivel()
    {
        coleccionablesConLosQueEmpezamos = coleccionables;
        Debug.Log("original" + CurrentScene);
        CurrentScene++;
        Debug.Log("nueva" + CurrentScene);
        ActualizarDisfraz();

        CargarEscena(CurrentScene);
    }
    public void SaltarEscena(int sig)
    {
        coleccionablesConLosQueEmpezamos = coleccionables;
        CurrentScene++;
        CargarEscena(sig);
        Caos = 0;

    }
    public void ReproducirPitchAleatorio(string sonido)
    {

        audioManager.PlayRandomPitch(sonido);
    }
    public void GuardaPartida()
    {
        //guardamos la escena actual
        
        StreamWriter guardado = new StreamWriter("partida.txt");
        guardado.WriteLine(CurrentScene);

        guardado.WriteLine(coleccionablesConLosQueEmpezamos);

        guardado.Close();
    }
    public void CargarPartida()
    {
        StreamReader cargar = new StreamReader("partida.txt");
        //Leemos la escena que debemos cargar
        CurrentScene = int.Parse(cargar.ReadLine());
        coleccionables = coleccionablesConLosQueEmpezamos = int.Parse(cargar.ReadLine());
        cargar.Close();
        CargarEscena(CurrentScene);
        //asignamos el disfraz por defecto al inicio de cada escena
        switch (CurrentScene)
        {
            case 1:
                CambioDisfrazJugador(Disfraz.ninguno);
                break;
            case 2:
                CambioDisfrazJugador(Disfraz.programador);
                break;
            case 3:
                CambioDisfrazJugador(Disfraz.artista);
                break;
        }
    }

    public int ColeccionablesTotales()
    {
        return coleccionables;
    }
    public void AvisoPuertas()
    {
        if (!subirAscensor)
        {
            uIManager.AvisoPuertas(true);
        }
    }
    public bool ReproducirAnimacionPrincipio()
    {
        if (reproducirAnimacionTarjeta)
        {
            reproducirAnimacionTarjeta = false;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void MostrarTextoEnPantalla(float delay, string texto)
    {
        uIManager.MostrarTexto(delay, texto);
    }
}
