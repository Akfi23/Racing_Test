using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : UIScreen
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TMP_Text _hpText;

    public TMP_Text NameText => _nameText;
    public Button RestartButton => _restartButton;
    public TMP_Text HPText => _hpText;
}
