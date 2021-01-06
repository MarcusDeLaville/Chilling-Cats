using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum QuestType
{
    BuyFurniture,
    CollectCoins,
    CollectHearts,
    CaterCustomers,
    BuyRecipe,
    SellProducts
}

public enum QuestsState
{
    Active,
    Complete,
    AllQuestsCompleted,
}


public class QuestsGiver : MonoBehaviour
{
    [SerializeField] private Text _questDiscription;
    [SerializeField] private Text _questRewardCount;

    [SerializeField] private Text _progressText;

    [SerializeField] private Image _questsSpriteRenderer;
    [SerializeField] private Sprite _spriteActiveQuest;
    [SerializeField] private Sprite _spriteCompleteQuest;

    [SerializeField] private Button _collectButton;

    public void SetQuest(string description, int count)
    {
        _questDiscription.text = description;
        _questRewardCount.text = count.ToString();
    }

    public void SetProgressText(int currentCount, int requiredCount)
    {
        if (currentCount > requiredCount)
        {
            currentCount = requiredCount;
        }

        _progressText.text = currentCount + "/" + requiredCount;
    }

    public void SetQuestsState(QuestsState questsState)
    {
        if(questsState == QuestsState.Active)
        {
            _questsSpriteRenderer.sprite = _spriteActiveQuest;
            _collectButton.interactable = false;
        }
        else if(questsState == QuestsState.Complete)
        {
            _questsSpriteRenderer.sprite = _spriteCompleteQuest;
            _collectButton.interactable = true;
        }
        else if (questsState == QuestsState.AllQuestsCompleted)
        {
            _collectButton.interactable = false;
            _questsSpriteRenderer.gameObject.SetActive(false);
        }
    }

    
}
