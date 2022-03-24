using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Menu : MonoBehaviour
{

    public EventSystem eventSystem;
    public GameObject selectedObject;
    
    bool buttonSelected;



    void Update()
    {
        //si se acciona el eje vertical y no hay ningún botón seleccionado selecciona el primer botón 
        if (Input.GetAxisRaw("Vertical") != 0 && !buttonSelected)
        {
           
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;

        }
        else if(Input.GetAxisRaw("Horizontal") != 0 && !buttonSelected)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;

        }
    }
    //al desactivarse el botón está de nuevo disponible
    void OnDisable()
    {

        buttonSelected = false;
    }

    private void OnEnable()
    {
        eventSystem.SetSelectedGameObject(null);
    }
    
}
