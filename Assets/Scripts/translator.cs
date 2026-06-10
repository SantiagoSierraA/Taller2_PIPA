using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("este escrpt esta en: " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Jugador salió del trigger, reciclando plataforma..." + gameObject.name);
            PlataformManager manager = FindObjectOfType<PlataformManager>();
            manager.ReciclarPlataforma(gameObject);
        }
    }
}
