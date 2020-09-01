using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Advertise : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private int _requiredTapsCount;
    [SerializeField] private int _tapsCount;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private CatSpawner _spawner;

    [SerializeField] private GameObject[] _catsPrefabs;

    private void Start()
    {
        _spawner = FindObjectOfType<CatSpawner>();
        _slider.maxValue = _requiredTapsCount;
    }

    public void Advert()
    {
        _tapsCount++;

        if(_tapsCount >= _requiredTapsCount)
        {
            _tapsCount = 0;
            _spawner.SpawnCat(_catsPrefabs[Random.Range(0, _catsPrefabs.Length)], _spawnPoint);
        }

        _slider.value = _tapsCount;
    }
}
