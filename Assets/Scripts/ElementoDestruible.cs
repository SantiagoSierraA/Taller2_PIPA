using UnityEngine;

public class ElementoDestruible : MonoBehaviour
{
    public AudioClip sonidoMuerte; 

    // Reproduce el sonido de muerte y destruye el objeto
    public virtual void Morir()
    {
        if (sonidoMuerte != null)
            AudioSource.PlayClipAtPoint(sonidoMuerte, transform.position);

        Destroy(gameObject);
    }
}