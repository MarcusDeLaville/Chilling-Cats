using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawAdsUI : MonoBehaviour
{
    [SerializeField] private Advertising _advertising;
    [SerializeField] private Coins _coins;

    [SerializeField] private Text _rewardText;

    public void DrawRewardUI()
    {
        switch (_advertising.AdRewardType)
        {
            case AdvertisingRewardType.Coins:
                _rewardText.text = $"Посмотрев рекламу, вы получите {450 + (_coins.CoinsPerVisit * 5)} монеток.";
                break;
            case AdvertisingRewardType.Hearths:
                _rewardText.text = $"Посмотрев рекламу, вы получите 10 сердечек.";
                break;
            case AdvertisingRewardType.Customers:
                _rewardText.text = "Если вы посмотрите рекламу, к вам приедет автобус с 15-ю посетителями.";
                break;
        }
    }
}
