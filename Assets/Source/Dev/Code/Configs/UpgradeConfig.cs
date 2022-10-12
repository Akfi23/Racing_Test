using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UpgradeConfig/UpgradeConfig")]
public class UpgradeConfig : ScriptableObject
{
    [SerializeField] private int _healthBaseBonus;
    [SerializeField] private int _priceMultiplyer;
    [SerializeField] private UpgradeType _type;

    public int HealthBonus => _healthBaseBonus;
    public int PriceMultiplyer => _priceMultiplyer;
    public UpgradeType Type => _type;
}
