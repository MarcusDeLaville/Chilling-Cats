using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum WalkingState
{
    Walk,
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

    private bool _isWalking = true;
    private bool _inPlace = false;
    private bool _isLeaved = false;

    private void Start()
    {
        _isWalking = true;

        _spawner = FindObjectOfType<CatSpawner>();

        _enterPoint = _spawner.EnterPoint();
        _exitPoint = _spawner.ExitPoint();
        _walkPoints = _spawner.GetWaypoints();

        _transform = GetComponent<Transform>();

        WalkToPoint(_enterPoint);
        StartCoroutine(Walking());
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

    public void ChangeWalkState(WalkingState walkingState)
    {
        if(walkingState == WalkingState.Stand)
        {
            _isWalking = false;
        }
        else if (walkingState == WalkingState.Walk)
        {
            _isWalking = true;
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
                _inPlace = true;
            }
            if (_inPlace)
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

    private float CheckDistanceSqure()
    {
        float distanceSqure = Vector2.Distance(_transform.position, _targetPosition.position);
        return distanceSqure;
    }
}
