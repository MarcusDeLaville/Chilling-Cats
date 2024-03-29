﻿using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SlidePanel : Panel
{
    [SerializeField] private Image _panelSubstrate;
    [SerializeField] private GameObject _panelObject;

    [Range(0, 1)]
    [SerializeField] private float _normalAlpha = 0.53f;

    [Range(0, 1)]
    [SerializeField] private float _hideAlpha = 0f;

    [SerializeField] private float _downPoint = 50f;
    
    private void Start()
    {
        _panelObject.transform.position = Vector2.down * _downPoint;
        _panelSubstrate.DOFade(_hideAlpha, 0.01f);
        
    }

    public override void ShowPanel()
    {
        _panelObject.transform.DOMove(Vector2.zero, 0.5f);
        _panelObject.transform.DOPunchScale(Vector2.one * 0.05f, 0.3f, 10, 1).SetDelay(0.4f);
        _panelSubstrate.DOFade(_normalAlpha, 0.5f).SetDelay(0.4f);
        PanelShowed?.Invoke();
    }

    public override void HidePanel()
    {
        _panelSubstrate.DOFade(_hideAlpha, 0.4f);
        _panelObject.transform.DOMove(Vector2.down * _downPoint, 0.4f).SetDelay(0.4f);
        PanelHided?.Invoke();
    }
}
