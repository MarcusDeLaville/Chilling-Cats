using UnityEngine;

public class StoreCategory : MonoBehaviour
{
    [SerializeField] private string _categoryName;
    [SerializeField] private ContextPanel _categoryPanel;
    
    public string CategoryName => _categoryName;

    public ContextPanel CategoryPanel => _categoryPanel;
}
