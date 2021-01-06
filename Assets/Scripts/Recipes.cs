using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct RecipesSave
{
    public List<string> RecipeName;
}

public class Recipes : MonoBehaviour
{
    [SerializeField] private List<Recipe> _allRecipes;

    [SerializeField] private List<Recipe> _milkRecipes;
    [SerializeField] private List<Recipe> _coffeeRecipes;
    [SerializeField] private List<Recipe> _fishRecipes; 
    [SerializeField] private List<Recipe> _playerRecipes;

    [SerializeField] private string _savePath;
    [SerializeField] private string _saveFileName = "data.json";

    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _savePath = Path.Combine(Application.persistentDataPath, _saveFileName);
#else
        _savePath = Path.Combine(Application.dataPath, _saveFileName);

#endif
        LoadConfig();

    }

    private void SaveConfig()
    {
        List<string> playerRecipesNames = new List<string>();

        for (int i = 0; i < _playerRecipes.Count; i++)
        {
            playerRecipesNames.Add(_playerRecipes[i].RecipeName);
        }

        RecipesSave _recipesSave = new RecipesSave
        {
            RecipeName = playerRecipesNames
        };

        string json = JsonUtility.ToJson(_recipesSave, true);
        File.WriteAllText(_savePath, json);
    }

    private void LoadConfig()
    {
        if (!File.Exists(_savePath))
        {
            //SaveConfig();
            return;
        }

        string json = File.ReadAllText(_savePath);
        RecipesSave _coinsFromJson = JsonUtility.FromJson<RecipesSave>(json);

        for (int i = 0; i < _allRecipes.Count - 1; i++)
        {
            if (_coinsFromJson.RecipeName.Contains(_allRecipes[i].RecipeName))
            {
                _playerRecipes.Add(_allRecipes[i]);
                _allRecipes[i].SetBoughtStatus(true);
            }
        }

       
    } 

    private void OnApplicationQuit()
    {
        SaveConfig();
    }

    private void OnApplicationPause(bool pause)
    {
        SaveConfig();
    }

    public void AddRecipes(Recipe recipe)
    {
        _playerRecipes.Add(recipe);
    }

    public List<Recipe> GetPlayerRecipes { get => _playerRecipes;}

    public List<Recipe> DesiredRecipes(VendingType vendingType)
    {
        List<Recipe> dishes;

        switch (vendingType)
        {
            case VendingType.Milk:
                dishes = _milkRecipes;
                break;
            case VendingType.Coffee:
                dishes = _coffeeRecipes;
                break;
            case VendingType.Fish:
                dishes = _fishRecipes;
                break;
            default:
                dishes = _milkRecipes;
                break;
        }
        return dishes;
    }
}
