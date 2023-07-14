using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

public class AdsController : MonoBehaviour
{
    public static AdsController instance;
    private bool AdmobBanner = false;
    private bool AdmobRewarded = false;
    private bool AdmobInterstitial = false;
    private bool isShowingAd = false;
    private bool appopentest = true;

    public bool UnityAndAdmob = true;

    public string BannerAdId = "unused";
    public string AppOpenAdId = "unused";
    public string InterstitialAdId = "unused";
    public string RewardedAdId = "unused";
    
/*
#if UNITY_EDITOR
    private string _adUnitId = "unused";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
  private string _adUnitId = "unused";
#endif*/
    RewardedAd rewardedAd;
    BannerView _bannerView;
    InterstitialAd interstitial; 
    AppOpenAd _appOpenAd;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    void Start()
    {
       
        
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                CreateAdmobInterstitial();
              

               
      
            });
        });
        //CreateAdmobSmartBanner();
       
        CreateAndLoadAdmobRewardedAd();
        RequestAppOpenAd();

    }

    private void Update()
    {
        if (appopentest)
        {
            RequestAppOpenAd();
        }
    }


    IEnumerator ssss()
    {
        yield return new WaitForSeconds(2);
        CreateAndLoadAdmobRewardedAd();
        CreateAdmobInterstitial();
        Debug.Log("challahai");
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }
    
    public void CreateAdmobSmartBanner()
    {
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyAd();
        }

        // Create a 320x50 banner at top of the screen
        _bannerView = new BannerView(BannerAdId, AdSize.SmartBanner, AdPosition.Top);
        _bannerView.LoadAd(CreateAdRequest());
        
    }
    /*public void CreateAppOpenAd()
    {
        Debug.Log("Creating banner view");

        
        if (_appOpenAd != null)
        {
            _appOpenAd.Destroy();
        }

       
        _appOpenAd = new AppOpenAd(_adUnitId);
        _bannerView.LoadAd(CreateAdRequest());
        
    }*/

    #region AdmobInterstitial

    
    public void CreateAdmobInterstitial()
    {
        interstitial = new InterstitialAd(InterstitialAdId);
        Debug.Log("Interstitial");

        // If we already have a banner, destroy the old one.
        if (interstitial != null)
        {
            interstitial.Destroy();
        }

        // Create a 320x50 banner at top of the screen
       
        
        
                // Called when an ad request has successfully loaded.
                interstitial.OnAdLoaded += HandleInterstitialLoaded;
                
                interstitial.OnAdFailedToLoad += HandleInterstitialFailedtoLoad;
                // Called when an ad request failed to load.
                // rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
                // Called when an ad is shown.
                interstitial.OnAdOpening += HandleInterstitialAdOpening;
                // Called when an ad request failed to show.
                interstitial.OnAdFailedToShow += HandleInterstitialFailedtoshow;
                // Called when the user should be rewarded for interacting with the ad
                interstitial.OnAdClosed += HandleInterstitialClosed;
                AdRequest adRequest = CreateAdRequest();
        interstitial.LoadAd(adRequest);
        //interstitial.Show();
        
       
        
    }
    public void ShowInterStitialAdmob()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        else
        {
           
        }
        
     
        
    }
    private void HandleInterstitialClosed(object sender, EventArgs e)
    {
        CreateAdmobInterstitial();
    }

    private void HandleInterstitialFailedtoshow(object sender, AdErrorEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void HandleInterstitialAdOpening(object sender, EventArgs e)
    {
        Time.timeScale = 0;
    }

    private void HandleInterstitialFailedtoLoad(object sender, AdFailedToLoadEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void HandleInterstitialLoaded(object sender, EventArgs e)
    {
        Time.timeScale = 1;
    }

#endregion
    public void DestroyAd()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner ad.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }
    
    
    public void ShowSmartBanner()
    {
        CreateAdmobSmartBanner();

        //_bannerView.Show();
    } 
    public void HideSmartBanner()
    {
        if (_bannerView != null)
            _bannerView.Hide();
      
    }

    public void RequestAppOpenAd()
    {
        
        AppOpenAd.LoadAd(AppOpenAdId, ScreenOrientation.AutoRotation, CreateAdRequest(), (_appOpenAd, error) =>
        {
            if (error != null)
            {
                Debug.Log("Failed to load app open ad: " + error);
            }
            else
            {
                this._appOpenAd = _appOpenAd;
                Debug.Log("App open ad loaded");
            }

            appopentest = false;
        });
    }
    public void ShowAppOpenAd()
    {
        if (_appOpenAd != null && !isShowingAd)
        {
            isShowingAd = true;
            _appOpenAd.Show();
            isShowingAd = false;
            RequestAppOpenAd();
        }
    }
    public void CreateAndLoadAdmobRewardedAd()
    {
        
        rewardedAd = new RewardedAd(RewardedAdId);

        // Called when an ad request has successfully loaded.
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        // rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = CreateAdRequest();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);
   
    }

    #region AdmobRewardedEventHandler

    

    private void HandleUserEarnedReward(object sender, Reward e)
    {
        throw new NotImplementedException();
    }

    private void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void HandleRewardedAdOpening(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void HandleRewardedAdClosed(object sender, EventArgs e)
    {
        CreateAndLoadAdmobRewardedAd();
    }

    private void HandleRewardedAdLoaded(object sender, EventArgs e)
    {
      Debug.Log("Rewarded Loaded");
    }
    
    #endregion
    
    public void ShowAdmobRewardedAd()
    {
  
        if (rewardedAd.IsLoaded())
        {
           rewardedAd.Show();
           CreateAndLoadAdmobRewardedAd();
        }
     
        else
        {
            CreateAndLoadAdmobRewardedAd();
        }
    }

    // Update is called once per frame
 
   
  
}
