using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Android;
using UnityEngine.Events;

[Serializable]
public enum AdvertisingRewardType
{
    Coins = 0,
    Hearths = 1,
    Customers = 2
}

public class Advertising : MonoBehaviour
{
    [SerializeField] private Coins _coins;
    [SerializeField] private Advertise _advertise;

    public AdvertisingRewardType AdRewardType { get; private set; }
    private RewardedAd _rewardedAd;
    private string _adUnitId = "ca-app-pub-6141565930545662/2559035997";

    public UnityEvent FailedLoadEvent;

    public void Start()
    {
        MobileAds.Initialize(initStatus => { });

        AdRequest request = new AdRequest.Builder().Build();
        this._rewardedAd.LoadAd(request);   
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        PlayRewardAd();
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        FailedLoadEvent.Invoke();
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {       
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        if (AdRewardType == AdvertisingRewardType.Coins)
        {
            _coins.AddCoins(450 + (_coins.CoinsPerVisit * 5));
        }
        else if (AdRewardType == AdvertisingRewardType.Hearths)
        {
            _coins.AddHearts(10);
        }
        else if (AdRewardType == AdvertisingRewardType.Customers)
        {
            _advertise.AdvertTurbo();
        }
    }

    public void SetAdsRewardType(AdvertisingRewardType advertisingRewardType)
    {
        AdRewardType = advertisingRewardType;
    }

    public void SetAdsRewardType(int typeNumber)
    {
        if (typeNumber == 0)
        {
            AdRewardType = AdvertisingRewardType.Coins;
        }
        else if (typeNumber == 1)
        {
            AdRewardType = AdvertisingRewardType.Hearths;
        }
        else if (typeNumber == 2)
        {
            AdRewardType = AdvertisingRewardType.Customers;
        }
    }

    public void RequestAd()
    {    
        this._rewardedAd = new RewardedAd(_adUnitId);

        this._rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        this._rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this._rewardedAd.OnAdClosed += HandleRewardedAdClosed;


        AdRequest request = new AdRequest.Builder().Build();
        this._rewardedAd.LoadAd(request);
    }

    private void PlayRewardAd()
    {
        if (_rewardedAd.IsLoaded())
        {
            _rewardedAd.Show();
        }
    }
}
