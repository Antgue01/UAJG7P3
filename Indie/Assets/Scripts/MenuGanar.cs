using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuGanar : MonoBehaviour {

    public Text ContadorColeccionables,resultados;

    private void Start()
    {
        int coleccionables = GameManager.instance.ColeccionablesTotales();
        ContadorColeccionables.text = "" + coleccionables;
        if (coleccionables>=13)
        {
            resultados.text = @"Wendy consiguió suficientes 
coleccionables como para publicar
su juego";
        }
        else
        {
            resultados.text = @"Wendy no consiguio suficientes 
coleccionables como para 
publicar su juego";
        }
    }

    public void VolverAlMenu()
    {
        GameManager.instance.CargarEscena(0);
    }
}
