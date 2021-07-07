using System;
using UnityEngine;
using UnityEngine.Events;

public class Panel : MonoBehaviour
{
    public UnityAction PanelShowed;
    public UnityAction PanelHided;


    public virtual void ShowPanel()
    {
        
    }

    public virtual void HidePanel()
    {
        
    }
}
