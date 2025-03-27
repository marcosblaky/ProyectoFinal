using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Juego1 : MonoBehaviour
{
    public Button[] buttons;  // Arreglo de 25 botones en Unity
    public TMP_Text timerText; // Texto para mostrar el tiempo
    public TMP_Text scoreText; // Texto para mostrar la puntuación
    private bool[,] tabla = new bool[5, 5];
    private float tiempoRestante = 60f; // 60 segundos de juego
    private bool juegoActivo = true;
    private int puntuacion = 0; // Contador de puntuación

    void Start()
    {
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
    }

    void ActualizarColores()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                int index = i * 5 + j;
                if (buttons[index] != null)
                {
                    buttons[index].image.color = tabla[i, j] ? Color.green : Color.red;
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
        timerText.text = $"Tiempo: {Mathf.CeilToInt(tiempoRestante)}s";
    }

    void ActualizarPuntuacionUI()
    {
        scoreText.text = $"Puntuación: {puntuacion}";
    }

    void FinDelJuego()
    {
        juegoActivo = false;
        timerText.text = "¡Tiempo terminado!";
        scoreText.text = $"Puntuación final: {puntuacion}";

        // Deshabilitar botones
        foreach (Button btn in buttons)
        {
            btn.interactable = false;
        }
    }
}