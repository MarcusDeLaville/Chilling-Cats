using UnityEngine;
using DG.Tweening;

public class ContextPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panelObject;
    [SerializeField] private CanvasGroup _panelCanvsGroup;

    private bool _isShow = false;
    
    private void Start()
    {
        _panelObject.transform.localScale = Vector3.zero;
        _panelCanvsGroup.alpha = 0;
    }

    public void ShowPanel()
    {
        _panelObject.transform.DOScale(1f, 0.2f);
        _panelCanvsGroup.DOFade(1f, 0.2f);
        if (_isShow == true)
        {
            _panelObject.transform.DOPunchScale(Vector3.one * 0.05f, 0.3f, 10, 1).SetDelay(0.2f);
        }

        _isShow = true;
    }

    public void HidePanel()
    {
        _panelObject.transform.DOScale(0.8f, 0.2f);
        _panelCanvsGroup.DOFade(0f, 0.2f);
        _isShow = false;
    }
}
