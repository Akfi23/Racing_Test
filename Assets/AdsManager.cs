using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : DontDestroySingleton<AdsManager>
{
    #region Constants

    private const string AndroidInterstitial = "8dbb6a66ff9887f1";
    private const string AndroidRewarded = "07df4378cd7d40c3";
    //private const string AndroidBanner = "612155ee1d272b57";

    //private const string IOSInterstitial = "08b0b992bffbd238";
    //private const string IOSRewarded = "80196376e3a2ccd6";
    //private const string IOSBanner = "1fb5edee2b654b43";

    private const string ApplovinKey = "tyH4OD_0TW3Gs0nLCN1lLcd3bok7XbKG-0Vp3gwen-JCv4mGRorrpd89c2VpvQ9hBFlaGozQn0kBaEAhHUzUxJ";

    #endregion

    #region Variables

    private string Interstitial;
    private string Rewarded;
    private string Banner;

    public struct RewardedData
    {
        public Action callback;
        public Action failedCallback;
        public string reason;
        public bool isFail;
    }

    // Interstitial Settings
    private const int InterstitialCooldown = 15;
    private int interstitialRetry = 0;

    // Rewarded Settings
    private int rewardedRetry = 0;
    private RewardedData rewardedData;

    //Helpers
    private float currentTime = 0f;
    private bool noAds = false;
    float timeScaler = 1;

    // Inter quickfix
    private string intersititialReason;
    private Action interstitialCallback;

    int LevelsCount;

    #endregion

    string Reason;

    void Start()
    {
#if UNITY_ANDROID
        Init();
#elif UNITY_IOS || UNITY_IPHONE
        // Version less than 14.5
        if (MaxSdkUtils.CompareVersions(UnityEngine.iOS.Device.systemVersion, "14.5") == MaxSdkUtils.VersionComparisonResult.Lesser)
        {
            Init();
        }

        // ATT status is already set
        else if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() != ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            Init();
        }

        //14.5 + and Not-determined status
        else
        {
            ATTrackingStatusBinding.RequestAuthorizationTracking();
            StartCoroutine(ATTRoutine(1f));
        }
#endif
    }

#if UNITY_IOS || UNITY_IPHONE
    IEnumerator ATTRoutine(float step)
    {
        while (true)
        {
            yield return new WaitForSeconds(step);

            if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() != ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                Init();
                break;
            }
        }
    }
