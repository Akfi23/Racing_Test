using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGarageSystem : GameSystemWithScreen<ShopScreen>
{
    public override void OnInit()
    {
        screen.MoneyText.text = player.Money.ToString();
        screen.HealthText.text = (config.BaseHealth + player.Health).ToString();
    }
}
