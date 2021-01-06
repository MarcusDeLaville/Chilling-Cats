using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum ValueAction
{
    Add, 
    Deprive
}

public enum WalletType
{
    Coins,
    Hearts
}

public class DrawUI : MonoBehaviour
{
    [SerializeField] private Text _coinsText;
    [SerializeField] private Text _catsCoinsCount;

    [SerializeField] private Coins _coins;

    [SerializeField] private float _minTime;
    [SerializeField] private float _maxTime;

    private ValueAction _valueAction;
    private WalletType _walletType;

    private Text _textValue;

    private void Start()
    {
        SetCurentValue();
    }

    private IEnumerator LerpValue(float startValue, float endValue, float duration, UnityAction<float> action)
    {
        float elapsed = 0;
        float nextVallue;

        while(elapsed < duration)
        {
            nextVallue = Mathf.Lerp(startValue, endValue, elapsed / duration);
            action?.Invoke(nextVallue);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    public void ChangeValueUI(int value, ValueAction valueAction, WalletType walletType)
    {
        float normalizeValue = 0;
        int currentValue = 0;

        if (walletType == WalletType.Coins)
        {
            currentValue = _coins.GetCoins();
            _textValue = _coinsText;
        }
        else if (walletType == WalletType.Hearts)
        {
            currentValue = _coins.GetHearts();
            _textValue = _catsCoinsCount;
        }

        if (valueAction == ValueAction.Add)
        {
            normalizeValue = currentValue + value;
        }
        else if (valueAction == ValueAction.Deprive)
        {
            normalizeValue = currentValue - value;
        }

        float duration = Mathf.Lerp(_minTime, _maxTime, normalizeValue);
        StartCoroutine(LerpValue(currentValue, normalizeValue, duration, SetTextValue));
    } 

    private void SetTextValue(float value)
    {
        _textValue.text = ((int)value).ToString();
    }

    private void SetCurentValue()
    {
        _coinsText.text = _coins.GetCoins().ToString();
        _catsCoinsCount.text = _coins.GetHearts().ToString();
    }
}
