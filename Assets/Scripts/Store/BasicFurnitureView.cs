using UnityEngine;
using UnityEngine.UI;

public class BasicFurnitureView : MonoBehaviour
{
    [SerializeField] private BasicFurniture _basicFurniture;

    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _defaultColor;

    private void OnEnable()
    {
        _basicFurniture.OnTapEvent += Draw;
    }

    private void OnDisable()
    {
        _basicFurniture.OnTapEvent -= Draw;
    }

    private void Draw()
    {
        ProductStatDrawer.Instance.Draw(_basicFurniture.Furniture.Price);
    }
}
