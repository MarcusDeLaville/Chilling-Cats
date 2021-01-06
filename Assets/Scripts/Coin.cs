using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    private Coins _coins;
    public Buyer _buyer;

    private bool _isCollected = false;
    [SerializeField] private int _rewardCount = 100;

    [SerializeField] private Text _text;
    [SerializeField] private Animation _animation;
    private Button _button;

    private void Start()
    {
        _button = GetComponentInChildren<Button>();
        _coins = FindObjectOfType<Coins>();
    }

    public void OnClick()
    {
        if (!_isCollected)
        {
            _rewardCount = (int)(_coins.CoinsPerVisit * _buyer.BonusRate);
            _coins.AddCoins(_rewardCount);

            _text.text = _rewardCount.ToString();
            _button.interactable = false;
            _animation.Play();

            _isCollected = true;
            Destroy(gameObject, 1.5f);
        }
    }
}
