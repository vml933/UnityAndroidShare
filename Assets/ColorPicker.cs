using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public Image canvas;

    public Button btnRed;
    public Button btnGreen;
    public Button btnBlue;

    void Start()
    {

        btnRed.onClick.AddListener(() =>
        {
            canvas.color = Color.red;
        });

        btnGreen.onClick.AddListener(() =>
        {
            canvas.color = Color.green;
        });

        btnBlue.onClick.AddListener(() =>
        {
            canvas.color = Color.blue;
        });

    }

}
