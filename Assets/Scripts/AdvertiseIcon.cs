using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvertiseIcon : MonoBehaviour
{
    [SerializeField] private Image _adsImage;

    [SerializeField] private Sprite _listingSprite;
    [SerializeField] private Sprite _bannerSprite;
    [SerializeField] private Sprite _tvSprite;
    [SerializeField] private Sprite _internetSprite;
    [SerializeField] private Sprite _spaceinternetSprite;

    private void Start()
    {
        SetIcon();
    }

    public void SetIcon()
    {
        int level = PlayerPrefs.GetInt("Ad");
        Sprite iconSprite;

        switch (level)
        {
            case 1:
                iconSprite = _listingSprite;
                break;
            case 2:
                iconSprite = _bannerSprite;
                break;
            case 3:
                iconSprite = _tvSprite;
                break;
            case 4:
                iconSprite = _internetSprite;
                break;
            case 5:
                iconSprite = _spaceinternetSprite;
                break;
            default:
                iconSprite = _listingSprite;
                break;
        }

        _adsImage.sprite = iconSprite;
    }

}
