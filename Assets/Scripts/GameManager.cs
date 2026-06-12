using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [Header("Referencias")]
    public Transform jugador;

    [Header("UI - HUD")]
    public TMP_Text textoPuntaje;
    public TMP_Text textoMonedas;

    [Header("UI - Paneles")]
    public GameObject panelInicio;
    public GameObject panelGameOver;
    public TMP_Text textoFinalPuntaje;
    public TMP_Text textoFinalMonedas;

    private int monedas = 0;
    private int puntaje = 0;
    private bool juegoActivo = false;

    public AudioSource musicaFondo;
    public AudioSource musicaDerrota;

    void Awake()
    {
        instancia = this;
    }

    void Start()
    {
        Time.timeScale = 0f; // juego pausado hasta presionar Play

        if (panelInicio != null)   panelInicio.SetActive(true);
        if (panelGameOver != null) panelGameOver.SetActive(false);

        actualizarUI();
    }

    void Update()
    {
        if (!juegoActivo) return;

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

    // Boton PLAY del panel de inicio
    public void IniciarPartida()
    {
        juegoActivo = true;
        Time.timeScale = 1f;

        if (panelInicio != null) panelInicio.SetActive(false);
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
        if (!juegoActivo) return;
        juegoActivo = false;
        Time.timeScale = 0f;

        if (panelGameOver != null)
        {
            if (musicaFondo != null) musicaFondo.Stop();
            if (musicaDerrota != null) musicaDerrota.Play();
            panelGameOver.SetActive(true);

            if (textoFinalPuntaje != null)
                textoFinalPuntaje.text = "PUNTUACIÓN: " + puntaje.ToString("D8");
            if (textoFinalMonedas != null)
                textoFinalMonedas.text = "MONEDAS: " + monedas.ToString("D2");
        }
    }

    // Vuelve a cargar la escena actual (reinicia la partida)
    public void ReiniciarPartida()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}