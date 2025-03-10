using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Juego1 : MonoBehaviour
{

    public Button[] buttons;
    // hacer tabla 5x5 de booleanos que son todo 0




    // Start is called before the first frame update
    void Start()
    {
        ChangeColorButtons(Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        
        //que los botones comprueben si el que has apretado es el del color distinto (1 en la tabla) y si es así actualicen colores.

    }

    //hacer funcion que cambie el booleano de la tabla que si es un 1 a uno aleatorio

    public void ChangeColorButtons(Color newColor)
    {
        foreach (Button btn in buttons)
        {
            btn.image.color = newColor;
        }
    }

}
