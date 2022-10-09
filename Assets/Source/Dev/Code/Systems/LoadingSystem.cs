using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Supyrb;
using DG.Tweening;
using System;

public class LoadingSystem : GameSystemWithScreen<LoadingScreen>
{
    public override void OnInit()
    {
        Signals.Clear();
        Application.targetFrameRate = 120;

        screen.LoadingImage.transform.DORotate(Vector3.forward * 90f, 1).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        EndLoadingView();
    }

    private async void EndLoadingView()
    {
        await OnLoadingViewEnded();
    }

    private async UniTask OnLoadingViewEnded()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(4f));
        screen.LoadingImage.transform.DOKill();

        screen.LoadingImage.transform.DORotate(Vector3.up * 90f, 0.3f).SetLoops(5, LoopType.Incremental).SetEase(Ease.Linear).
            OnComplete(()=>screen.BackgroundImage.DOFade(0,0.3f));
    }
}
