using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CarStatsShopSystem : GameSystemWithScreen<ShopScreen>
{
    private Dictionary<UpgradeType, UpgradeConfig> _upgradeConfigs = new Dictionary<UpgradeType, UpgradeConfig>();

    public async override void OnInit()
    {
        screen.UpgradeShopButton.onClick.AddListener(ToggleUpgradeShopWindow);

        for (int i = 0; i < config.ReferenceContainer.UpgradeConfigRef.Length; i++)
        {
            await LoadColorConfig(i);
        }

        TryInitializeUpgradeData();

        await LoadButtonPrefabs();
    }

    private void ToggleUpgradeShopWindow()
    {
        screen.UpgradeShopWindow.gameObject.SetActive(!screen.UpgradeShopWindow.gameObject.activeSelf);
    }

    private async UniTask LoadColorConfig(int configIndex)
    {
        AsyncOperationHandle<UpgradeConfig> handle = config.ReferenceContainer.UpgradeConfigRef[configIndex].LoadAssetAsync<UpgradeConfig>();
        await handle.ToUniTask();

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            UpgradeConfig config = handle.Result;
            _upgradeConfigs.Add(config.Type, config);
            Addressables.Release(handle);
        }
    }

    private void TryInitializeUpgradeData()
    {
        if (player.UpgradeDatas.Count > 0) return;

        foreach (var config in _upgradeConfigs)
        {
            UpgradeData newData = new UpgradeData(config.Key);
            player.UpgradeDatas.Add(config.Key, newData);
        }
    }

    private async UniTask LoadButtonPrefabs()
    {
        foreach (var config in _upgradeConfigs)
        {
            var button = await this.config.ReferenceContainer.UpgradeButtonRef.InstantiateAsync(screen.UpgradeContentParent).ToUniTask();
            button.TryGetComponent(out UpgradeButtonComponent upgradeButton);
            upgradeButton.Init(config.Key,GetPrice(config.Key));
            upgradeButton.Button.onClick.AddListener(() => GetUpgrade(config.Key,upgradeButton));
        }
    }

    private void GetUpgrade(UpgradeType type,UpgradeButtonComponent upgradeButton)
    {
        int price = GetPrice(type);

        if (player.Money < price) return;

        player.Money -= price;
        screen.UpdateMoneyInfo(player.Money);

        player.UpgradeDatas[type].UpgradeLevel++;
        player.UpgradeDatas[type].BonusValue = player.UpgradeDatas[type].UpgradeLevel * _upgradeConfigs[type].HealthBonus;

        UnityEngine.Debug.Log(player.UpgradeDatas[type].BonusValue);

        upgradeButton.UpdateInfo(GetPrice(type));

        if (type != UpgradeType.Health) return;

        game.Health += player.UpgradeDatas[type].BonusValue;
        screen.UpdateHealthInfo(game.Health);
    }

    private int GetPrice(UpgradeType type)
    {
        int price;
        return price = (player.UpgradeDatas[type].UpgradeLevel + 1) * _upgradeConfigs[type].PriceMultiplyer;
    }
}
