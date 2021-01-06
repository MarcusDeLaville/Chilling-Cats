using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class StoreItem : MonoBehaviour
{
    [SerializeField] protected int Price;
    [SerializeField] protected string Name;
    [SerializeField] protected bool IsBought;

    public virtual void OnTap()
    {

    }    
}
