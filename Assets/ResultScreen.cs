using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : UIScreen
{
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private Button _restartButton;

    public TMP_Text ResultText => _resultText;
    public Button RestartButton => _restartButton;
}
