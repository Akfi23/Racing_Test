using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGarageSystem : GameSystemWithScreen<ShopScreen>
{
    public override void OnInit()
    {
        screen.UpdateMoneyInfo(player.Money);

        if (player.UpgradeDatas.Count > 0)
            game.Health = config.BaseHealth + player.UpgradeDatas[UpgradeType.Health].BonusValue;
        else
            game.Health = config.BaseHealth;

        screen.UpdateHealthInfo(game.Health);
    }
}
