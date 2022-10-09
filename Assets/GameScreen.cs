using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScreen : UIScreen
{
    [SerializeField] private TMP_Text _nameText;

    public TMP_Text NameText => _nameText;
}
