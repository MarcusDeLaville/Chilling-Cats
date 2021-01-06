using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CatType
{
    Обычный,
    Средний,
    Хороший,
    Великолепный
}

public class Buyer : MonoBehaviour
{
    [SerializeField] private CatType _catType;
    [SerializeField] private List<Recipe> _desiredRecipes;
    [SerializeField] private int _minimumPostions;
    [SerializeField] private int _maximumPositions;

    public VendingType _desiredType { get; private set; }
    public float _bonusRate { get; private set; }
    public Recipe _desiredDish { get; private set; }

    private Recipes _recipes;

    private void Start()
    {
        _recipes = FindObjectOfType<Recipes>();
        SetRate();
    }

    public void SetDesiredType(VendingType vendingType)
    {
        _desiredType = vendingType;
    }

    public void SetDesiredDish()
    {
        _desiredDish = _recipes.DesiredRecipes(_desiredType)[Random.Range(_minimumPostions, _maximumPositions)];
        print(_desiredDish.RecipeName);
    }

    private void SetRate()
    {
        switch (_catType)
        {
            case CatType.Обычный:
                _bonusRate = 1f;
                break;
            case CatType.Средний: 
                _bonusRate = 1.25f;
                break;
            case CatType.Хороший:
                _bonusRate = 1.5f;
                break;
            case CatType.Великолепный:
                _bonusRate = 1.75f;
                break;

        }
    }
}
