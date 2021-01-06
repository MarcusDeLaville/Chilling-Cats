using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CatAction
{
    Walking,
    Leaves,
    Buys
}

public class Cat : MonoBehaviour
{
    [SerializeField] private UnityEvent _buyEvent;
    [SerializeField] private CatMove _catMove;
    [SerializeField] private SpawnCoin _spawnCoin;

    private CatSpawner _catSpawner;
    [SerializeField]private CatAction _catActions;

    private void Start()
    {
        _catSpawner = FindObjectOfType<CatSpawner>();
    }

    private void OnValidate()
    {
        SetAction(_catActions);
    }

    private void SetAction(CatAction catAction)
    {
        _catActions = catAction;

        switch (_catActions)
        {
            case CatAction.Walking:
                _catMove.ChangeWalkState(WalkingState.Walk);
                break;
            case CatAction.Buys:
                _catMove.ChangeWalkState(WalkingState.Stand);
                _catMove.WalkToPoint(_catSpawner.VendingPosition(VendingType.Coffee));
                _spawnCoin.SpawnCoins();
                break;
            case CatAction.Leaves:
                _catMove.Leave();
                break;
        }
    }



}
