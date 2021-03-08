using UnityEngine;

[CreateAssetMenu(fileName = "New Furniture", menuName = "Furniture/Create new furniture", order = 10)]
public class Furniture : ScriptableObject
{
    [Header("General Settings")]
    [SerializeField] private int _price;
    [SerializeField] private int _stage;
    [SerializeField] private int _growth;
    [SerializeField] private int _reward;
    [SerializeField] private Sprite _furnitureSprite;
    [Space]
    [SerializeField] private string _saveKey;

    public int Price => _price;
    public int Stage => _stage;
    public int Growth => _growth;
    public int Reward => _reward;
    public Sprite FurnitureSprite => _furnitureSprite;
    public string SaveKey => _saveKey;
}
