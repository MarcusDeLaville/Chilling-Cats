using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoin : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _parrent;

    private Transform _spawnPosition;
    private Buyer _buyer;

    private void Start()
    {
        _buyer = GetComponent<Buyer>();
        _parrent = FindObjectOfType<Canvas>().transform;
    }

    public void SpawnCoins()
    {
        GameObject newCoin = Instantiate(_coinPrefab, gameObject.transform.localPosition, Quaternion.identity, _parrent);
        newCoin.gameObject.GetComponent<Coin>()._buyer = _buyer;
    }
}
