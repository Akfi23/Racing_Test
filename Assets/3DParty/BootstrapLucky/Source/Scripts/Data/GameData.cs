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
    public PlayerCarComponent Player;
    public int Health;
    public int GearShiftNumber=1;

    public LevelConfig CurrentLevelConfig;
    public bool IsWin;

    public Dictionary<string, ColorConfig>ColorConfigs = new Dictionary<string, ColorConfig>();
    public List<OpponentCarComponent> OpponentsCars = new List<OpponentCarComponent>();
}