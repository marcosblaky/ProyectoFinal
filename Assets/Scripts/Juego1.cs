using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Juego1 : MonoBehaviour
{

    public Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        ChangeColorButtons(Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColorButtons(Color newColor)
    {
        foreach (Button btn in buttons)
        {
            btn.image.color = newColor;
        }
    }

}
