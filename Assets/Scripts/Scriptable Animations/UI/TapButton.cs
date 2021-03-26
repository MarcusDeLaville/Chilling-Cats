using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class TapButton : MonoBehaviour
{
    [SerializeField] private float _rotateMultiplier = 35;
    
    
    private Button _button;
    private Transform _buttonTransform;

    private void Start()
    {
        _button = GetComponent<Button>();
        _buttonTransform = _button.transform;
        _button.onClick.AddListener(TapAction);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(TapAction);
    }

    private void TapAction()
    {
        _buttonTransform.DOPunchRotation(Vector3.forward * _rotateMultiplier, 0.5f, 5, 1f);
    }
}
