using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomersControl : MonoBehaviour
{
    [SerializeField] private List<Cat> _customers;

    public void AddCustomer(Cat customer)
    {
        _customers.Add(customer);
        print(customer.name);
    }

    public void RemoveCustomer(Cat customer)
    {
        _customers.Remove(customer);
    }

    public List<Cat> Customers 
    {
        get => _customers;
    }
}
