using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputNameSystem : GameSystemWithScreen<MenuScreen>
{
    public override void OnInit()
    {
        screen.NameInput.text = player.PlayerName;
        screen.SubmitName.onClick.AddListener(() => SubmitName());
    }

    private void SubmitName()
    {
        player.PlayerName = screen.NameInput.text;
        screen.NameInput.gameObject.SetActive(false);
    }
}
