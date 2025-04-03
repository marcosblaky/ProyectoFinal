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

    void Start()
    {
        CambiarTextoBoton(boton1, "");
        CambiarTextoBoton(boton2, "");
    }

    void CambiarTextoBoton(Button boton, string nuevoTexto)
    {
        TMP_Text texto = boton.GetComponentInChildren<TMP_Text>();
        if (texto != null)
        {
            texto.text = nuevoTexto;
        }
    }
}
