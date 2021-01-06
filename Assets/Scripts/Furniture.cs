using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Furniture : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private int _stage;
    [SerializeField] private int _maxStages;
    [SerializeField] private int _growth;
    [SerializeField] private int _reward;
    [SerializeField] private bool _isBought;
    public string _type;

    [SerializeField] private bool _isSkin = false;

    [SerializeField] private Sprite _furnitureSprite;
    [SerializeField] private SpriteRenderer _furnitureSpriteRenderer;

    [SerializeField] private Text _textPrice;

    [SerializeField] private Furniture[] _allPosition;

    private Color _defaultColor = new Color(0.233f, 0.216f, 0.202f, 0.255f);
    private Color _activeColor = new Color(1f, 0.847f, 0.560f, 1.0f);

    private Coins _coins;

    private void Start()
    {
        _coins = FindObjectOfType<Coins>();
        _allPosition = FindObjectsOfType<Furniture>();

        if(PlayerPrefs.HasKey(_type + _stage))
        {
            _isBought = true;
        }

        for (int i = 0; i <= _maxStages; i++)
        {
            if (PlayerPrefs.HasKey(_type + i))
            {
                _isSkin = true;
            }
            else
            {
                _isSkin = false;
            }
        }

        if (PlayerPrefs.GetInt(_type + _stage) == 1)
        {
            _isBought = true;
        }

        SetPrice();

        if (PlayerPrefs.GetInt(_type) == _stage)
        {
            SetFurniture();
            _textPrice.color = _activeColor;
        }
    }

    public void OnTap()
    {
        if (!_isBought)
        {
            if (_coins.GetCoins() >= _price)
            {
                _coins.DepriveCoins(_price);
                _coins.AddHearts(_reward);

                _coins.AddPricePerVisit(_growth);
                _isBought = true;
                PlayerPrefs.SetInt(_type + _stage, 1);

                SetPrice();
                SetFurniture();
            }
        }

        if (_isSkin)
        {
            SetFurniture(); 
            _textPrice.text = "✔";
        } 
    }

    private void SetFurniture()
    {
        if (_isBought)
        {
            for(int i = 0; i < _allPosition.Length; i++)
            {
                if(_allPosition[i]._type == _type && _allPosition[i]._isBought)
                {
                    _allPosition[i]._textPrice.color = _defaultColor; 
                }
            }

            _furnitureSpriteRenderer.sprite = _furnitureSprite;
            _textPrice.color = _activeColor;
            PlayerPrefs.SetInt(_type, _stage);
        }
    }

    private void SetPrice()
    {
        if (_isBought)
        {
            _textPrice.text = "✔";
            _textPrice.color = _defaultColor;
        }
        else
        {
            _textPrice.text = _price.ToString();
        }    
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
}
