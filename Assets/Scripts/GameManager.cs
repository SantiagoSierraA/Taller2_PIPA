using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    void Awake()
    {
        instancia = this;
    }

    // Termina la partida cuando el jugador muere
    public void GameOver()
    {
        ReiniciarPartida();
    }

    // Vuelve a cargar la escena actual (reinicia la partida)
    public void ReiniciarPartida()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}