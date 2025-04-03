using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Juego1 : MonoBehaviour
{
    public Button[] buttons;  // Arreglo de 25 botones en Unity
    public TMP_Text timerText; // Texto para mostrar el tiempo
    public TMP_Text scoreText; // Texto para mostrar la puntuación
    private bool[,] tabla = new bool[5, 5];
    private float tiempoRestante = 60f; // 60 segundos de juego
    private bool juegoActivo = true;
    private int puntuacion = 0; // Contador de puntuación

    private Color[,] parejasColores = new Color[6, 2] {
        { new Color(0.9f, 0.1f, 0.1f), new Color(1f, 0.2f, 0.2f) }, // Rojos
        { new Color(0.1f, 0.9f, 0.1f), new Color(0.2f, 1f, 0.2f) }, // Verdes
        { new Color(0.1f, 0.1f, 0.9f), new Color(0.2f, 0.2f, 1f) }, // Azules
        { new Color(0.9f, 0.9f, 0.1f), new Color(1f, 1f, 0.2f) }, // Amarillos
        { new Color(0.6f, 0.1f, 0.6f), new Color(0.7f, 0.2f, 0.7f) }, // Morados
        { new Color(1f, 0.6f, 0.8f), new Color(1f, 0.7f, 0.9f) }  // Rosas
    };
    private int indiceColorActual = 0;

    void Start()
    {
        // Quitar texto de todos los botones
        foreach (Button btn in buttons)
        {
            TMP_Text btnText = btn.GetComponentInChildren<TMP_Text>();
            if (btnText != null)
            {
                btnText.text = "";
            }
        }

        CambiarAleatorio();
        ActualizarColores();

        // Asignar eventos a los botones
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => VerificarBoton(index));
        }

        ActualizarTiempoUI();
        ActualizarPuntuacionUI();
    }

    void Update()
    {
        if (juegoActivo)
        {
            tiempoRestante -= Time.deltaTime; // Reducir el tiempo
            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                FinDelJuego();
            }
            ActualizarTiempoUI();
        }
    }

    void CambiarAleatorio()
    {
        System.Random rnd = new System.Random();

        // Poner todos los valores en false
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                tabla[i, j] = false;
            }
        }

        // Seleccionar una posición aleatoria y ponerla en true
        int fila = rnd.Next(0, 5);
        int columna = rnd.Next(0, 5);
        tabla[fila, columna] = true;

        // Cambiar a la siguiente pareja de colores
        indiceColorActual = (indiceColorActual + 1) % 6;
    }

    void ActualizarColores()
    {
        Color colorFalso = parejasColores[indiceColorActual, 0];
        Color colorCorrecto = parejasColores[indiceColorActual, 1];

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                int index = i * 5 + j;
                if (buttons[index] != null)
                {
                    buttons[index].image.color = tabla[i, j] ? colorCorrecto : colorFalso;
                }
            }
        }
    }

    void VerificarBoton(int index)
    {
        if (!juegoActivo) return; // No hacer nada si el juego ha terminado

        int fila = index / 5;
        int columna = index % 5;

        // Si el botón corresponde al 1 en la tabla, aumentar la puntuación
        if (tabla[fila, columna])
        {
            puntuacion++; // Sumar un punto
            CambiarAleatorio();
            ActualizarColores();
            ActualizarPuntuacionUI();
        }
    }

    void ActualizarTiempoUI()
    {
        timerText.text = $"Time: {Mathf.CeilToInt(tiempoRestante)}s";
    }

    void ActualizarPuntuacionUI()
    {
        scoreText.text = $"Score: {puntuacion}";
    }

    void FinDelJuego()
    {
        juegoActivo = false;
        timerText.text = "¡Time has ended!";
        scoreText.text = $"Final score: {puntuacion}";

        // Deshabilitar botones
        foreach (Button btn in buttons)
        {
            btn.interactable = false;
        }
        Invoke("CambiarEscena", 5);
    }

    void CambiarEscena()
    {
        SceneManager.LoadScene("PantallaJuego1PRE");
    }
}