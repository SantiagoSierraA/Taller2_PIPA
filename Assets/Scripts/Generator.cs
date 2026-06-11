using UnityEngine;


public class GeneradorContenido : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] zombies;  // prefabs de zombie
    public GameObject[] vehiculos; // prefabs de vehiculos

    [Header("Posición")]
    public float[] carriles = { -3f, 0f, 3f };  // X de los 3 carriles
    public float alturaSpawn = 0.5f;
    public float distanciaSegura = 25f;  
    [Header("Probabilidades")]
    [Range(0f, 1f)] public float probabilidadVacio = 0.4f; 
    [Range(0f, 1f)] public float probabilidadZombie = 0.65f;

    private GameObject contenidoActual; // referencia al objeto generado, para borrarlo al reciclar
    private Transform jugador; // referencia al jugador, para medir la distancia segura

    // Start se ejecuta al iniciar la escena: busca al jugador y genera el contenido inicial
    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) jugador = p.transform;

        Generar();
    }

    // Borra el contenido anterior y genera uno nuevo. Se llama al inicio y al reciclar la plataforma.
    public void Generar()
    {
        // elimina el contenido anterior si todavia existe
        if (contenidoActual != null)
            Destroy(contenidoActual);

        // si la plataforma está demasiado cerca (delante) del jugador, no genera nada (zona segura)
        if (jugador != null && transform.position.z - jugador.position.z < distanciaSegura)
            return;

        if (Random.value < probabilidadVacio) return;

        // decide si aparece un zombie o un vehiculo
        GameObject prefab = elegirPrefab();
        if (prefab == null) return;

        // aparece sobre la plataforma, en un carril al azar
        float x = carriles[Random.Range(0, carriles.Length)];
        Vector3 posicion = new Vector3(x, transform.position.y + alturaSpawn, transform.position.z);

        contenidoActual = Instantiate(prefab, posicion, Quaternion.identity);
        contenidoActual.transform.parent = transform; // se cuelga de la plataforma
    }

    // Elige un prefab respetando la proporción zombie/vehiculo
    GameObject elegirPrefab()
    {
        bool quiereZombie = Random.value < probabilidadZombie;

        if (quiereZombie && zombies.Length > 0)
            return zombies[Random.Range(0, zombies.Length)];

        if (vehiculos.Length > 0)
            return vehiculos[Random.Range(0, vehiculos.Length)];

        if (zombies.Length > 0)
            return zombies[Random.Range(0, zombies.Length)];

        return null;
    }
}