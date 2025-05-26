using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Juego3 : MonoBehaviour
{
    public Button botonReaccion;
    public TMP_Text mensajeTiempo;
    private float tiempoInicio;
    private bool puedePresionar = false;

    void Start()
    {
        MoverBotonAleatoriamente();

        // Ocultamos el botón al inicio
        botonReaccion.gameObject.SetActive(false);

        // Mostramos el mensaje inicial
        mensajeTiempo.text = "Don't press yet";

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

    void MoverBotonAleatoriamente()
    {
        RectTransform rt = botonReaccion.GetComponent<RectTransform>();
        RectTransform canvasRT = botonReaccion.GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        float anchoCanvas = canvasRT.rect.width;
        float altoCanvas = canvasRT.rect.height;
        altoCanvas = altoCanvas - 180;

        float anchoBoton = rt.rect.width;
        float altoBoton = rt.rect.height;

        float x = Random.Range(-anchoCanvas / 2 + anchoBoton / 2, anchoCanvas / 2 - anchoBoton / 2);
        float y = Random.Range(-altoCanvas / 2 + altoBoton / 2, altoCanvas / 2 - altoBoton / 2);

        rt.anchoredPosition = new Vector2(x, y);
    }

    IEnumerator EsperarYCambiar()
    {
        float espera = Random.Range(3f, 5f);
        yield return new WaitForSeconds(espera);

        // Activamos el botón y cambiamos su apariencia
        botonReaccion.gameObject.SetActive(true);
        botonReaccion.image.color = Color.green;
        CambiarTextoBoton("CLICK NOW!");

        mensajeTiempo.text = ""; // Limpiamos el mensaje

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
        SceneManager.LoadScene("PantallaJuego3PRE");
    }
}
