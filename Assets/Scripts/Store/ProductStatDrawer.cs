using UnityEngine;
using UnityEngine.UI;

public class ProductStatDrawer : MonoBehaviour
{
    public static ProductStatDrawer Instance;

    [SerializeField] private Text _price;
    
    private void Start()
    {
        Instance = this;
    }

    public void Draw(int price)
    {
        _price.text = price.ToString();
    }
    
}
