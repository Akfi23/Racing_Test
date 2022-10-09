using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Used to store runtime game data.
/// 
[Serializable]
public class GameData
{
    public CarContainerComponent Player;
    public int GearShiftNumber=1;
    public LevelConfig CurrentLevelConfig;
    public bool IsWin;
}