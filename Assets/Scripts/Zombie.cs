using UnityEngine;

public class Zombie : ElementoDestruible
{
    public float velocidad = 3f;   // velocidad con la que avanza hacia la pantalla
    public Animator animator;

    [Header("Choque con autos")]
    public float distanciaDeteccion = 1f; 
    public float alturaRayo = 0.5f; // Para colision con vehiculo

    void Start()
    {
        // gira 180° para mirar hacia donde camina
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

    void Update()
    {
        // se mueve hacia -Z (hacia el jugador)
        transform.Translate(Vector3.back * velocidad * Time.deltaTime, Space.World);

        revisarChoqueConAuto();
    }

    // Lanza un rayo corto hacia adelante; si hay un obstáculo (auto), el zombie muere
    void revisarChoqueConAuto()
    {
        // origen un poco adelante y a la altura del cuerpo, para no chocar con su propio collider ni con el suelo
        Vector3 origen = transform.position + Vector3.up * alturaRayo + Vector3.back * 0.5f;

        if (Physics.Raycast(origen, Vector3.back, out RaycastHit hit, distanciaDeteccion))
        {
            if (hit.collider.CompareTag("Obstacle"))
                Morir();
        }
    }
}