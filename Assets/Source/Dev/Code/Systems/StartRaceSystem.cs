using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class StartRaceSystem : GameSystemWithScreen<MenuScreen>
{
    [SerializeField] private AssetReference _buttonPrefab;
    [SerializeField] private AssetReference[] _levelConfigAssets;
    [SerializeField] private AssetReference[] _scenes;

    private LoadingScreen _loadingScreen;

    public async override void OnInit()
    {
        _loadingScreen = UIManager.GetUIScreen<LoadingScreen>();
        screen.StartRaceButton.onClick.AddListener(ToggleLevelWindow);

        for (int i = 0; i < _levelConfigAssets.Length; i++)
        {
            await InitLevelConfigs(i);
        }
    }

    private async UniTask InitLevelConfigs(int configIndex)
    {
        AsyncOperationHandle<LevelConfig> levelHandle = _levelConfigAssets[configIndex].LoadAssetAsync<LevelConfig>();
        await levelHandle.ToUniTask();

        if (levelHandle.Status == AsyncOperationStatus.Succeeded)
        {
            LevelConfig config = levelHandle.Result;
            await InitLevelButton(config);
            Addressables.Release(levelHandle);
        }
    }

    private async UniTask InitLevelButton(LevelConfig config)
    {
        var button = await _buttonPrefab.InstantiateAsync(screen.LevelSelectorWindow).ToUniTask();
        button.TryGetComponent(out LevelButtonComponent levelButton);
        levelButton.InitButton(config.LevelNumber);
        levelButton.LevelButton.OnClickAsAsyncEnumerable().Subscribe(a => LoadLevelAsync(config.LevelNumber));
    }

    private void ToggleLevelWindow()
    {
        screen.LevelSelectorWindow.gameObject.SetActive(!screen.LevelSelectorWindow.gameObject.activeSelf);
    }

    private async void LoadLevelAsync(int levelNumber)
    {
        AdsManager.Instance.ShowInterstitial("Start Race");
        await StartLoadingLevel(levelNumber).ToCoroutine();
    }

    private async UniTask StartLoadingLevel(int levelNumber)
    {
        _loadingScreen.LoadingImage.transform.rotation = Quaternion.Euler(Vector3.zero);

        _loadingScreen.BackgroundImage.DOFade(1,0.15f).SetEase(Ease.Linear)
            .OnComplete(async ()=> await _scenes[levelNumber-1].LoadSceneAsync(LoadSceneMode.Additive).ToUniTask());

        _loadingScreen.LoadingImage.transform.DORotate(Vector3.up * 90f, 0.3f).SetLoops(5, LoopType.Incremental).SetEase(Ease.Linear).
            OnComplete(() => _loadingScreen.BackgroundImage.DOFade(0, 0.3f));

        Bootstrap.Instance.ChangeGameState(GameStateID.PrepareRace);
    }
}
