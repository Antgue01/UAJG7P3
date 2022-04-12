using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using System.IO;
using UAJ;

public class Menuprincipal : MonoBehaviour
{
    public GameObject botonesPrincipales, niveles, ajustes, continuar, fondo, fondoNiveles,mandoImagen,mandoTexto, creditos;
    public GameObject nivel1, nivel2, nivel3;
    int Escena;

    void Start()
    {

        if (!File.Exists("partida.txt"))     //Si no existe continuar no debe salir y activo el nivel 1
        {
            Escena = 1;
            continuar.SetActive(false);
            nivel1.SetActive(true);
            StreamWriter archivo = new StreamWriter("partida.txt");  //Creo el archivo y pongo el nivel 1 pero sin activar el "continuar"
            archivo.WriteLine("1");
            archivo.WriteLine(0);
            archivo.Close();
        }
        else
        {
            StreamReader cargar = new StreamReader("partida.txt");  // si existe leo la primera linea para ver cuántos niveles tengo que mostrar
           
            Escena = int.Parse(cargar.ReadLine());
            print(Escena);
            cargar.Close();
           
        }
        
                nivel1.SetActive(true);
                nivel2.SetActive(true);
                nivel3.SetActive(true);
               
        
    }




    public void CargarNivel(int escena)
    {
        
        GameManager.instance.CargarEscena(escena);
        }
    public void SelecciónNiveles()
    {

        botonesPrincipales.SetActive(false);
        niveles.SetActive(true);
        fondo.SetActive(false);
        fondoNiveles.SetActive(true);
        mandoImagen.SetActive(false);
        mandoTexto.SetActive(false);
    }
    public void VolverMenu()
    {
        niveles.SetActive(false);
        ajustes.SetActive(false);
        mandoImagen.SetActive(true);
        mandoTexto.SetActive(true);
        creditos.SetActive(false);
        botonesPrincipales.SetActive(true);
        fondo.SetActive(true);
        fondoNiveles.SetActive(false);


    }
    public void SalirJuego()
    {
        Application.Quit();   // funcionara cuando tengamos el juego como una aplicacion de escritorio, mientras desarrollemos no
    }
    public void Opciones()
    {
        botonesPrincipales.SetActive(false);
        ajustes.SetActive(true);
        mandoImagen.SetActive(false);
        mandoTexto.SetActive(false);
    }

    public void Creditos()
    {
        fondo.SetActive(false);
        fondoNiveles.SetActive(true);
        mandoImagen.SetActive(false);
        mandoTexto.SetActive(false);
        botonesPrincipales.SetActive(false);
        creditos.SetActive(true);
    }
    public void AjustarVolumen(float volumen)
    {
        //audioMixer.SetFloat("volumen", volumen);
        Debug.Log(volumen);
        GameManager.instance.AjustarVolumen(volumen);
    }
    public void Continuar()
    {
        GameManager.instance.CargarPartida();
    }


}
