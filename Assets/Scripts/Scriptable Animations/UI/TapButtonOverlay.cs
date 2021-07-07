using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class TapButtonOverlay : MonoBehaviour
{
    [SerializeField] private float _targetScale = 0.8f;
    [SerializeField] private float _duration = 0.21f;
    
    private Button _button;
    private Transform _buttonTransform;
    private float _normalScale;
    
    private void Start()
    {
        _button = GetComponent<Button>();
        _buttonTransform = _button.transform;
        _normalScale = _buttonTransform.localScale.x;
        _button.onClick.AddListener(TapAction);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(TapAction);
    }

    private void TapAction()
    {
        _buttonTransform.DOScale(_targetScale, _duration);
        _buttonTransform.DOScale(_normalScale, _duration).SetDelay(_duration);
    }
}