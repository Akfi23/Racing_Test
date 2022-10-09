using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSystem : GameSystemWithScreen<ResultScreen>
{
    public override void OnInit()
    {
        CompareResult();
        screen.RestartButton.onClick.AddListener(() => Bootstrap.Instance.GameRestart(0));
    }

    private void CompareResult()
    {
        if (game.IsWin)
            screen.ResultText.text = "Win!";
        else
            screen.ResultText.text = "Lose!";
    }
}
