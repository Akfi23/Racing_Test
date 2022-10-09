using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "WorldConfig/levelConfig")]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private int _levelNumber;
    [SerializeField] private int _opponentCount;

    [InfoBox("here can store the envir of the level/ Now is not usable", EInfoBoxType.Warning)]
    [SerializeField] private GameObject _levelEnvir;

    public int LevelNumber => _levelNumber;
    public int OpponentCount => _opponentCount;
    public GameObject LevelEnvir => _levelEnvir;
}
