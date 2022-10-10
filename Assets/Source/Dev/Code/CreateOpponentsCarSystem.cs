using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Device;

public class CreateOpponentsCarSystem : GameSystem
{
    [SerializeField] private AssetReference _carPrefab;

    public async override void OnInit()
    {
        for (int i = 0; i < 3; i++)
        {
            await CreateCar(i);
        }
    }

    private async UniTask CreateCar(int index)
    {
        var car = await _carPrefab.InstantiateAsync(null).ToUniTask();
        car.transform.position = game.Player.transform.position+Vector3.right*(4*(index+1));
    }
}
