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
    [SerializeField] private CatMove _catMove;
    [SerializeField] private SpawnCoin _spawnCoin;

    [SerializeField] private int _minVisitTime, _maxVisitTime;
    [SerializeField] private int _buyingDelay;
    private int _visitTime;

    [SerializeField] private bool _isTrainingCat = false;

    private CatSpawner _catSpawner;
    private CustomersControl _customersControl;
    private CatAction _catActions;
    private Coroutine _catsBehest;
    private Sounds _sounds;
    private PlayerStatistic _playerStatistic;

    public bool _desireBuy = false;
    private bool _isPurchased = false;


    public WishCloud _wishCloud { get; private set; }
    public Buyer _buyer { get; private set; }

    public bool AtVending { get; private set; } = false;
    public bool _isBuys { get; private set; } = false;



    private void Start()
    {
        //переписать под фабрику

        _buyer = GetComponent<Buyer>();
        _wishCloud = GetComponentInChildren<WishCloud>();
        _customersControl = FindObjectOfType<CustomersControl>();
        _catSpawner = FindObjectOfType<CatSpawner>();
        _spawnCoin = FindObjectOfType<SpawnCoin>();
        _sounds = FindObjectOfType<Sounds>();
        _playerStatistic = FindObjectOfType<PlayerStatistic>();

        if (!_isTrainingCat)
        {
            _desireBuy = RandomBoolean();
        }
        else
        {
            _desireBuy = true;
        }

        _visitTime = Random.Range(_maxVisitTime, _maxVisitTime);

        _catsBehest = StartCoroutine(CatBehest());
    }

    public void SetAction(CatAction catAction, VendingType vendingType = VendingType.Milk)
    {
        _catActions = catAction;

        switch (_catActions)
        {
            case CatAction.Walking:
                _catMove.ChangeWalkState(WalkingState.Walk);
                break;
            case CatAction.Buys:
                _customersControl.RemoveCustomer(this);
                _catMove.ChangeWalkState(WalkingState.WalkToVending, vendingType);
                StopCoroutine(_catsBehest);
                StartCoroutine(CatsPurchase());
                break;
            case CatAction.Leaves:
                _playerStatistic.AddStatisticValue(QuestType.CaterCustomers, 1);
                _customersControl.RemoveCustomer(this);
                PayToVisit();
                _catMove.Leave();
                break;
        }
    }

    public void SetBuyingState(bool status)
    {
        _isBuys = status;
    } 

    private IEnumerator CatBehest()
    {    
        SetAction(CatAction.Walking);
        yield return new WaitForSeconds(_buyingDelay);

        if (_desireBuy)
        {
            _customersControl.AddCustomer(this);
        }

        yield return new WaitForSeconds(_visitTime - _buyingDelay);

        SetAction(CatAction.Leaves);
    }

    private IEnumerator CatsPurchase()
    {
        yield return new WaitUntil(() => _catMove.InPlace == true);
        _wishCloud.ChooseDesiredProduct(_buyer._desiredType);
        AtVending = true;

        yield return new WaitUntil(() => _isBuys == true);

        if(_wishCloud._isReplyed == false)
        {
            _sounds.EffectSound(3);
        }

        SetAction(CatAction.Walking);
        yield return new WaitForSeconds(3);
        SetAction(CatAction.Leaves);
    }



    private bool RandomBoolean()
    {
        bool randomBoolean = false;

        var randomNunber = Random.Range(0, 100);

        if (randomNunber >= 50)
        {
            randomBoolean = true;
        }
        else if (randomNunber < 50)
        {
            randomBoolean = false;
        }

        return randomBoolean;
    }

    private void OnDestroy()
    {
        if (_desireBuy)
        {
            _customersControl.RemoveCustomer(this);
        }
    }

    public void TakePayment(int price)
    {
        _spawnCoin.SpawnCoins(transform, PaymentType.Purchase, _buyer, price);
    }

    private void PayToVisit()
    {
        _spawnCoin.SpawnCoins(transform, PaymentType.Visit, _buyer);
    }

}
