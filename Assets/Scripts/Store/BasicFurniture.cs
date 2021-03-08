using System;
using UnityEngine;
using UnityEngine.UI;

public class BasicFurniture : StoreItem
{
    [SerializeField] private Furniture _furniture;
    [SerializeField] private Coins _coins;

    
    [SerializeField] private SpriteRenderer _furnitureSpriteRenderer;

    //
    [SerializeField] private Text _textPrice;
    //

    public Furniture Furniture => _furniture;
    public event Action OnTapEvent;
    
    private void Start()
    {
        if(PlayerPrefs.HasKey(_furniture.SaveKey + _furniture.Stage))
        {
            IsBought = true;
        }

        if (PlayerPrefs.GetInt(_furniture.SaveKey + _furniture.Stage) == 1)
        {
            IsBought = true;
        }

        SetPrice();
        
        if (PlayerPrefs.GetInt(_furniture.SaveKey) == _furniture.Stage)
        {
            SetFurniture();
            // _textPrice.color = _activeColor;
        }
    }

    public override void OnTap()
    {
        if (!IsBought)
        {
            if (_coins.GetCoins() >= _furniture.Price)
            {
                _coins.DepriveCoins(_furniture.Price);
                _coins.AddHearts(_furniture.Reward);

                _coins.AddPricePerVisit(_furniture.Reward);
                IsBought = true;
                PlayerPrefs.SetInt(_furniture.SaveKey + _furniture.Stage, 1);

                SetPrice();
                SetFurniture();
            }
        }
        else
        {
            SetFurniture();
        }
        
        OnTapEvent?.Invoke();
    }

    private void SetFurniture()
    {
        if (IsBought)
        {
            /*for(int i = 0; i < _allPosition.Length; i++)
            {
                if(_allPosition[i]._type == _type && _allPosition[i]._isBought)
                {
                    _allPosition[i]._textPrice.color = _defaultColor; 
                }
            }*/

            _furnitureSpriteRenderer.sprite = _furniture.FurnitureSprite;
            // _textPrice.color = _activeColor;
            _textPrice.text = "✔";
            PlayerPrefs.SetInt(_furniture.SaveKey, _furniture.Stage);
        }
    }

    private void SetPrice()
    {
        if (IsBought)
        {
            _textPrice.text = "✔";
            // _textPrice.color = _defaultColor;
        }
        else
        {
            _textPrice.text = _furniture.Price.ToString();
        }    
    }
}
