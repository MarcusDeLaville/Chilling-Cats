using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CatType
{
    Обычный,
    Средний,
    Хороший,
    Эпический
}

public class Buyer : MonoBehaviour
{
    [SerializeField] private CatType _catType;
    public float BonusRate { get; private set; }

    private void Start()
    {
        SetRate();
    }

    private void SetRate()
    {
        switch (_catType)
        {
            case CatType.Обычный:
                BonusRate = 1f;
                break;
            case CatType.Средний:
                BonusRate = 1.1f;
                break;
            case CatType.Хороший:
                BonusRate = 1.25f;
                break;
            case CatType.Эпический:
                BonusRate = 1.75f;
                break;

        }
    }

    
}
