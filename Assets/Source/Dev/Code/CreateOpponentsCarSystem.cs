using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Device;

public class CreateOpponentsCarSystem : GameSystem
{
    public async override void OnInit()
    {
        for (int i = 0; i < game.CurrentLevelConfig.OpponentCount; i++)
        {
            await CreateCar(i);
        }
    }

    private async UniTask CreateCar(int index)
    {
        var car = await config.ReferenceContainer.CarRef.InstantiateAsync(null).ToUniTask();
        car.transform.position = game.Player.transform.position+Vector3.right*(4*(index+1));

        if (!car.TryGetComponent(out OpponentCarComponent opponentCar)) return;

        opponentCar.SetOwner(CarOwner.Opponent);
        opponentCar.RB.isKinematic = false;
        game.OpponentsCars.Add(opponentCar);
    }
}
