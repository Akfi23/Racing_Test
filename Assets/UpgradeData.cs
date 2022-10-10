using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradeData 
{
    public int BonusValue;
    public int UpgradeLevel;
    public UpgradeType Type;

    public UpgradeData(UpgradeType type) { Type = type; }
}
