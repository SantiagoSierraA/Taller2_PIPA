using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocidad = 30f; 
    public float tiempoDeVida = 0.8f; 

    void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        impactar(other.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        impactar(collision.gameObject);
    }

    void impactar(GameObject objeto)
    {
        if (objeto.CompareTag("Enemy") || objeto.CompareTag("Obstacle"))
        {
            ElementoDestruible elemento = objeto.GetComponent<ElementoDestruible>();
            if (elemento != null)
                elemento.Morir(); // reproduce el sonido de muerte y se destruye
            else
                Destroy(objeto);    // si el objeto no hereda de ElementoDestruible

            Destroy(gameObject);    // la bala también desaparece
        }
    }
}