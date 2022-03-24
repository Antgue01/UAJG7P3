using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{

    public int velocidadEstandar, velocidadPersecucion;
    public Transform[] puntosPatrulla;
    public GameObject playerGO, exclamationPrefab;
    public bool esGuardia;
    public bool recorridoGuardiaRealizado;
    GameObject exclamationSprite;
    int i;
    Rigidbody2D rb;
    Vector2 direccion, direccionInicial;
    RaycastHit2D ray;
    public Estados estado;
    Animator anim;
    public bool fueraZona;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        estado = Estados.volviendo;
        recorridoGuardiaRealizado = false;
        direccionInicial = transform.right;
        i = 0;
        Physics2D.queriesStartInColliders = false;
    }

    void Update()
    {

        if (puntosPatrulla.Length > 0)
        {
            switch (estado)     //Maquina de estados
            {
                case Estados.patrulla:
                    EstadoPatrulla();
                    break;
                case Estados.persecucion:
                    if (!fueraZona) EstadoPersecucion();
                    else EstadoVolviendo();
                    anim.SetBool("IsMoving", true);
                    break;
                case Estados.volviendo:
                    EstadoVolviendo();
                    anim.SetBool("IsMoving", true);
                    break;
                case Estados.finTrayecto:
                    anim.SetBool("IsMoving", false);
                    break;
            }
        }
        if (Mathf.Abs(rb.velocity.x) > 0.01 || Mathf.Abs(rb.velocity.y) > 0.01)
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }
        void EstadoPatrulla()
        {
            if (puntosPatrulla.Length > 1)      //Si el NPC va a seguir un patron
            {
                if (puntosPatrulla[i % puntosPatrulla.Length] != null &&
                    Vector2.Distance(puntosPatrulla[i % puntosPatrulla.Length].position, transform.position) > 0.4f)     //Movimiento hasta llegar al siguiente punto
                {
                    if (esGuardia && i >= puntosPatrulla.Length && !recorridoGuardiaRealizado)
                    {
                        InvierteCamino();
                        i = 0;
                        recorridoGuardiaRealizado = true;
                        anim.SetBool("IsMoving", false);

                    }
                    if (esGuardia && i >= puntosPatrulla.Length && recorridoGuardiaRealizado)
                    {
                        recorridoGuardiaRealizado = false;
                        i = 0;
                        gameObject.SetActive(false);
                        anim.SetBool("IsMoving", true);

                    }
                    if (esGuardia && !this.gameObject.activeSelf)
                    {
                        recorridoGuardiaRealizado = false;
                    }
                    direccion = new Vector2(puntosPatrulla[i % puntosPatrulla.Length].position.x - transform.position.x, puntosPatrulla[i % puntosPatrulla.Length].position.y - transform.position.y).normalized;
                    transform.right = direccion;
                    rb.velocity = transform.right * velocidadEstandar;
                }
                else i++;
            }
            else
            {
                anim.SetBool("IsMoving", false);
                transform.right = direccionInicial;     //Se queda mirando a la posicion inicial
                rb.velocity = Vector2.zero;
            }

            rb.angularVelocity = 0;
        }

        void EstadoPersecucion()
        {
            ray = Physics2D.Raycast(transform.position, playerGO.transform.position - transform.position, Vector2.Distance(playerGO.transform.position, transform.position));  //Raycast
            Debug.DrawRay(transform.position, playerGO.transform.position - transform.position, Color.red);    //Debug del raycast

            if (ray.transform.tag == "Player")      //Si el tag es player le persigue
            {
                if (exclamationSprite == null) exclamationSprite = Instantiate<GameObject>(exclamationPrefab);
                direccion = new Vector2(playerGO.transform.position.x - transform.position.x, playerGO.transform.position.y - transform.position.y).normalized;
                transform.right = direccion;
                rb.velocity = transform.right * velocidadPersecucion;
            }
            else CambiaEstado(Estados.volviendo);

            if (exclamationSprite != null) exclamationSprite.transform.position = transform.position + new Vector3(0f, 2f, 0f);

            rb.angularVelocity = 0;
        }

        void EstadoVolviendo()
        {
            ray = Physics2D.Raycast(transform.position, puntosPatrulla[i % puntosPatrulla.Length].transform.position - transform.position, Vector2.Distance(puntosPatrulla[i % puntosPatrulla.Length].transform.position, transform.position)); //Raycast
            Debug.DrawRay(transform.position, puntosPatrulla[i % puntosPatrulla.Length].transform.position - transform.position, Color.blue);       //Debug del raycast

            if (ray.transform != null && ray.transform.tag != "Player") i++;         //Cambiar punto de destino si se interpone un obstaculo en medio

            if (Vector2.Distance(puntosPatrulla[i % puntosPatrulla.Length].position, transform.position) > 0.4f)     //Movimiento del NPC hasta que llega al punto
            {
                direccion = new Vector2(puntosPatrulla[i % puntosPatrulla.Length].transform.position.x - transform.position.x, puntosPatrulla[i % puntosPatrulla.Length].transform.position.y - transform.position.y).normalized;
                transform.right = direccion;
                rb.velocity = transform.right * velocidadEstandar;
            }
            else CambiaEstado(Estados.patrulla);

            rb.angularVelocity = 0;
        }

        public void CambiaEstado(Estados nuevoEstado)
        {
            estado = nuevoEstado;
        }

        public void PlayerPerdido()
        {
            CambiaEstado(Estados.volviendo);
            if (exclamationSprite != null) Destroy(exclamationSprite);
        }

        public void AbandonoZona()
        {
            CambiaEstado(Estados.volviendo);
            if (exclamationSprite != null) Destroy(exclamationSprite);
            Debug.Log("zona");
        }

        public void CambioPatron(Transform[] puntosnuevos)
        {
            i = 0;
            puntosPatrulla = puntosnuevos;
        }

        public bool Hellegado()
        {
            if (puntosPatrulla[puntosPatrulla.Length - 1] != null && Vector2.Distance(transform.position, new Vector2(puntosPatrulla[puntosPatrulla.Length - 1].position.x, puntosPatrulla[puntosPatrulla.Length - 1].position.y)) < 1f)
            {
                return true;
            }
            else return false;
        }

        public Estados Estado()
        {
            return estado;
        }

        void InvierteCamino()
        {
            Transform aux;
            for (int i = 0; i < puntosPatrulla.Length / 2; i++)
            {
                aux = puntosPatrulla[i];
                puntosPatrulla[i] = puntosPatrulla[puntosPatrulla.Length - i - 1];
                puntosPatrulla[puntosPatrulla.Length - i - 1] = aux;
            }
        }
    }
