using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip sonidoRecoger;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        GameManager.instancia.sumarMoneda();

        if (sonidoRecoger != null)
            AudioSource.PlayClipAtPoint(sonidoRecoger, transform.position);

        Destroy(gameObject);
    }
}