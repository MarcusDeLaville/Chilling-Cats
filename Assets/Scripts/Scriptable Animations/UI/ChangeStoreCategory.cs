using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChangeStoreCategory : MonoBehaviour
{
    [SerializeField] private ContextPanel _currentCategoryPanel;
    [SerializeField] private Text _categoryText;
    
    public void SetCategory(StoreCategory category)
    {
        if (_categoryText.text != category.CategoryName)
        {
            _categoryText.DOText(category.CategoryName, 0.5f, true, ScrambleMode.Lowercase);
        }

        if (_currentCategoryPanel != null)
        {
            if(_currentCategoryPanel != category.CategoryPanel)
            {
                _currentCategoryPanel.HidePanel();
            }
            
            category.CategoryPanel.ShowPanel();
        }
        else
        {
            category.CategoryPanel.ShowPanel();
        }

        _currentCategoryPanel = category.CategoryPanel;
    }
}
