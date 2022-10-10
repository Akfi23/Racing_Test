using System.Collections;
using System.Collections.Generic;
using Supyrb;
using UnityEngine;

public class CarHPSystem : GameSystemWithScreen<GameScreen>
{
    public override void OnInit()
    {
        screen.HPText.text = game.Health.ToString();
        Signals.Get<OnObstacleCollide>().AddListener(CheckPower);
    }

    private void CheckPower(float power)
    {
        int damage = Mathf.RoundToInt(power) / 10;

        if (game.Health - damage > 0)
            game.Health -= damage;
        else
            game.Health = 0;

        screen.HPText.text = game.Health.ToString();

        if (game.Health <= 0) Bootstrap.Instance.ChangeGameState(GameStateID.Result);
    }
}
