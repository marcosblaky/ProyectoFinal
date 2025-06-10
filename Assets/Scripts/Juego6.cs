using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class MemoryGame : MonoBehaviour
{
    [Header("Cartas (Botones)")]
    public Button[] cartas = new Button[12];

    [Header("Textos de UI")]
    public TextMeshProUGUI textoMovimientos;
    public TextMeshProUGUI textoInstrucciones;

    private int movimientos = 0;
    private bool esperando = false;

    private int[] valoresCartas = new int[12];
    private Button cartaSeleccionada1 = null;
    private Button cartaSeleccionada2 = null;

    void Start()
    {
        textoInstrucciones.text = "Encuentra los pares.";
        textoMovimientos.text = "Movimientos: 0";

        AsignarValoresAleatorios();

        for (int i = 0; i < cartas.Length; i++)
        {
            int index = i; // evitar error por closures
            cartas[i].onClick.AddListener(() => AlHacerClickCarta(index));
        }
    }

    void AsignarValoresAleatorios()
    {
        List<int> valores = new List<int>();

        // Agregar dos veces cada número del 0 al 5 (6 pares)
        for (int i = 0; i < 6; i++)
        {
            valores.Add(i);
            valores.Add(i);
        }

        // Mezclar la lista
        for (int i = 0; i < valores.Count; i++)
        {
            int temp = valores[i];
            int randomIndex = Random.Range(0, valores.Count);
            valores[i] = valores[randomIndex];
            valores[randomIndex] = temp;
        }

        // Asignar a valoresCartas
        for (int i = 0; i < 12; i++)
        {
            valoresCartas[i] = valores[i];

            // Ocultar texto inicialmente
            cartas[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    void AlHacerClickCarta(int index)
    {
        if (esperando) return;

        Button carta = cartas[index];
        TextMeshProUGUI textoCarta = carta.GetComponentInChildren<TextMeshProUGUI>();

        if (textoCarta.text != "") return; // Ya revelada

        textoCarta.text = valoresCartas[index].ToString();

        if (cartaSeleccionada1 == null)
        {
            cartaSeleccionada1 = carta;
        }
        else if (cartaSeleccionada2 == null && carta != cartaSeleccionada1)
        {
            cartaSeleccionada2 = carta;
            movimientos++;
            textoMovimientos.text = "Movimientos: " + movimientos;
            StartCoroutine(CompararCartas());
        }
    }

    IEnumerator CompararCartas()
    {
        esperando = true;

        int val1 = int.Parse(cartaSeleccionada1.GetComponentInChildren<TextMeshProUGUI>().text);
        int val2 = int.Parse(cartaSeleccionada2.GetComponentInChildren<TextMeshProUGUI>().text);

        yield return new WaitForSeconds(1f);

        if (val1 == val2)
        {
            // Match: desactivar botones
            cartaSeleccionada1.interactable = false;
            cartaSeleccionada2.interactable = false;
            textoInstrucciones.text = "¡Encontraste un par!";
        }
        else
        {
            // No match: ocultar valores
            cartaSeleccionada1.GetComponentInChildren<TextMeshProUGUI>().text = "";
            cartaSeleccionada2.GetComponentInChildren<TextMeshProUGUI>().text = "";
            textoInstrucciones.text = "Intenta de nuevo.";
        }

        cartaSeleccionada1 = null;
        cartaSeleccionada2 = null;
        esperando = false;

        // Verificar si el juego terminó
        if (JuegoTerminado())
        {
            textoInstrucciones.text = $"¡Ganaste en {movimientos} movimientos!";
        }
    }

    bool JuegoTerminado()
    {
        foreach (Button b in cartas)
        {
            if (b.interactable) return false;
        }
        return true;
    }
}
