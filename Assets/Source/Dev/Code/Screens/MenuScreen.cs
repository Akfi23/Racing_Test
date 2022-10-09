using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuScreen : UIScreen
{
    [SerializeField] private Button _startRaceButton;
    [SerializeField] private Transform _levelSelectorWindow;
    [SerializeField] private TMP_Dropdown _renderSettingList;
    [SerializeField] private TMP_InputField _nameImput;
    [SerializeField] private Button _submitName;

    public Button StartRaceButton => _startRaceButton;
    public Transform LevelSelectorWindow => _levelSelectorWindow;
    public TMP_Dropdown RenderSettingList=>_renderSettingList;
    public TMP_InputField NameInput => _nameImput;
    public Button SubmitName => _submitName;
}
