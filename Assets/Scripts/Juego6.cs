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

    [Header("Sprites")]
    public Sprite reversoCarta; // Imagen que se muestra cuando la carta está boca abajo
    public Sprite[] carasCartas = new Sprite[6]; // Las 6 imágenes para cada par

    private int movimientos = 0;
    private bool esperando = false;

    private int[] valoresCartas = new int[12];
    private Button cartaSeleccionada1 = null;
    private Button cartaSeleccionada2 = null;

    void Start()
    {
        textoInstrucciones.text = "Find pairs.";
        textoMovimientos.text = "Moves: 0";

        AsignarValoresAleatorios();

        // Inicializar todas las cartas con la imagen de reverso y añadir listener
        for (int i = 0; i < cartas.Length; i++)
        {
            int index = i; // Para cierre correcto en listener
            cartas[i].image.sprite = reversoCarta;
            cartas[i].interactable = true;
            cartas[i].onClick.AddListener(() => AlHacerClickCarta(index));
        }
    }

    void AsignarValoresAleatorios()
    {
        List<int> valores = new List<int>();

        // Dos veces cada valor (0 a 5)
        for (int i = 0; i < 6; i++)
        {
            valores.Add(i);
            valores.Add(i);
        }

        // Mezclar
        for (int i = 0; i < valores.Count; i++)
        {
            int temp = valores[i];
            int randomIndex = Random.Range(0, valores.Count);
            valores[i] = valores[randomIndex];
            valores[randomIndex] = temp;
        }

        for (int i = 0; i < 12; i++)
        {
            valoresCartas[i] = valores[i];
        }
    }

    void AlHacerClickCarta(int index)
    {
        if (esperando) return;

        Button carta = cartas[index];

        // Si ya está revelada, no hacer nada
        if (carta.image.sprite != reversoCarta) return;

        // Mostrar la cara de la carta
        carta.image.sprite = carasCartas[valoresCartas[index]];

        if (cartaSeleccionada1 == null)
        {
            cartaSeleccionada1 = carta;
            textoInstrucciones.text = "Flip another card";  // Ya seleccionaste la primera carta
        }
        else if (cartaSeleccionada2 == null && carta != cartaSeleccionada1)
        {
            cartaSeleccionada2 = carta;
            movimientos++;
            textoMovimientos.text = "Moves: " + movimientos;
            StartCoroutine(CompararCartas());
        }
    }


    IEnumerator CompararCartas()
    {
        esperando = true;

        int val1 = valoresCartas[System.Array.IndexOf(cartas, cartaSeleccionada1)];
        int val2 = valoresCartas[System.Array.IndexOf(cartas, cartaSeleccionada2)];

        yield return new WaitForSeconds(1f);

        if (val1 == val2)
        {
            // Par encontrado, desactivar botones
            cartaSeleccionada1.interactable = false;
            cartaSeleccionada2.interactable = false;
            textoInstrucciones.text = "¡You found a pair!";
        }
        else
        {
            // No es par, ocultar cartas (poner reverso)
            cartaSeleccionada1.image.sprite = reversoCarta;
            cartaSeleccionada2.image.sprite = reversoCarta;
            textoInstrucciones.text = "Try again.";
        }

        cartaSeleccionada1 = null;
        cartaSeleccionada2 = null;
        esperando = false;

        if (JuegoTerminado())
        {
            textoInstrucciones.text = $"¡You win!";
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
