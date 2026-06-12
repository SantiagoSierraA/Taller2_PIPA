using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [Header("Referencias")]
    public Transform jugador;

    [Header("UI")]
    public TMP_Text textoPuntaje;
    public TMP_Text textoMonedas;

    private int monedas = 0;
    private int puntaje = 0; 

    void Awake()
    {
        instancia = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
        actualizarUI();
    }

    void Update()
    {
        if (jugador != null)
        {
            int nuevoPuntaje = Mathf.Max(0, Mathf.FloorToInt(jugador.position.z));
            if (nuevoPuntaje != puntaje)
            {
                puntaje = nuevoPuntaje;
                actualizarUI();
            }
        }
    }

    // Suma una moneda al contador y refresca la UI
    public void sumarMoneda()
    {
        monedas++;
        actualizarUI();
    }

    // Refresca los textos de puntaje y monedas en pantalla (con ceros a la izquierda)
    void actualizarUI()
    {
        if (textoPuntaje != null) textoPuntaje.text = "PUNTUACIÓN: " + puntaje.ToString("D8");
        if (textoMonedas != null) textoMonedas.text = "MONEDAS: " + monedas.ToString("D2");
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