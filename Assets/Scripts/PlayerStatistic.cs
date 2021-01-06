using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistic : MonoBehaviour
{
    [SerializeField] private int CollectedCoins;
    [SerializeField] private int CollectedHearts;
    [SerializeField] private int CateredCustomers;
    [SerializeField] private int ProductsSold;

    private void Awake()
    {
        SetStatistic();
    }

    public void AddStatisticValue(QuestType questType, int growth)
    {
        switch (questType)
        {
            case QuestType.CollectCoins:
                CollectedCoins += growth;
                break;
            case QuestType.CollectHearts:
                CollectedHearts += growth;
                break;
            case QuestType.CaterCustomers:
                CateredCustomers += growth;
                break;
            case QuestType.SellProducts:
                ProductsSold += growth;
                break;
        }

        SaveStatistic();
    }

    public int CurrentStatisticsValue(QuestType questType)
    {
        int currentValue;

        switch (questType)
        {
            case QuestType.CollectCoins:
                currentValue = CollectedCoins;
                break;
            case QuestType.CollectHearts:
                currentValue = CollectedHearts;
                break;
            case QuestType.CaterCustomers:
                currentValue = CateredCustomers;
                break;
            case QuestType.SellProducts:
                currentValue = ProductsSold;
                break;
            default:
                currentValue = 0;
                break;

        }

        return currentValue;
    }

    private void OnApplicationQuit()
    {
        SaveStatistic();
    }

    private void OnApplicationPause(bool pause)
    {
        SaveStatistic();
    }

    private void SaveStatistic()
    {
        PlayerPrefs.SetInt("CollectedCoins", CollectedCoins);
        PlayerPrefs.SetInt("CollectedHearts", CollectedHearts);
        PlayerPrefs.SetInt("CateredCustomers", CateredCustomers);
        PlayerPrefs.SetInt("ProductsSold", ProductsSold);

        PlayerPrefs.Save();
    }

    private void SetStatistic()
    {
        CollectedCoins = PlayerPrefs.GetInt("CollectedCoins");
        CollectedHearts = PlayerPrefs.GetInt("CollectedHearts");
        CateredCustomers = PlayerPrefs.GetInt("CateredCustomers");
        ProductsSold = PlayerPrefs.GetInt("ProductsSold");
    }
}
