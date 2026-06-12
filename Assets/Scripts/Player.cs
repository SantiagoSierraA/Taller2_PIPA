using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadAdelante = 8f; // avance forzado (eje Z)
    public float velocidadCambioCarril = 6f; 
    public float[] carriles = { -3f, 0f, 3f };    
    private int carrilActual = 1;

    [Header("Salto")]
    public float fuerzaSalto = 5f;
    private bool enSuelo = true;
    private Rigidbody rb;

    public Transform puntoDisparo;
    public GameObject prefabBala;

    private float tiempoUltimoDisparo = 0f; 
    public float cooldownDisparo = 0.5f;

    public AudioSource audioSource;
    public AudioClip sonidoDisparo;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // A = carril a la izquierda, D = carril a la derecha
        if (Input.GetKeyDown(KeyCode.A) && carrilActual > 0) carrilActual--;
        if (Input.GetKeyDown(KeyCode.D) && carrilActual < carriles.Length - 1) carrilActual++;
        saltar();
        Disparo();
    }

    void FixedUpdate()
    {
        moverPlayer();
    }

   // Avance forzado en Z + acercamiento suave al carril objetivo en X
    void moverPlayer()
    {
        float xObjetivo = carriles[carrilActual];
        float velocidadX = (xObjetivo - rb.position.x) * velocidadCambioCarril;
        rb.velocity = new Vector3(velocidadX, rb.velocity.y, velocidadAdelante);
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

        // muerte: si choca con un enemigo u obstáculo, termina la partida
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Obstacle"))
            GameManager.instancia.GameOver();
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
            audioSource.PlayOneShot(sonidoDisparo);
            Instantiate(prefabBala, puntoDisparo.position, puntoDisparo.rotation);
            tiempoUltimoDisparo = Time.time;
        }
    }
}