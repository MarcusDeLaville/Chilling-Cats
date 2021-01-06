using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum CloudState
{
    Show,
    Hide
}


public class WishCloud : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Animator _cloudAnimator;
    [SerializeField] private SpriteRenderer _productOfWish;

    [SerializeField] private Buyer _buyer;
    [SerializeField] private Cat _cat;

    [SerializeField] private GameObject _lackDesired;

    private Recipe _desiredProduct;
    private Recipes _playerRecipes;

    public bool _isReplyed { get; private set; } = false;

    private void Start()
    {
        _playerRecipes = FindObjectOfType<Recipes>(); 
    }

    public void OnPointerClick(PointerEventData eventData)
    {   
        if (_playerRecipes.GetPlayerRecipes.Contains(_desiredProduct))
        {
            _isReplyed = true;
        }
        else
        { 
            _productOfWish.gameObject.SetActive(false);
            _lackDesired.SetActive(true);
        }

        StartCoroutine(SwitchCloudState(CloudState.Hide));
    }

    public void ChooseDesiredProduct(VendingType vendingType)
    {
        StartCoroutine(SwitchCloudState(CloudState.Show));

        _buyer.SetDesiredDish();
        _desiredProduct = _buyer._desiredDish;
        _productOfWish.sprite = _desiredProduct.RecipeIcon;
    }

    public Recipe DesiredRecipe { get => _desiredProduct;}

    public void HideCloud()
    {
        StartCoroutine(SwitchCloudState(CloudState.Hide));
    }

    private IEnumerator SwitchCloudState(CloudState cloudState)
    {
        yield return new WaitForSeconds(1);

        if (cloudState == CloudState.Show)
        {
            _cloudAnimator.SetBool("isWished", true);
        }
        else if(cloudState == CloudState.Hide)
        {
            _cloudAnimator.SetBool("isWished", false);
        }  
    }
    
}
