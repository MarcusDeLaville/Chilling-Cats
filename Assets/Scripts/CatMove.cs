using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum WalkingState
{
    Walk,
    WalkToVending,
    Stand
}

public class CatMove : MonoBehaviour
{
    private Transform _enterPoint;
    private Transform _exitPoint;
    private Transform[] _walkPoints;
    private Transform[] _vendingPoins = new Transform[3];

    private Transform _transform;
    private Transform _targetPosition;

    [SerializeField] private float _speed;

    [SerializeField] private CatSpawner _spawner;

    [SerializeField] private bool _isWalking = true;

    public bool InPlace { get; private set; } = false;
    private bool _isLeaved = false;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _targetPosition = _transform;
        _spawner = FindObjectOfType<CatSpawner>();

        _enterPoint = _spawner.EnterPoint();
        _exitPoint = _spawner.ExitPoint();
        _walkPoints = _spawner.GetWaypoints();

        WalkToPoint(_enterPoint);
        ChangeWalkState(WalkingState.Walk);
    }

    private void FixedUpdate()
    {
        _transform.position = Vector2.MoveTowards(_transform.position, _targetPosition.position, Time.deltaTime * _speed);
    }

    public void WalkToPoint(Transform point)
    { 
        _targetPosition = point;     
    }

    public void Leave()
    {
        StartCoroutine(GetOut());
    }

    public void ChangeWalkState(WalkingState walkingState, VendingType vendingType = VendingType.Milk)
    {
        if(walkingState == WalkingState.Stand)
        {
            _isWalking = false;
        }
        else if (walkingState == WalkingState.WalkToVending)
        {
            _isWalking = false;
            StartCoroutine(WalkingToVending(vendingType));
        }
        else if (walkingState == WalkingState.Walk)
        {
            _isWalking = true;
            StartCoroutine(Walking());
        }
    }

    private IEnumerator GetOut()
    {
        _isWalking = false;

        while (!_isLeaved)
        {
            WalkToPoint(_enterPoint);
            if (CheckDistanceSqure() < 0.3)
            {
                InPlace = true;
            }
            if (InPlace)
            {
                WalkToPoint(_exitPoint);
                if (CheckDistanceSqure() < 0.3)
                {
                    if (CheckDistanceSqure() < 0.4)
                    {
                        _isLeaved = true;
                    }
                }
            }

            yield return new WaitForSeconds(1);
        }

        Destroy(gameObject);
    }

    private IEnumerator Walking()
    {
        while (_isWalking)
        {
            if (CheckDistanceSqure() < 0.3)
            {
                WalkToPoint(_walkPoints[Random.Range(0, _walkPoints.Length)]);          
            }
            yield return new WaitForSeconds(Random.Range(3, 4));
        }
    }

    private IEnumerator WalkingToVending(VendingType vendingType)
    {
        Transform targetVending = _spawner.VendingPosition(vendingType);
        _isWalking = false;

        while (!InPlace)
        {
            WalkToPoint(targetVending);

            if (CheckDistanceSqure(targetVending) < 0.3)
            {
                InPlace = true;
            }

            yield return new WaitForSeconds(1);
        }

        InPlace = false;
    }

    private float CheckDistanceSqure(Transform transform = null)
    {
        if(transform != null)
        {
            _targetPosition = transform;
        }

        float distanceSqure = Vector2.Distance(_transform.position, _targetPosition.position);
        return distanceSqure;
    }


}
