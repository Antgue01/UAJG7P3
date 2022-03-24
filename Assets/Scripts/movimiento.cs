using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento : MonoBehaviour
{
    public float speed;
    bool move,paso;
    float horizontal, vertical;
    Vector2 mov, direc;
    Rigidbody2D rb;
    public float cadenciaPasos;
    Animator anim;
   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        move = true;
      //  GameManager.instance.ReproducirSonido("Ascensor");
        paso = false;
        
    }

    void Update()
    {       
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        if (!paso&&( Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f))
        {
            paso = true;
            GameManager.instance.ReproducirPitchAleatorio("Pisada");
            Invoke("CambioPaso", cadenciaPasos);
        }        
    }

    void FixedUpdate()
    {
        if (move)
        {
            Move();           
        }
        else
        {
            StopMovement();
        }

    }

    //Mueve al jugador
    void Move()
    {

        if (mov != new Vector2(0, 0))
        {    // Se guarda la dirección anterior en direc si esta es distinta de 0,0
            direc = mov;
            anim.SetBool("IsMoving", true);

        }

        mov = new Vector2(horizontal, vertical) * speed;
        mov = new Vector2(horizontal, vertical) * speed;

        if (mov == new Vector2(0, 0))
        {     // Si está quieto pone al jugador mirando a la dirección direc en lugar de a mov
            transform.right = direc;
            anim.SetBool("IsMoving", false);
        }

        else
            transform.right = mov;

        rb.velocity = mov;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);

    }

    //Para al jugador
    void StopMovement()
    {
        rb.velocity = new Vector2(0,0);
    }

    //Determina si el jugador debe moverse o no
    public void ShouldMove(bool movement)
    {
        move = movement;
    }
    void CambioPaso()
    {
        paso = !paso;
    }
}