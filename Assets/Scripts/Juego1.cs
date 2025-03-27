using System;
using UnityEngine;
using UnityEngine.UI;

public class Juego1 : MonoBehaviour
{
    public Button[] buttons; // Debe tener 25 botones en el Inspector
    private bool[,] tabla = new bool[5, 5];

    void Start()
    {
        CambiarAleatorio();
        ActualizarColores();

        // Asignar eventos a los botones
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Captura el índice para la lambda
            buttons[i].onClick.AddListener(() => VerificarBoton(index));
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
                int index = i * 5 + j; // Convertir coordenadas 2D a 1D para acceder al array de botones
                if (buttons[index] != null)
                {
                    buttons[index].image.color = tabla[i, j] ? Color.green : Color.red;
                }
            }
        }
    }

    void VerificarBoton(int index)
    {
        int fila = index / 5;
        int columna = index % 5;

        // Si el botón corresponde al 1 en la tabla, actualizar los colores con una nueva posición aleatoria
        if (tabla[fila, columna])
        {
            CambiarAleatorio();
            ActualizarColores();
        }
    }
}