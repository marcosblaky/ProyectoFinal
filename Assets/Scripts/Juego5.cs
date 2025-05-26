using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Juego5 : MonoBehaviour
{
    public Button botonRojo;
    public Button botonVerde;
    public Button botonAzul;
    public Button botonAmarillo;

    public TextMeshProUGUI textoPuntuacion;
    public TextMeshProUGUI textoInstrucciones; // Nuevo campo para el mensaje de instrucciones

    public float tiempoEncendido = 0.5f;
    public float tiempoEntreLuces = 0.2f;

    private Dictionary<int, Button> mapaBotones;
    private List<int> secuenciaCorrecta = new List<int>();
    private List<int> entradaJugador = new List<int>();
    private int puntuacion = 0;
    private bool turnoJugador = false;

    private Dictionary<int, Color> coloresOriginales = new Dictionary<int, Color>();

    // Colores específicos para cada botón
    private Color colorRojo = Color.red;
    private Color colorVerde = Color.green;
    private Color colorAzul = Color.blue;
    private Color colorAmarillo = Color.yellow;

    void Start()
    {
        // Asignar colores a los botones según su nombre
        botonRojo.image.color = colorRojo;
        botonVerde.image.color = colorVerde;
        botonAzul.image.color = colorAzul;
        botonAmarillo.image.color = colorAmarillo;

        LimpiarTextoBoton(botonRojo);
        LimpiarTextoBoton(botonVerde);
        LimpiarTextoBoton(botonAzul);
        LimpiarTextoBoton(botonAmarillo);

        mapaBotones = new Dictionary<int, Button>()
        {
            { 0, botonRojo },
            { 1, botonVerde },
            { 2, botonAzul },
            { 3, botonAmarillo }
        };

        // Guardar los colores originales de los botones
        coloresOriginales[0] = colorRojo;
        coloresOriginales[1] = colorVerde;
        coloresOriginales[2] = colorAzul;
        coloresOriginales[3] = colorAmarillo;

        // Asignar eventos
        botonRojo.onClick.AddListener(() => BotonPresionado(0));
        botonVerde.onClick.AddListener(() => BotonPresionado(1));
        botonAzul.onClick.AddListener(() => BotonPresionado(2));
        botonAmarillo.onClick.AddListener(() => BotonPresionado(3));

        IniciarJuego();
    }

    void IniciarJuego()
    {
        secuenciaCorrecta.Clear();
        entradaJugador.Clear();
        puntuacion = 0;
        ActualizarTextoPuntuacion();
        AñadirNumeroAleatorio();
        StartCoroutine(MostrarSecuencia());
    }

    void LimpiarTextoBoton(Button boton)
    {
        TextMeshProUGUI texto = boton.GetComponentInChildren<TextMeshProUGUI>();
        if (texto != null) texto.text = "";
    }

    void AñadirNumeroAleatorio()
    {
        int numeroAleatorio = Random.Range(0, 4);
        secuenciaCorrecta.Add(numeroAleatorio);
    }

    IEnumerator MostrarSecuencia()
    {
        turnoJugador = false;
        entradaJugador.Clear();

        // Mostrar el mensaje de "Wait and Memorize" mientras el jugador memorizando
        if (textoInstrucciones != null)
        {
            textoInstrucciones.text = "Wait and Memorize";
        }

        yield return new WaitForSeconds(0.5f); // pausa antes de empezar

        foreach (int indice in secuenciaCorrecta)
        {
            Button boton = mapaBotones[indice];

            // Cambiar el color temporalmente a negro
            boton.image.color = Color.black;
            yield return new WaitForSeconds(tiempoEncendido);

            // Restaurar al color original del botón
            boton.image.color = coloresOriginales[indice];
            yield return new WaitForSeconds(tiempoEntreLuces);
        }

        // Una vez terminada la secuencia, indicamos que el jugador repita
        if (textoInstrucciones != null)
        {
            textoInstrucciones.text = "Repeat";
        }

        turnoJugador = true;
    }

    void BotonPresionado(int indiceBoton)
    {
        if (!turnoJugador) return;

        entradaJugador.Add(indiceBoton);

        int indexActual = entradaJugador.Count - 1;
        if (entradaJugador[indexActual] != secuenciaCorrecta[indexActual])
        {
            Debug.Log("¡Fallaste!");
            textoInstrucciones.text = "You failed";

            // Desactivar todos los botones cuando el jugador falle
            DesactivarBotones();

            // Cambiar de escena después de un pequeño retraso
            Invoke("CambiarEscena", 5);
            return;
        }

        if (entradaJugador.Count == secuenciaCorrecta.Count)
        {
            puntuacion++;
            ActualizarTextoPuntuacion();
            AñadirNumeroAleatorio();
            StartCoroutine(MostrarSecuencia());
        }
    }

    void ActualizarTextoPuntuacion()
    {
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = "Puntuación: " + puntuacion;
        }
    }

    void DesactivarBotones()
    {
        // Desactivar los botones
        botonRojo.interactable = false;
        botonVerde.interactable = false;
        botonAzul.interactable = false;
        botonAmarillo.interactable = false;
    }

    void ActivarBotones()
    {
        // Reactivar los botones
        botonRojo.interactable = true;
        botonVerde.interactable = true;
        botonAzul.interactable = true;
        botonAmarillo.interactable = true;
    }

    void CambiarEscena()
    {
        SceneManager.LoadScene("PantallaJuego5PRE");
    }
}
