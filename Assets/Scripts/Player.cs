using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadAdelante = 8f; // velocidad de avance forzado (eje Z)
    public float velocidadLateral = 6f; // velocidad al moverse con A/D (eje X)
    public float limiteIzquierda = -4f;
    public float limiteDerecha = 4f;

    [Header("Salto")]
    public float fuerzaSalto = 8f;
    private bool enSuelo = true;

    private float movX;  
    private Rigidbody rb;

    public Transform puntoDisparo;
    public GameObject prefabBala;

    private float tiempoUltimoDisparo = 0f; 
    public float cooldownDisparo = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // A = -1, D = 1
        movX = Input.GetAxis("Horizontal");
        saltar();
        Disparo();
    }

    void FixedUpdate()
    {
        moverPlayer();
    }

    // Avance forzado en Z + movimiento lateral con A/D
    void moverPlayer()
    {
        float vx = movX * velocidadLateral;

        // si esta en un limite e intenta salir, no se mueve en X
        if ((rb.position.x <= limiteIzquierda && vx < 0) ||
            (rb.position.x >= limiteDerecha && vx > 0))
            vx = 0f;

        rb.velocity = new Vector3(vx, rb.velocity.y, velocidadAdelante);
    }

    // Salta hacia arriba si el jugador está tocando el suelo
    void saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            rb.velocity = new Vector3(rb.velocity.x, fuerzaSalto, rb.velocity.z);
            enSuelo = false;
        }
    }

    // Detecta el contacto con el suelo
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Translator"))
            enSuelo = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Translator"))
            enSuelo = false;
    }

    void Disparo()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time >= tiempoUltimoDisparo + cooldownDisparo)
        {
            Instantiate(prefabBala, puntoDisparo.position, puntoDisparo.rotation);
            tiempoUltimoDisparo = Time.time;
        }
    }
}