using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class InitShopSystem : GameSystemWithScreen<ShopScreen> // Rename to ColorSelectorSystem !!!
{
    public async override void OnInit()
    {
        screen.ColorShopButton.onClick.AddListener(ToggleShopWindow);

        for (int i = 0; i < config.ReferenceContainer.ColorConfigsRef.Length; i++)
        {
            await LoadColorConfig(i);
        }

        await LoadButtonPrefab();

        SetColorBySave(player.ColorName);
    }

    private void ToggleShopWindow()
    {
        screen.ColorShopWindow.gameObject.SetActive(!screen.ColorShopWindow.gameObject.activeSelf);
    }

    private async UniTask LoadColorConfig(int configIndex)
    {
        AsyncOperationHandle<ColorConfig> handle = config.ReferenceContainer.ColorConfigsRef[configIndex].LoadAssetAsync<ColorConfig>();
        await handle.ToUniTask();

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            ColorConfig config = handle.Result;
            game.ColorConfigs.Add(config.Name, config);
            Addressables.Release(handle);
        }
    }

    private async UniTask LoadButtonPrefab()
    {
        foreach (var config in game.ColorConfigs)
        {
            var button = await this.config.ReferenceContainer.ColorButtonRef.InstantiateAsync(screen.ColorContentParent).ToUniTask();
            button.TryGetComponent(out ColorButtonComponent colorButton);
            colorButton.InitButton(config.Value.Color);
            colorButton.Button.onClick.AddListener(() => SetCarColor(config.Value.ColorMaterial, config.Value.Name));
        }
    }

    private void SetCarColor(Material material, string name)
    {
        game.Player.BodyRenderer.material = material;
        player.ColorName = name;
    }

    private void SetColorBySave(string name)
    {
        var material = game.ColorConfigs[name].ColorMaterial;
        var color = game.ColorConfigs[name].Name;

        SetCarColor(material, color);
    }
}
