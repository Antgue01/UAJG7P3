using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicLight2D;

public class TipoEnemigo : MonoBehaviour {
    public DynamicLight luz;
    public Disfraz tipo;
    //SpriteRenderer rend;

    private void Start()
    {
        //rend = GetComponent<SpriteRenderer>();
        
        switch (tipo)
        {
            case Disfraz.artista:
                //rend.color = Color.red;
                luz.LightColor = Color.red;
                break;
            case Disfraz.programador:
                //rend.color = Color.cyan;
                luz.LightColor = Color.cyan;
                break;
            case Disfraz.diseñador:
                //rend.color = Color.green;
                luz.LightColor = Color.green;
                break;
            case Disfraz.personal:
                //rend.color = Color.gray;
                luz.LightColor = Color.gray;
                break;
            case Disfraz.lead:
                //rend.color = Color.yellow;
                luz.LightColor = Color.yellow;
                GameManager.instance.AvisoLead(this.gameObject.GetComponent<MoveEnemy>());
                break;
            case Disfraz.guardia:
                //rend.color = Color.gray;
                luz.LightColor = Color.blue;
                break;
        }
    }

    public Disfraz TipoDeEnemigo()
    {
        return tipo;
    }
}
