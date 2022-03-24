using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour {

    public Transform player;
    public float limiteX, limiteY;      // Se asigna desde el inspector el límite de movimiento de
    float offsetZ, movX, movY;          // la cámara para que no muestre más allá del borde del nivel

    void Start () {
        offsetZ = transform.position.z;
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, offsetZ);
    }

    void LateUpdate () {
        if (player != null)
        {
            // Para que no se salga en horizontal
            if (Mathf.Abs(player.position.x) > Mathf.Abs(limiteX))
            {
                if(player.position.x>0)
                    movX = limiteX;
                else
                    movX = -limiteX;

                transform.position = new Vector3(movX, movY, offsetZ);
            }  
            else
            {
                movX = player.position.x;
                transform.position = new Vector3(movX, movY, offsetZ);
            }

            // Para que no se salga en vertical
            if (player != null && Mathf.Abs(player.position.y) > Mathf.Abs(limiteY))
            {
                if (player.position.y > 0)
                    movY = limiteY;
                else
                    movY = -limiteY;

                transform.position = new Vector3(movX, movY, offsetZ);
            }  
            else
            {
                movY = player.position.y;
                transform.position = new Vector3(movX, movY, offsetZ);
            }
        }
    }
}