#endif

    void Init()
    {
#if UNITY_ANDROID
        Interstitial = AndroidInterstitial;
        Rewarded = AndroidRewarded;
        //Banner = AndroidBanner;

#elif UNITY_IOS
        Interstitial = IOSInterstitial;
        Rewarded = IOSRewarded;
        Banner = IOSBanner;
 
#endif

        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {

            Debug.Log("MAX SDK Initialized");

#if UNITY_IOS || UNITY_IPHONE //|| UNITY_EDITOR
            if (MaxSdkUtils.CompareVersions(UnityEngine.iOS.Device.systemVersion, "14.5") != MaxSdkUtils.VersionComparisonResult.Lesser)
            {
#if UNITY_IOS
                var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
                var isAuthorized = status == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED;

                AdSettings.SetAdvertiserTrackingEnabled(isAuthorized);
#endif
            }
#endif

            // AdSettings.SetDataProcessingOptions(new string[] { });

            InitializeInterstitialAds();
            InitializeRewardedAds();
            //InitializeBannerAds();
        };

        string[] testDevices = new[] { "e8026c95-b195-4d50-afc3-c261afa3e99a", "3f144809-be48-4561-a04a-8a96fd68388d", "4224c1d5-780a-438b-bd3d-60866bc9962f" };
        MaxSdk.SetTestDeviceAdvertisingIdentifiers(testDevices);
        MaxSdk.SetVerboseLogging(true);
        MaxSdk.SetHasUserConsent(false);
        MaxSdk.SetSdkKey(ApplovinKey);
        MaxSdk.InitializeSdk();
    }

    public void Init(bool noAds = false)
    {
        this.noAds = noAds;
    }

    public void SetupNoAds(bool value)
    {
        noAds = value;
    }

    #region Banner ADS

    //void InitializeBannerAds()
    //{
    //    // Banners are automatically sized to 320×50 on phones and 728×90 on tablets
    //    // You may call the utility method MaxSdkUtils.isTablet() to help with view sizing adjustments
    //    MaxSdk.CreateBanner(Banner, MaxSdkBase.BannerPosition.BottomCenter);
    //    MaxSdk.SetBannerExtraParameter(Banner, "adaptive_banner", "true");

    //    // Set background or background color for banners to be fully functional
    //    MaxSdk.SetBannerBackgroundColor(Banner, Color.black);
    //}

    public void ShowBanner()
    {
        MaxSdk.ShowBanner(Banner);
    }

    #endregion

    #region Interstitial ADS

    private void InitializeInterstitialAds()
    {
        // Attach callback
        MaxSdkCallbacks.OnInterstitialLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.OnInterstitialLoadFailedEvent += OnInterstitialFailedEvent;
        MaxSdkCallbacks.OnInterstitialAdFailedToDisplayEvent += InterstitialFailedToDisplayEvent;
        MaxSdkCallbacks.OnInterstitialHiddenEvent += OnInterstitialDismissedEvent;
        MaxSdkCallbacks.OnInterstitialDisplayedEvent += OnInterstitialDisplayEvent;


        // Load the first interstitial
        LoadInterstitial();
    }

    public void LoadInterstitial()
    {
        MaxSdk.LoadInterstitial(Interstitial);
    }

    public void ShowInterstitial(string reason, Action callback = null)
    {
        if (IsInterstitialReady())
        {
            Reason = reason;

            var available = MaxSdk.IsInterstitialReady(Interstitial);

            interstitialCallback = callback;
            intersititialReason = reason;

            if (available)
            {
                timeScaler = Time.timeScale;
                Time.timeScale = 0;

                MaxSdk.ShowInterstitial(Interstitial);
            }

            else
            {
                LoadInterstitial();
            }
        }
    }

    private void OnInterstitialDisplayEvent(string adUnitId)
    {
    }

    private void OnInterstitialLoadedEvent(string adUnitId)
    {
        interstitialRetry = 0;
    }

    private void OnInterstitialFailedEvent(string adUnitId, int errorCode)
    {
        interstitialRetry++;
        double retryDelay = Math.Pow(2, Math.Min(6, interstitialRetry));
        Invoke("LoadInterstitial", (float)retryDelay);
        Time.timeScale = timeScaler;

    }

    private void InterstitialFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        LoadInterstitial();
        Time.timeScale = timeScaler;
    }

    private void OnInterstitialDismissedEvent(string adUnitId)
    {

        currentTime = Time.realtimeSinceStartup;
        Time.timeScale = timeScaler;
        interstitialCallback?.Invoke();
        LoadInterstitial();
    }

    public bool IsInterstitialReady()
    {
        return Time.realtimeSinceStartup - currentTime >= InterstitialCooldown && noAds == false && MaxSdk.IsInterstitialReady(Interstitial);
    }

    #endregion

    #region Rewarded ADS

    public void InitializeRewardedAds()
    {
        // Attach callback
        MaxSdkCallbacks.OnRewardedAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.OnRewardedAdLoadFailedEvent += OnRewardedAdFailedEvent;
        MaxSdkCallbacks.OnRewardedAdFailedToDisplayEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.OnRewardedAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.OnRewardedAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.OnRewardedAdHiddenEvent += OnRewardedAdDismissedEvent;
        MaxSdkCallbacks.OnRewardedAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

        // Load the first rewarded ad
        LoadRewardedAd();
    }

    public void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(Rewarded);
    }

    public void ShowRewarded(string reason, Action callback = null, Action failedCallback = null)
    {
        Reason = reason;

        var available = MaxSdk.IsRewardedAdReady(Rewarded);
        rewardedData = new RewardedData() { callback = callback, reason = reason, failedCallback = failedCallback, isFail = true };

        if (available)
        {
            timeScaler = Time.timeScale;
            Time.timeScale = 0;
            MaxSdk.ShowRewardedAd(Rewarded);
        }
        else
        {
            LoadRewardedAd();
        }
    }

    private void OnRewardedAdLoadedEvent(string adUnitId)
    {
        rewardedRetry = 0;
    }

    private void OnRewardedAdFailedEvent(string adUnitId, int errorCode)
    {
        rewardedRetry++;
        double retryDelay = Math.Pow(2, Math.Min(6, rewardedRetry));

        Invoke("LoadRewardedAd", (float)retryDelay);
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        Time.timeScale = timeScaler;
        LoadRewardedAd();
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId) { }
    private void OnRewardedAdClickedEvent(string adUnitId) { }
    private void OnRewardedAdDismissedEvent(string adUnitId)
    {
        currentTime = Time.realtimeSinceStartup;
        Time.timeScale = timeScaler;
        LoadRewardedAd();


        if (rewardedData.isFail)
            rewardedData.failedCallback?.Invoke();

        Debug.Log("Rewarded dismissed");
    }
    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward)
    {
        currentTime = Time.realtimeSinceStartup;
        Time.timeScale = timeScaler;
        rewardedData.callback?.Invoke();
        rewardedData.isFail = false;

        Debug.Log("Reward received");
    }

    #endregion
}
