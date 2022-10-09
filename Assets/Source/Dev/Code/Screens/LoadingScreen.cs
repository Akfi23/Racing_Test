using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : UIScreen
{
    [SerializeField] private Image _loadingImage;
    [SerializeField] private Image _backGroundImage;

    public Image LoadingImage => _loadingImage;
    public Image BackgroundImage => _backGroundImage;
}
