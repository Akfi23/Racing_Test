using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadingSystem : GameSystem
{
    public override void OnInit()
    {
        game.Player = FindObjectOfType<CarContainerComponent>();

        game.Player.SetOwner(CarOwner.Player);
    }
}
