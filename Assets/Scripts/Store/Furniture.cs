using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Furniture : StoreItem
{
    [SerializeField] private int _stage;
    [SerializeField] private int _maxStages;
    [SerializeField] private int _growth;
    [SerializeField] private int _reward;
    [SerializeField] private bool _isSkin = false;

    [SerializeField] private Sprite _furnitureSprite;
    [SerializeField] private SpriteRenderer _furnitureSpriteRenderer;

    [SerializeField] private Text _textPrice;
    [SerializeField] private Image _itemImage;
    [SerializeField] private Coins _coins;
    [SerializeField] private Sounds _sounds;

    [SerializeField] private Furniture[] _allPosition;

    private Color _defaultColor = new Color(0.233f, 0.216f, 0.202f, 0.255f);
    private Color _activeColor = new Color(1f, 0.847f, 0.560f, 1.0f);
 

    private void Start()
    {
        _itemImage.sprite = _furnitureSprite;

        if (PlayerPrefs.GetInt(Name + _stage) == 1)
        {
            IsBought = true;
        }

        SetPrice();

        if (PlayerPrefs.GetInt(Name) == _stage)
        {
            SetFurniture();
            _textPrice.color = _activeColor;
        }

        if (!IsBought)
        {
            PlayerPrefs.SetInt(Name + _stage, 0);
        }

        CheakIsSkin();
    }

    public override void OnTap()
    {
        if (!IsBought)
        {
            if (_coins.GetCoins() >= Price)
            {
                _coins.AddHearts(_reward);
                _coins.DepriveCoins(Price);

                _coins.AddPricePerVisit(_growth);
                IsBought = true;
                PlayerPrefs.SetInt(Name + _stage, 1);

                SetPrice();
                SetFurniture();
                _sounds.EffectSound(1);
            }
            else
            {
                _sounds.EffectSound(2);
            }
        }

        CheakIsSkin();

        if (_isSkin)
        {
            SetFurniture();
        } 
    }

    private void SetFurniture()
    {
        if (IsBought)
        {
            PlayerPrefs.SetInt(Name, _stage);

            for (int i = 0; i < _allPosition.Length; i++)
            {
                if(_allPosition[i].Name == Name && _allPosition[i].IsBought)
                {
                    _allPosition[i]._textPrice.color = _defaultColor; 
                }
            }  
            _furnitureSpriteRenderer.sprite = _furnitureSprite;
            _textPrice.color = _activeColor;
        }
    }

    private void SetPrice()
    {
        if (IsBought)
        {
            _textPrice.text = "✔";
            _textPrice.color = _defaultColor;
        }
        else
        {
            if (Price == 0)
            {
                _textPrice.text = "Бесплатно";
            }
            else
            {
                _textPrice.text = Price.ToString();
            }       
        }    
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }

    private void CheakIsSkin()
    {
        bool exitTrigger = false;
       
        for (int i = 1; i < _maxStages; i++)
        {
            if (exitTrigger)
            {
                break;
            }

            if(PlayerPrefs.GetInt(Name + i) == 0)
            {
                _isSkin = false;
                exitTrigger = true;
            } 
            else if(PlayerPrefs.GetInt(Name + i) == 1)
            {
                _isSkin = true;
            }
        }
                
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }

    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.Save();
    }
}
