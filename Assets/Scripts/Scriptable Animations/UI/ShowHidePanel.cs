using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShowHidePanel : Panel
{
    [SerializeField] private GameObject _panelObject;
    [SerializeField] private CanvasGroup _panelCanvsGroup;

    private void Start()
    {
        _panelObject.transform.localScale = Vector3.zero;
        _panelCanvsGroup.alpha = 0;
    }

    public override void ShowPanel()
    {
        _panelObject.transform.DOScale(1f, 0.4f);
        _panelCanvsGroup.DOFade(1f, 0.5f);
        _panelObject.transform.DOPunchScale(Vector3.one * 0.085f, 0.2f, 10, 1f).SetDelay(0.3f);
        PanelShowed?.Invoke();
    }
    
    public override void HidePanel()
    {
        _panelCanvsGroup.DOFade(0f, 0.3f);
        _panelObject.transform.DOScale(0.8f, 0.4f);
        PanelHided?.Invoke();
    }
}
