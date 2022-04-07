using System;
using System.IO;
using System.Collections.Generic;


/// <summary>
/// Interfaz que determina los metodos que toda clase que determine los eventos que vamos a analizar durante la ejecucion
/// </summary>
public interface ITrackerAsset
{
    public bool init(string configFilePath);
    public bool accept(/*TrackerEvent event*/ string tipo);
}

/// <summary>
/// Clase destinada a determinar cuales seran los eventos que se van a analizar en unasesion y cuales van a ser ignorados 
/// </summary>
public class ProgressionTracker : ITrackerAsset
{
    public ProgressionTracker()
    {
    }

    /// <summary>
    /// Metodo que realiza la inicializacion de la clase
    /// </summary>
    /// <returns>Bool que indica si la inicializacion se ha hecho con exito o no</returns>
    public bool init(string configFilePath)
    {
        trackedEvents = new Dictionary<string, bool>();
        try
        {
            //Leemos todas las lineas del fichero y por cada una de ellas determinamos un nuevo tipo de evento a analizar durante esta ejecucion
            string[] tiposEventos = File.ReadAllLines(configFilePath);
            foreach (string tipo in tiposEventos)
                trackedEvents.Add(tipo, true);
        }

        //En caso de que haya habido algun problema a la hora de leer indicamos que la inicializacion no ha sido posible
        catch { return false; }

        //Si no es el caso nos hemos inicializado correctamente
        return true;
    }


    /// <summary>
    /// Metodo que recibe un evento y determina si durante esta ejecucion estamos interesados en tratar esta clase de evento que nos llega
    /// </summary>
    /// <returns> bool que determina si nos interesa analizar el tipo de evento que nos llega </returns>
    public bool accept(/*TrackerEvent event*/ string tipo)
    {
        string clave = tipo;
        //clave = obtener clave del trackerEvent o de alguna forma
        return trackedEvents.ContainsKey(clave) && trackedEvents[clave];
    }


    private Dictionary<string, bool> trackedEvents;
}