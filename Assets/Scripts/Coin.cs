using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PaymentType
{
    Visit,
    Purchase
}

public class Coin : MonoBehaviour
{
    private Coins _coins;
    private Buyer _buyer;

    private bool _isCollected = false;
    private int _rewardCount;

    [SerializeField] private Text _text;
    [SerializeField] private Animation _animation;
    private Button _button;

    private void Start()
    {
        _button = GetComponentInChildren<Button>();
    }

    public void SetRewardCount(PaymentType paymentType, int itemPrice = 0)
    {
        switch (paymentType)
        {
            case PaymentType.Purchase:
                _rewardCount = itemPrice;
                break;
            case PaymentType.Visit:
                _rewardCount = (int)(_coins.CoinsPerVisit * _buyer._bonusRate);
                break;
        }
    }

    public void SetCoins(Coins coins)
    {
        _coins = coins;
    }

    public void SetBuyer(Buyer buyer)
    {
        _buyer = buyer;
    }

    public void OnClick()
    {
        if (!_isCollected)
        {
           
            _isCollected = true;
            _coins.AddCoins(_rewardCount);

            _text.text = _rewardCount.ToString();
            _button.interactable = false;
            _animation.Play();

            Destroy(gameObject, 1.5f);
        }
    }
}
