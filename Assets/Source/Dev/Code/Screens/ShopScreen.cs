using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreen : UIScreen
{
    [Header("Shop")]
    [HorizontalLine(color: EColor.Green)]
    [SerializeField] private Button _shopButton;
    [SerializeField] private Transform _shopWindow;
    [SerializeField] private Transform _contentLayoutParent;

    public Transform ShopWindow => _shopWindow;
    public Button ShopButton => _shopButton;
    public Transform ContentParent => _contentLayoutParent;

    [Header("PlayerStats")]
    [HorizontalLine(color: EColor.Blue)]
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _healthText;

    public TMP_Text MoneyText => _moneyText;
    public TMP_Text HealthText => _healthText;
}
