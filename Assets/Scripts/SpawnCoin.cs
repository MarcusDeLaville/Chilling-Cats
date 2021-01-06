using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoin : MonoBehaviour
{
    [SerializeField] private Coins _coins;
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Transform _parent;

    [SerializeField] private float _maximumOffsetX;
    [SerializeField] private float _maximumOffsetY;

    private Vector3 _spawnOffset;

    public void SpawnCoins(Transform spawnPosition, PaymentType paymentType, Buyer buyer, int reward = 0)
    {
        _spawnOffset = RandomOffset();
        Coin newCoin = Instantiate(_coinPrefab, spawnPosition.localPosition + _spawnOffset, Quaternion.identity, _parent);

        switch (paymentType)
        {
            case PaymentType.Purchase:
                newCoin.SetCoins(_coins);
                newCoin.SetRewardCount(paymentType, reward);
                break;
            case PaymentType.Visit:
                newCoin.SetCoins(_coins);
                newCoin.SetBuyer(buyer);
                newCoin.SetRewardCount(paymentType);
                break;
        }
    }

    private Vector3 RandomOffset()
    {
        Vector3 templateVector;
        templateVector = new Vector3(Random.Range(-_maximumOffsetX, _maximumOffsetX), Random.Range(-_maximumOffsetY, _maximumOffsetY), 0);

        return templateVector;
    }

}
