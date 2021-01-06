using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
 

public class Quests : MonoBehaviour
{
    [SerializeField] private int _levelsComplited = 0;

    [SerializeField] private List<Quest> _quests;
    [SerializeField] private Quest _currentQuest;
    [SerializeField] private QuestsGiver _questsGiver;

    [SerializeField] private int _currentProgressCount;
    [SerializeField] private int _requiredCount;
    [SerializeField] private int _startCount;

    [SerializeField] private PlayerStatistic _playerStatistic;
    [SerializeField] private Recipes _playerRecipes;
    [SerializeField] private Coins _coins;

    [SerializeField] private bool _allQuestCompiled;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("QuestsLevel"))
        {
            SaveProgress();
        }

        PickProgress();
    }

    private void Start()
    {
        if(_requiredCount == 0)
        {
            PickNewQuest();
        }

        _currentQuest = _quests[_levelsComplited];
        CheakCompleteQuests();

        DrawCurrentQuest();

        if (!_allQuestCompiled)
        {
            StartCoroutine(CheakingProgress());
        }   
    }

    private void DrawCurrentQuest()
    {    
        _questsGiver.SetQuest(_currentQuest.QuestDiscription, _currentQuest.RewardCount);
        _questsGiver.SetQuestsState(QuestsState.Active);
    }

    private void PickNewQuest()
    {
        _currentQuest = _quests[_levelsComplited];

        if (_currentQuest.QuestType == QuestType.BuyFurniture)
        {
            _currentProgressCount = 0;
            _startCount = 0;
            _requiredCount = 1;
        }
        else if (_currentQuest.QuestType == QuestType.BuyRecipe)
        {
            // пофиксить бубляж 
            _currentProgressCount = 0;
            _startCount = 0;
            _requiredCount = 1;
        }
        else
        {
            _currentProgressCount = _playerStatistic.CurrentStatisticsValue(_currentQuest.QuestType);
            _startCount = _playerStatistic.CurrentStatisticsValue(_currentQuest.QuestType);
            _requiredCount = _playerStatistic.CurrentStatisticsValue(_currentQuest.QuestType) + _currentQuest.RequiredCount;
        }

        DrawCurrentQuest();
        CheakProgress();
    }

    private IEnumerator CheakingProgress()
    {
        while (!_allQuestCompiled)
        {
            CheakProgress();
            yield return new WaitForSeconds(3);
        }
    }

    public void CheakProgress()
    {
        if (_currentQuest.QuestType == QuestType.BuyFurniture)
        {
            if (PlayerPrefs.GetInt(_currentQuest.PlayPrefsKey) == 1)
            {
                _currentProgressCount = 1;
            }   
            else
            {
                _currentProgressCount = 0;
            }
        }
        else if (_currentQuest.QuestType == QuestType.BuyRecipe)
        {
            if (_playerRecipes.GetPlayerRecipes.Contains(_currentQuest.RequiredRecipe))
            {
                _currentProgressCount = 1;
            }
            else
            {
                _currentProgressCount = 0;
            }
        }
        else
        {
            _currentProgressCount = _playerStatistic.CurrentStatisticsValue(_currentQuest.QuestType);
        }

        if (_currentQuest.QuestType != QuestType.BuyRecipe && _currentQuest.QuestType != QuestType.BuyFurniture)
        {
            _questsGiver.SetProgressText(_currentProgressCount - _startCount, _currentQuest.RequiredCount);
        }
        else
        {
            _questsGiver.SetProgressText(_currentProgressCount, _currentQuest.RequiredCount);
        }
        
        if (_currentProgressCount >= _requiredCount)
        {
            _questsGiver.SetQuestsState(QuestsState.Complete);
        }
        else
        {
            _questsGiver.SetQuestsState(QuestsState.Active);
        }
    }

    private void CheakCompleteQuests()
    {
        if (_levelsComplited == _quests.Count)
        {
            _allQuestCompiled = true;
            _questsGiver.SetQuestsState(QuestsState.AllQuestsCompleted);
        }
    }

    public void CompleteQuests()
    {
        SaveProgress();
        _coins.AddCoins(_currentQuest.RewardCount);
        CheakCompleteQuests();

        if (_allQuestCompiled == false)
        {
            PickNewQuest();
            _levelsComplited++;
        }
    }

    private void OnApplicationQuit()
    {
        SaveProgress();
    }

    private void OnApplicationPause(bool pause)
    {
        SaveProgress();
    }

    private void SaveProgress()
    {
        PlayerPrefs.SetInt("QuestsLevel", _levelsComplited);
        PlayerPrefs.SetInt("CurrentProgressQuest", _currentProgressCount);
        PlayerPrefs.SetInt("RequiredCountQuests", _requiredCount);
        PlayerPrefs.SetInt("StartCount", _startCount);

        PlayerPrefs.Save();
    }

    private void PickProgress()
    {
        _levelsComplited = PlayerPrefs.GetInt("QuestsLevel");
        _currentProgressCount = PlayerPrefs.GetInt("CurrentProgressQuest");
        _requiredCount = PlayerPrefs.GetInt("RequiredCountQuests");
        _startCount = PlayerPrefs.GetInt("StartCount");
    }

    [Serializable]
    public struct Quest
    {
        public string QuestDiscription;
        public QuestType QuestType;
        public int RewardCount;
        public int RequiredCount;
        public string PlayPrefsKey;
        public Recipe RequiredRecipe;
    }
}
