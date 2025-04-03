using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Juego4 : MonoBehaviour
{
    public Button boton1;
    public Button boton2;
    public TMP_Text timerText;
    public TMP_Text scoreText;

    private int resultadoMayor; // Guardará el resultado mayor
    private int score = 0; // Puntuación del jugador
    private float tiempoRestante = 60f; // Tiempo de juego en segundos
    private bool juegoActivo = true;

    void Start()
    {
        GenerarOperaciones();
        boton1.onClick.AddListener(() => VerificarRespuesta(boton1));
        boton2.onClick.AddListener(() => VerificarRespuesta(boton2));
        StartCoroutine(Temporizador());
    }

    void CambiarTextoBoton(Button boton, string nuevoTexto)
    {
        TMP_Text texto = boton.GetComponentInChildren<TMP_Text>();
        if (texto != null)
        {
            texto.text = nuevoTexto;
            texto.fontSize = 60;
        }
    }

    void GenerarOperaciones()
    {
        if (!juegoActivo) return;

        (string operacion1, int resultado1) = GenerarOperacion();
        (string operacion2, int resultado2) = GenerarOperacion();

        // Asignar las operaciones a los botones
        CambiarTextoBoton(boton1, operacion1);
        CambiarTextoBoton(boton2, operacion2);

        // Guardar cuál es la mayor
        resultadoMayor = Mathf.Max(resultado1, resultado2);

        // Guardar el resultado en los botones usando PlayerPrefs (opcional)
         boton1.gameObject.name = resultado1.ToString();
         boton2.gameObject.name = resultado2.ToString();
       
    }

    (string, int) GenerarOperacion()
    {
        int num1 = Random.Range(1, 10);
        int num2 = Random.Range(1, 10);
        int operacion = Random.Range(0, 3); // 0 = suma, 1 = resta, 2 = multiplicación

        string textoOperacion;
        int resultado;

        switch (operacion)
        {
            case 0: // Suma
                textoOperacion = $"{num1} + {num2}";
                resultado = num1 + num2;
                break;
            case 1: // Resta
                textoOperacion = $"{num1} - {num2}";
                resultado = num1 - num2;
                break;
            default: // Multiplicación
                textoOperacion = $"{num1} × {num2}";
                resultado = num1 * num2;
                break;
        }

        return (textoOperacion, resultado);
    }

    void VerificarRespuesta(Button botonPresionado)
    {
        if (!juegoActivo) return;

        int resultadoSeleccionado = int.Parse(botonPresionado.gameObject.name);

        if (resultadoSeleccionado == resultadoMayor)
        {
            score++;
        }
        else
        {
            score--;
        }

        scoreText.text = "Score: " + score;
        GenerarOperaciones(); // Generar nuevas operaciones
    }

    IEnumerator Temporizador()
    {
        while (tiempoRestante > 0)
        {
            timerText.text = "Time: " + tiempoRestante.ToString("F0");
            yield return new WaitForSeconds(1);
            tiempoRestante--;
        }

        FinDelJuego();
    }

    void FinDelJuego()
    {
        juegoActivo = false;
        timerText.text = "Time has ended!";
        scoreText.text = "Final score: " + score;
        boton1.interactable = false;
        boton2.interactable = false;
        Invoke("CambiarEscena", 5);
    }
    void CambiarEscena()
    {
        SceneManager.LoadScene("PantallaJuego4PRE");
    }
}
