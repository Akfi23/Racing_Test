using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButtonComponent : MonoBehaviour
{
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Button _chooseColorButton;

    public Image Image => _buttonImage;
    public Button Button => _chooseColorButton;

    public void InitButton(Color color)
    {
        _buttonImage = GetComponent<Image>();
        _chooseColorButton = GetComponent<Button>();
        _buttonImage.color = color;
    }
}
