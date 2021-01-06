using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : StoreItem
{
    [Header("Parametrs")]
    [SerializeField] private Sprite _recipeIcon;
    [SerializeField] private int _dishPrice;

    [Header("Components")]

    [SerializeField] private Text _priceText;
    [SerializeField] private Text _nameText;
    [SerializeField] private Image _recipeImage;
    [SerializeField] private Button _buttonRecipe;

    [SerializeField] private Coins _coins;
    [SerializeField] private Recipes _recipes;
    [SerializeField] private Sounds _sounds;

    public Sprite RecipeIcon => _recipeIcon;
    public int RecipePrice => Price;
    public int DishPrice => _dishPrice;
    public string RecipeName => Name;

    private void Start()
    {
        _recipeImage.sprite = _recipeIcon;
        _nameText.text = Name;

        SetPrice();
    }

    public override void OnTap()
    {
        if (!IsBought)
        {
            if (_coins.GetCoins() >= Price)
            {
                _coins.DepriveCoins(Price);
                _recipes.AddRecipes(this);
                IsBought = true;
                _sounds.EffectSound(1);
            }
            else
            {
                _sounds.EffectSound(2);
            }
        }

        SetPrice();
    }

    public void SetPrice()
    {
        if (IsBought)
        {
            _priceText.text = "✔";
            _buttonRecipe.interactable = false;
        }
        else
        {
            if (Price == 0)
            {
                _priceText.text = "Бесплатно";
            }
            else
            {
                _priceText.text = Price.ToString();
            }
        }
    }

    public void SetBoughtStatus(bool status)
    {
        IsBought = status;
        SetPrice();
    }
}
