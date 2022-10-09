using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGameSystem : GameSystemWithScreen<GameScreen>
{
    public override void OnInit()
    {
        cameraController.SetRaceCameraActive();
        game.Player.RB.isKinematic = false;
        screen.NameText.text = player.PlayerName;
    }
}
