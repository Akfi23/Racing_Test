using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonComponent : MonoBehaviour
{
    private TMP_Text _levelText;
    private Button _levelButton;

    public TMP_Text LevelText => _levelText;
    public Button LevelButton => _levelButton;

    public void InitButton(int levelNumber)
    {
        _levelText = GetComponentInChildren<TMP_Text>();
        _levelText.text = "Level " + levelNumber.ToString();

        _levelButton = GetComponent<Button>();
    }
}
