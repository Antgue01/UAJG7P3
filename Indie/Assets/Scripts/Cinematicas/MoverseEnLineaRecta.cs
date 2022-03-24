using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverseEnLineaRecta : MonoBehaviour
{

    Rigidbody2D rb;
    public GameObject fundido;
    public Transform punto;
    public Animator anim;
    public Animator CanvasAnim;
    public GameObject convBuena, convMala, textoFin, joshua, Wendy;
    public GameObject canvas;
    public float segundos;
    public int ColeccionablesMinimos;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("Final", segundos);
        convMala.SetActive(false);
        convBuena.SetActive(false);


    }


    void Update()
    {
        if (transform.position.x <= punto.position.x)
        {
            rb.AddForce(new Vector2(500, 0));
            anim.SetBool("IsMoving", true);
            canvas.SetActive(false);
        }
        else
        {
            anim.SetBool("IsMoving", false);
            canvas.SetActive(true);
        }

    }
    void Final()
    {

        CanvasAnim.enabled = false;
        Wendy.SetActive(false);
        Debug.Log(GameManager.instance.ColeccionablesTotales());
        if (GameManager.instance.ColeccionablesTotales() >= ColeccionablesMinimos)
        {
            joshua.SetActive(true);
            textoFin.SetActive(false);
            convBuena.SetActive(true);
            convMala.SetActive(false);

        }
        else
        {
            joshua.SetActive(true);
            textoFin.SetActive(false);
            convMala.SetActive(true);
            convBuena.SetActive(false);

        }
        Invoke("desactivaFundido", 5);
    }
void desactivaFundido()
    {
        fundido.SetActive(true);
        Invoke("PasarEscena", 1);
    }
    void PasarEscena()
    {
        GameManager.instance.SigNivel();
    }
}
