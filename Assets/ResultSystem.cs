using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSystem : GameSystem
{
    public override void OnInit()
    {
        CompareResult();
    }

    private void CompareResult()
    {
        if (game.IsWin)
            Debug.Log("Win");
        else
            Debug.Log("Lose");
    }
}
