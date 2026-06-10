using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following_Camera : MonoBehaviour
{
    public Transform jugador; 
    public float distanciaAtras = 7f;
    public float altura = 5f;
    private float xFija = 0f;
    public float suavizado = 5f;    

    void LateUpdate()
    {
        // Solo se mueve en Z
        Vector3 objetivo = new Vector3(xFija, altura, jugador.position.z - distanciaAtras);
        transform.position = Vector3.Lerp(transform.position, objetivo, suavizado * Time.deltaTime);
    }
}