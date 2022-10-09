using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Used to save data via Odin Serializer to PlayerPrefs
/// </summary>
[Serializable]
public class PlayerData
{
    public int RenderSettingsIndex=1;
    public int Money = 500;
    public int Health;
    public string ColorName = "Yellow";
    public string PlayerName = "Player";
}
