using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ContextItem : MonoBehaviour
{
    [SerializeField] private float _targetScale = 0f;
    [SerializeField] private float _duration = 0.21f;
    [SerializeField] private float _delay = 0.4f;
    [SerializeField] private Panel _itemPanel;

    private Transform _itemTransform;
    private float _normalScale;

    private void Start()
    {
        _itemTransform = GetComponent<Transform>();
        _normalScale = _itemTransform.localScale.x;

        _itemTransform.localScale = Vector3.zero;
    }

    private void OnEnable()
    {
        _itemPanel.PanelShowed += Show;
        _itemPanel.PanelHided += Hide;
    }

    private void OnDisable()
    {
        _itemPanel.PanelShowed -= Show;
        _itemPanel.PanelHided -= Hide;
    }

    private void Show()
    {
        _itemTransform.DOScale(_normalScale, _duration).SetDelay(_delay);
    }

    private void Hide()
    {
        _itemTransform.DOScale(_targetScale, _duration);
    }
}
