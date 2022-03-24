using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemaConversacion : MonoBehaviour
{

    public string[] chat;
    public float segundosEntreChat, cargaChat;
    bool hablando = false, hablado = false;
    int dialogo = 0;

    public Transform enemigo1, enemigo2;
    public GameObject bocadillo;

    GameObject bocadillo1, bocadillo2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Player") && (hablado == false))
        {
            Debug.Log("colision");
            hablando = true;
            
        }
    }

    private void Update()
    {
        if (hablando)
        {
            cargaChat += 1 * Time.deltaTime;
        }
        //Realizar conversacion    
        if (cargaChat >= segundosEntreChat)
        {
            cargaChat = 0;
            //
            Debug.Log(chat[dialogo]);
            if (dialogo == 0)
            {
                enemigo1.SetPositionAndRotation(new Vector3(enemigo1.position.x, enemigo1.position.y +5, enemigo1.position.z), Quaternion.Euler(0,0,0));
                bocadillo1 = Instantiate(bocadillo, enemigo1);

                bocadillo1.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = chat[dialogo];
            }

            //
            dialogo++;
        }

        if (dialogo >= chat.Length)
        {
            hablado = true;
            hablando = false;

        }
    }
}

