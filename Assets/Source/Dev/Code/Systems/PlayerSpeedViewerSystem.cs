using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedViewerSystem : GameSystemWithScreen<GameScreen>
{
    private int cachedValue;

    public override void OnFixedUpdate()
    {
        int speed = (int)game.Player.RB.velocity.magnitude;

        if (cachedValue == speed) return;

        screen.SpeedText.text =$"{cachedValue} km/h";
        cachedValue = speed;
    }
}
