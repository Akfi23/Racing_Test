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
    [SerializeField] private AssetReference _buttonPrefab;
    [SerializeField] private AssetReference[] _colorConfigsAsset;

    private Dictionary<string, ColorConfig> _colorConfigs = new Dictionary<string, ColorConfig>();

    public async override void OnInit()
    {
        screen.ShopButton.onClick.AddListener(ToggleShopWindow);

        for (int i = 0; i < _colorConfigsAsset.Length; i++)
        {
            await LoadColorConfig(i);
        }

        await LoadButtonPrefab();

        SetColorBySave(player.ColorName);
    }

    private void ToggleShopWindow()
    {
        screen.ShopWindow.gameObject.SetActive(!screen.ShopWindow.gameObject.activeSelf);
    }

    private async UniTask LoadColorConfig(int configIndex)
    {
        AsyncOperationHandle<ColorConfig> handle = _colorConfigsAsset[configIndex].LoadAssetAsync<ColorConfig>();
        await handle.ToUniTask();

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            ColorConfig config = handle.Result;
            _colorConfigs.Add(config.Name, config);
            Addressables.Release(handle);
        }
    }

    private async UniTask LoadButtonPrefab()
    {
        foreach (var config in _colorConfigs)
        {
            var button = await _buttonPrefab.InstantiateAsync(screen.ContentParent).ToUniTask();
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
        var material = _colorConfigs[name].ColorMaterial;
        var color = _colorConfigs[name].Name;

        SetCarColor(material, color);
    }
}
