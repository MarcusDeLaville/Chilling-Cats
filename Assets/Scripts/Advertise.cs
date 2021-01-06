using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Advertise : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private int _requiredTapsCount;
    [SerializeField] private int _tapsCount;

    public int RequiredTapsCount => _requiredTapsCount;
    public int CurrentTapsCount => _tapsCount;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private CatSpawner _spawner;

    [SerializeField] private List<GameObject> _catsPrefabs;
    [SerializeField] private float _spawnDelay = 4f;

    private void Start()
    {
        _slider.maxValue = _requiredTapsCount;
        StartCoroutine(PassiveAdvertising());
    }

    public void Advert()
    {
        _tapsCount++;

        if (_tapsCount >= _requiredTapsCount)
        {
            _tapsCount = 0;
            StartCoroutine(SpawnCustomer());
        }

        _slider.value = _tapsCount;
    }

    public void SetTapsCount(int requiredCount)
    {
        _requiredTapsCount = requiredCount;
        _slider.maxValue = requiredCount;
    }

    public void AddCustomers(List<GameObject> newCustomers)
    {
        for (int i = 0; i < newCustomers.Count; i++)
        {
            _catsPrefabs.Add(newCustomers[i]);
        }
    }

    public void SetSpawnDelay(float delay)
    {
        _spawnDelay = delay;
    }

    public void AdvertTurbo()
    {
        StartCoroutine(SpawnCustomerBus());
    }

    private IEnumerator PassiveAdvertising()
    {
        yield return new WaitForSeconds(_spawnDelay * 2.5f);
        _spawner.SpawnCat(_catsPrefabs[Random.Range(0, _catsPrefabs.Count)], _spawnPoint);
    }

    private IEnumerator SpawnCustomer()
    {
        yield return new WaitForSeconds(_spawnDelay);
        _spawner.SpawnCat(_catsPrefabs[Random.Range(0, _catsPrefabs.Count)], _spawnPoint);
    }

    private IEnumerator SpawnCustomerBus()
    {
        int i = 0;

        while (i < 15)
        {
            yield return new WaitForSeconds(Random.Range(0.3f, 0.6f));
            _spawner.SpawnCat(_catsPrefabs[Random.Range(0, _catsPrefabs.Count)], _spawnPoint);
            i++;
        }
    }
}
