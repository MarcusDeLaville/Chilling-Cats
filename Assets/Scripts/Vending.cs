using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vending : MonoBehaviour
{
    [SerializeField] private Recipe[] _vendindRecipes;
    [SerializeField] private VendingType _vendingType;
    [SerializeField] private Animation _buyAnimation;
    [SerializeField] private PlayerStatistic _playerStatistic;

    [SerializeField] private CustomersControl _customersControl;

    private Coroutine _vendingWork;

    //private void Start()
    //{
    //    StartCoroutine(VendingWork());
    //}

    private void OnEnable()
    {
        StartCoroutine(VendingWork());
    }

    private IEnumerator VendingWork()
    {

        while (true)
        {    
            yield return new WaitUntil(() => _customersControl.Customers.Count > 0);
            Cat customer = _customersControl.Customers[Random.Range(0, _customersControl.Customers.Count)];

            customer.SetAction(CatAction.Buys, _vendingType);
            customer._buyer.SetDesiredType(_vendingType);

            yield return new WaitUntil(() => customer.AtVending);

            yield return new WaitForSeconds(3f);

            if (customer._wishCloud._isReplyed)
            {
                _buyAnimation.Play();

                yield return new WaitForSeconds(2f);
                
                customer.TakePayment(customer._wishCloud.DesiredRecipe.DishPrice);
                customer.SetBuyingState(true);

                _playerStatistic.AddStatisticValue(QuestType.SellProducts, 1);
            }
            else
            {
                customer.SetBuyingState(true);
                customer._wishCloud.HideCloud();
            }

            yield return new WaitForSeconds(1.5f);
        }
    }


}
