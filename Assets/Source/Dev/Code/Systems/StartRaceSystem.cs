using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartRaceSystem : GameSystemWithScreen<MenuScreen>
{
    [SerializeField] private LevelButtonComponent _buttonPrefab;
    private LoadingScreen _loadingScreen;

    public override void OnInit()
    {
        _loadingScreen = UIManager.GetUIScreen<LoadingScreen>();
        screen.StartRaceButton.onClick.AddListener(ToggleLevelWindow);

        InitLevelButtons();
    }

    private void InitLevelButtons()
    {
        var levelConfigs = Resources.LoadAll<LevelConfig>("LevelConfigs");

        foreach (var config in levelConfigs)
        {
            var button = Instantiate(_buttonPrefab, screen.LevelSelectorWindow);
            button.InitButton(config.LevelNumber);
            button.LevelButton.OnClickAsAsyncEnumerable().Subscribe(a => LoadLevelAsync(config.LevelNumber));
        }
    }

    private void ToggleLevelWindow()
    {
        screen.LevelSelectorWindow.gameObject.SetActive(!screen.LevelSelectorWindow.gameObject.activeSelf);
    }

    private async void LoadLevelAsync(int levelNumber)
    {
        await StartLoadingLevel(levelNumber);
    }

    private async UniTask StartLoadingLevel(int levelNumber)
    {
        //await UniTask
        _loadingScreen.LoadingImage.transform.rotation = Quaternion.Euler(Vector3.zero);

        _loadingScreen.BackgroundImage.DOFade(1,0.15f).SetEase(Ease.Linear)
            .OnComplete(async ()=> await SceneManager.LoadSceneAsync("Level" + levelNumber, LoadSceneMode.Additive).ToUniTask().ToCoroutine());

        _loadingScreen.LoadingImage.transform.DORotate(Vector3.up * 90f, 0.3f).SetLoops(5, LoopType.Incremental).SetEase(Ease.Linear).
            OnComplete(() => _loadingScreen.BackgroundImage.DOFade(0, 0.3f));

        Bootstrap.Instance.ChangeGameState(GameStateID.PrepareRace);
    }
}
