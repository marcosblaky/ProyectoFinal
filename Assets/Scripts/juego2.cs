using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class juego2 : MonoBehaviour
{
    public Button botonReaccion;

    void Start()
    {
        botonReaccion.image.color = Color.red;
        CambiarTextoBoton("Don't press yet");
    }

    void CambiarTextoBoton(string nuevoTexto)
    {
        TMP_Text texto = botonReaccion.GetComponentInChildren<TMP_Text>(); // Obtener el texto dentro del bot�n
        if (texto != null)
        {
            texto.text = nuevoTexto;
            texto.fontSize = 60; // Aumentar el tama�o de la fuente (ajusta seg�n necesites)
        }
    }
}