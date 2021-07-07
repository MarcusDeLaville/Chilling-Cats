using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class TapButton : MonoBehaviour
{
    [SerializeField] private float _rotateMultiplier = 35;
    [SerializeField] private float _duration = 0.3f;
    
    private Vector3 _normalScale;
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
        _buttonTransform.DORotate(Vector3.forward * _rotateMultiplier, _duration, RotateMode.Fast);
        _buttonTransform.DORotate(Vector3.zero, _duration, RotateMode.Fast).SetDelay(_duration);
        // _buttonTransform.DOPunchRotation(Vector3.forward * _rotateMultiplier, 0.5f, 5, 1f);
    }
}
