using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Juego2 : MonoBehaviour
{
    public Button botonReaccion;
    public TMP_Text mensajeTiempo; 
    private float tiempoInicio;
    private bool puedePresionar = false;

    void Start()
    {
        botonReaccion.image.color = Color.red;
        CambiarTextoBoton("Don't press yet");
        botonReaccion.onClick.AddListener(RegistrarReaccion);
        StartCoroutine(EsperarYCambiar());
    }

    void CambiarTextoBoton(string nuevoTexto)
    {
        TMP_Text texto = botonReaccion.GetComponentInChildren<TMP_Text>();
        if (texto != null)
        {
            texto.text = nuevoTexto;
            texto.fontSize = 60;
        }
    }

    IEnumerator EsperarYCambiar()
    {
        float espera = Random.Range(3f, 5f); // Espera aleatoria entre 3 y 5 segundos
        yield return new WaitForSeconds(espera);

        botonReaccion.image.color = Color.green;
        CambiarTextoBoton("CLICK NOW!");
        tiempoInicio = Time.time;
        puedePresionar = true;
    }

    void RegistrarReaccion()
    {
        if (puedePresionar)
        {
            float tiempoReaccion = Time.time - tiempoInicio;
            mensajeTiempo.text = $"Reaction Time: {tiempoReaccion:F3} sec";
            puedePresionar = false;
            botonReaccion.interactable = false;
            Invoke("CambiarEscena", 5);
        }
        else
        {
            mensajeTiempo.text = "Too soon! Wait for green.";
        }
    }
    void CambiarEscena()
    {
        SceneManager.LoadScene("PantallaJuego2PRE");
    }
}
