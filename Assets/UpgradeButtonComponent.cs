using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonComponent : MonoBehaviour
{
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private TMP_Text _nameText;

    public Button Button => _upgradeButton;

    public void Init(UpgradeType type,int price)
    {
        _nameText.text = type.ToString();
        UpdateInfo(price);
    }

    public void UpdateInfo(int price)
    {
        _priceText.text = price.ToString();
    }
}
