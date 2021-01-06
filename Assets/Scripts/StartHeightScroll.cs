using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HeightAnchor
{
    Default,
    Vendings,
    WonderfulFurniture
}

public class StartHeightScroll : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;

    [Range(0, 1)]
    [SerializeField] float _normalizedPosition = 1;


    public void SetScrollPosition()
    {
        StartCoroutine(SetPosition());
    }

    public void SetNormilizedPosition(HeightAnchor heightAnchor)
    {
        switch (heightAnchor)
        {
            case HeightAnchor.Default:
                _normalizedPosition = 1f;
                break;
            case HeightAnchor.Vendings:
                _normalizedPosition = 0f;
                break;
            case HeightAnchor.WonderfulFurniture:
                _normalizedPosition = 0.065f;
                break;
        }
    }

    private IEnumerator SetPosition()
    {
        yield return new WaitForSeconds(0.4f);
        _scrollRect.verticalNormalizedPosition = _normalizedPosition;
        yield return new WaitForSeconds(0.4f);
        _scrollRect.verticalNormalizedPosition = _normalizedPosition;
    }

}
