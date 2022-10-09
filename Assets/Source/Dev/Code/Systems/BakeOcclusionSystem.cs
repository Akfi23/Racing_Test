using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class BakeOcclusionSystem : GameSystem
{
    public override void OnInit()
    {
        StartBake();

        Bootstrap.Instance.ChangeGameState(GameStateID.Game);
    }

    private async void StartBake()
    {
        await BakeOcclusionMap();
    }

    private async UniTask BakeOcclusionMap()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        StaticOcclusionCulling.Compute();
    }
}
