using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private int _coinsCount;
    [SerializeField] private int _catsCoinsCount;
    [SerializeField] private int _coinsPerVisit = 10;

    [SerializeField] private string _savePath;
    [SerializeField] private string _saveFileName = "data.json";

    [SerializeField] private DrawUI _drawUI;

    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _savePath = Path.Combine(Application.persistentDataPath, _saveFileName);
#else
        _savePath = Path.Combine(Application.dataPath, _saveFileName);

#endif
        LoadConfig();
    }

    private void Start()
    {
        _drawUI = FindObjectOfType<DrawUI>();
    }

    public void AddCoins(int value)
    {
        _drawUI.ChangeValueUI(value, ValueAction.Add);
        _coinsCount += value;
    }

    public void DepriveCoins(int value)
    {
        _coinsCount -= value;
        _drawUI.ChangeValueUI(value, ValueAction.Deprive);
    }

    public void AddHearts(int value)
    {
        _catsCoinsCount += value;
    }

    public void DepriveHearts(int value)
    {
        _catsCoinsCount -= value;
    }

    public int GetCoins()
    {
        return _coinsCount;
    }

    public int GetHearts()
    {
        return _catsCoinsCount;
    }

    public void AddPricePerVisit(int growth)
    {
        _coinsPerVisit += growth;
    }

    public int CoinsPerVisit => _coinsPerVisit;

    public void SaveConfig()
    {
        CoinsSave _coinsSave = new CoinsSave
        {
            CoinsCount = this._coinsCount,     
            CatsCoinsCount = this._catsCoinsCount,
            CoinsPerVisit = this._coinsPerVisit
        };

        string json = JsonUtility.ToJson(_coinsSave, true);
        File.WriteAllText(_savePath, json);
    }

    public void LoadConfig()
    {
        if (!File.Exists(_savePath))
        {
        }

        string json = File.ReadAllText(_savePath);
        CoinsSave _coinsFromJson = JsonUtility.FromJson<CoinsSave>(json);

        _coinsCount = _coinsFromJson.CoinsCount;
        _catsCoinsCount = _coinsFromJson.CatsCoinsCount;
        _coinsPerVisit = _coinsFromJson.CoinsPerVisit;
    }

    private void OnApplicationQuit()
    {
        SaveConfig();
    }

    private void OnApplicationPause(bool pause)
    {
        SaveConfig();
    }

}

[Serializable]
public struct CoinsSave
{
    public int CoinsCount;
    public int CatsCoinsCount;
    public int CoinsPerVisit;
}

