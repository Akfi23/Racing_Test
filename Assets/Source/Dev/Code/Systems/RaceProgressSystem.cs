using System.Collections;
using System.Collections.Generic;
using Supyrb;
using UnityEngine;

public class RaceProgressSystem : GameSystem
{
    public override void OnInit()
    {
        Signals.Get<OnCollide>().AddListener(CheckFinishCollision);
    }

    private void CheckFinishCollision(Transform transform)
    {
        if (!transform.TryGetComponent(out CarContainerComponent car)) return;

        if (car.GetOwner() == CarOwner.Player)
            game.IsWin = true;

        car.RB.isKinematic = true;

        Bootstrap.Instance.ChangeGameState(GameStateID.Result);
    }
}
