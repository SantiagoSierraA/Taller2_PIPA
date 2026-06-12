using System.Collections.Generic;
using UnityEngine;

public class GeneradorContenido : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] zombies;
    public GameObject[] vehiculos;
    public GameObject monedaPrefab;

    [Header("Posición")]
    public float[] carriles = { -3f, 0f, 3f };  // X de los 3 carriles (igual que en Player)
    public float alturaSpawn = 0.5f;
    public float distanciaSegura = 25f;
    [Header("Obstáculos")]
    [Range(0f, 1f)] public float probabilidadVacio = 0.4f;   // chance de que NO salga obstaculo
    [Range(0f, 1f)] public float probabilidadZombie = 0.65f; // chance de zombie 

    [Header("Monedas")]
    [Range(0f, 1f)] public float probabilidadMonedas = 0.5f; // chance de que salga una fila de monedas
    public int cantidadMonedas = 5;
    public float separacionMonedas = 2f;
    public float alturaMoneda = 0.7f;

    private List<GameObject> generados = new List<GameObject>();  // todo lo que se generó (para limpiar al reciclar)
    private Transform jugador;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) jugador = p.transform;

        Generar();
    }

    // Limpia lo generado antes y crea contenido nuevo (obstáculo y/o fila de monedas)
    public void Generar()
    {
        // borra lo anterior (obstaculo + monedas), sin tocar la decoración de la plataforma
        foreach (GameObject g in generados)
            if (g != null) Destroy(g);
        generados.Clear();

        // zona segura: si esta muy cerca del jugador, no genera nada
        if (jugador != null && transform.position.z - jugador.position.z < distanciaSegura)
            return;

        int carrilObstaculo = -1;

        // 1) obstaculo (zombie o vehiculo)
        if (Random.value >= probabilidadVacio)
        {
            GameObject prefab = elegirPrefab();
            if (prefab != null)
            {
                carrilObstaculo = Random.Range(0, carriles.Length);
                Vector3 pos = new Vector3(carriles[carrilObstaculo], transform.position.y + alturaSpawn, transform.position.z);
                GameObject obj = Instantiate(prefab, pos, Quaternion.identity);
                obj.transform.parent = transform;
                generados.Add(obj);
            }
        }

        // 2) fila de monedas (en un carril distinto al del obstáculo)
        if (monedaPrefab != null && Random.value < probabilidadMonedas)
            generarFilaMonedas(carrilObstaculo);
    }

    // Crea una fila de monedas a lo largo de la plataforma, en un carril libre
    void generarFilaMonedas(int carrilEvitar)
    {
        int carril = Random.Range(0, carriles.Length);
        if (carril == carrilEvitar)
            carril = (carril + 1) % carriles.Length;  // evita el carril del obstaculo

        float x = carriles[carril];

        // centra la fila respecto al origen de la plataforma
        float zInicio = transform.position.z - ((cantidadMonedas - 1) * separacionMonedas) / 2f;

        for (int i = 0; i < cantidadMonedas; i++)
        {
            float z = zInicio + i * separacionMonedas;
            Vector3 pos = new Vector3(x, transform.position.y + alturaMoneda, z);
            GameObject m = Instantiate(monedaPrefab, pos, Quaternion.identity);
            m.transform.parent = transform;
            generados.Add(m);
        }
    }

    // Elige un prefab de obstaculo
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