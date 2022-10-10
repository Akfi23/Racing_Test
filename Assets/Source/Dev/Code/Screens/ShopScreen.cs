using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class ShopScreen : UIScreen
{
    [Header("Shop")]
    [HorizontalLine(color: EColor.Green)]
    [SerializeField] private Button _colorShopButton;
    [SerializeField] private Transform _colorShopWindow;
    [SerializeField] private Transform _colorContentLayoutParent;

    public Button ColorShopButton => _colorShopButton;
    public Transform ColorShopWindow => _colorShopWindow;
    public Transform ColorContentParent => _colorContentLayoutParent;

    [Header("PlayerStats")]
    [HorizontalLine(color: EColor.Blue)]
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _healthText;

    [Header("Upgrades")]
    [HorizontalLine(color: EColor.Yellow)]
    [SerializeField] private Button _upgradeShopButton;
    [SerializeField] private Transform _upgradeShopWindow;
    [SerializeField] private Transform _upgradeContentLayoutParent;

    public Button UpgradeShopButton => _upgradeShopButton;
    public Transform UpgradeShopWindow => _upgradeShopWindow;
    public Transform UpgradeContentParent => _upgradeContentLayoutParent;

    [Header("IAP")]
    [HorizontalLine(color: EColor.Yellow)]
    [SerializeField] private IAPButton _100CoinButton;
    [SerializeField] private IAPButton _200CointButton;

    public IAPButton Coin_100Button => _100CoinButton;
    public IAPButton Coin_200Button => _200CointButton;

    public void UpdateHealthInfo(int value)
    {
        _healthText.text = value.ToString();
    }

    public void UpdateMoneyInfo(int value)
    {
        _moneyText.text = value.ToString();
    }

}
