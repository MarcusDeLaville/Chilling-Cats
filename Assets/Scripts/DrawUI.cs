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

public class DrawUI : MonoBehaviour
{
    [SerializeField] private Text _coinsText;
    [SerializeField] private Text _catsCoinsCount;

    [SerializeField] private Coins _coins;

    [SerializeField] private float _minTime;
    [SerializeField] private float _maxTime;

    private ValueAction _valueAction;

    private void Start()
    {
        _coins = FindObjectOfType<Coins>();
        SetCurentValue();
    }

    private void Update()
    {
        _catsCoinsCount.text = _coins.GetHearts().ToString();
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

    public void ChangeValueUI(int value, ValueAction valueAction)
    {
        float normalizeValue = 0;

        if (valueAction == ValueAction.Add)
        {
            normalizeValue = _coins.GetCoins() + value;
        }
        else if (valueAction == ValueAction.Deprive)
        {
            normalizeValue = _coins.GetCoins() - value;
        }

        float duration = Mathf.Lerp(_minTime, _maxTime, normalizeValue);
        StartCoroutine(LerpValue(_coins.GetCoins(), normalizeValue, duration, SetTextValue));
    } 

    private void SetTextValue(float value)
    {
        _coinsText.text = ((int)value).ToString();
    }

    private void SetCurentValue()
    {
        _coinsText.text = _coins.GetCoins().ToString();
    }
}
