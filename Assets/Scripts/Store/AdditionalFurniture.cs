using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AdditionalFurniture : StoreItem
{
    [SerializeField] private int _growth;

    [SerializeField] private GameObject _furniture;

    [SerializeField] private Text _textPrice;

    [SerializeField] private Coins _coins;
    [SerializeField] private Sounds _sounds;

    private void Start()
    {
        if (PlayerPrefs.GetInt(Name) == 1)
        {
            IsBought = true;
        }

        SetPrice();

        if (IsBought)
        {
            SetFurniture();
        }
        else
        {
            _furniture.SetActive(false);
        }

    }

    public override void OnTap()
    {
        if (!IsBought)
        {
            if (_coins.GetCoins() >= Price)
            {
                _coins.DepriveCoins(Price);
                _coins.AddHeartsPerMinutes(_growth);

                IsBought = true;
                PlayerPrefs.SetInt(Name, 1);

                SetPrice();
                SetFurniture();
                _sounds.EffectSound(1);
            }
            else
            {
                _sounds.EffectSound(2);
            }
        }
  
    }

    private void SetFurniture()
    {
        if (IsBought)
        {
            _furniture.SetActive(true);
        }
    }

    private void SetPrice()
    {
        if (IsBought)
        {
            _textPrice.text = "✔";
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
} 
