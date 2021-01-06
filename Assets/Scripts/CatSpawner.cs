using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VendingType
{
    Milk,
    Fish,
    Coffee
}

public class CatSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _walkPoints = new Transform[10];
    [SerializeField] private Transform _enterPoint;
    [SerializeField] private Transform _exitPoint;

    [SerializeField] private Transform _coffeeVendingPoint;
    [SerializeField] private Transform _fishVendingPoint;
    [SerializeField] private Transform _milkVendingPoint;

    private Transform _vendingTransform;

    public void SpawnCat(GameObject prefab, Transform position)
    {
        Instantiate(prefab, position.position, Quaternion.identity);
    }

    public Transform[] GetWaypoints()
    {
        return _walkPoints;
    }

    public Transform EnterPoint()
    {
        return _enterPoint;
    }

    public Transform ExitPoint()
    {
        return _exitPoint;
    }

    public Transform VendingPosition(VendingType vendingType)
    {
        _vendingTransform = null;

        switch (vendingType)
        {
            case VendingType.Coffee:
                _vendingTransform = _coffeeVendingPoint;
                break;
            case VendingType.Milk:
                _vendingTransform = _milkVendingPoint;
                break;
            case VendingType.Fish:
                _vendingTransform = _fishVendingPoint;
                break;
        }

        return _vendingTransform;
    }
}
