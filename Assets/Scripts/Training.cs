using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TypeTrainingStage
{
    OpenPanel,
    ClosePanel,
    BuyFurniture,
    BuyRecipe,
    AdverCustomer,
    ServeCustomer
}

public class Training : MonoBehaviour
{
    [SerializeField] private int _currentTrainigStage;
    private bool _trainingComplete;
    private int _currentServedCustomers;
    [SerializeField] private List<StageTraining> _stagesTraining;

    [SerializeField] private PlayerStatistic _playerStatistic;
    [SerializeField] private Recipes _playerRecipes;
    [SerializeField] private RectTransform _trainingFrame;
    [SerializeField] private TrainingHint _trainingHint;
    [SerializeField] private Advertise _advertise;

    [SerializeField] private GameObject[] _trainingObjects;

    [SerializeField] private GameObject _welcomePanel;
    [SerializeField] private GameObject _finishPanel;

    [SerializeField] private Menu _menu;
    [SerializeField] private StartHeightScroll _heightScroll;

    public bool IsTrainigComplite => _trainingComplete;

    private void Awake()
    {
        _currentTrainigStage = PlayerPrefs.GetInt("CurrentTrainigStage");
    }

    private void Start()
    {
        if (_currentTrainigStage >= _stagesTraining.Count)
        {
            _trainingComplete = true;
            _trainingFrame.localScale *= 0f;
            StartCoroutine(TraningDestroy());
        }

        if (_currentTrainigStage == 0)
        {
            _menu.OpenPanel(_welcomePanel);
        }

        StartCoroutine(CheakStagesTraining());
    }

    private IEnumerator CheakStagesTraining()
    {
        while (!_trainingComplete)
        {
            yield return new WaitUntil(() => PlayerPrefs.GetInt("isGreeted") == 1);

            SetCurrentStage();
            _currentServedCustomers = _playerStatistic.CurrentStatisticsValue(QuestType.CaterCustomers);

            switch (_stagesTraining[_currentTrainigStage].TypeTrainingStage)
            {
                case TypeTrainingStage.BuyFurniture:
                    yield return new WaitUntil(() => PlayerPrefs.GetInt(_stagesTraining[_currentTrainigStage].FurnitureKey) == 1);
                    break;
                case TypeTrainingStage.BuyRecipe:
                    yield return new WaitUntil(() => _playerRecipes.GetPlayerRecipes.Contains(_stagesTraining[_currentTrainigStage].Recipe));
                    break;
                case TypeTrainingStage.ClosePanel:
                    yield return new WaitUntil(() => _stagesTraining[_currentTrainigStage].PanelAnimator.GetBool("isOpen") == false);
                    break;
                case TypeTrainingStage.OpenPanel:
                    yield return new WaitUntil(() => _stagesTraining[_currentTrainigStage].PanelAnimator.GetBool("isOpen") == true);
                    break;
                case TypeTrainingStage.ServeCustomer:
                    yield return new WaitUntil(() => _playerStatistic.CurrentStatisticsValue(QuestType.CaterCustomers) == _currentServedCustomers + 1);
                    break;
                case TypeTrainingStage.AdverCustomer:
                    yield return new WaitUntil(() => _advertise.CurrentTapsCount == _advertise.RequiredTapsCount - 1);
                    break;            
            }

            yield return new WaitForSeconds(0.1f);
            _currentTrainigStage++;
            CheakFinishTraning();
        }
    }

    private void CheakFinishTraning()
    {
        PlayerPrefs.SetInt("CurrentTrainigStage", _currentTrainigStage);

        if (_currentTrainigStage >= _stagesTraining.Count)
        {
            _menu.OpenPanel(_finishPanel);
            _trainingComplete = true;
            _trainingFrame.localScale *= 0f;
            StartCoroutine(TraningDestroy());
        }
        else if (_currentTrainigStage == 13)
        {
            _heightScroll.SetNormilizedPosition(HeightAnchor.WonderfulFurniture);
        }
        else if (_currentTrainigStage == 5)
        {
            _heightScroll.SetNormilizedPosition(HeightAnchor.Vendings);
        }
        else
        {
            _heightScroll.SetNormilizedPosition(HeightAnchor.Default);
        }

        _heightScroll.SetScrollPosition();

    }

    private IEnumerator TraningDestroy()
    {
        yield return new WaitForSeconds(1f);

        for(int i = 0; i < _trainingObjects.Length; i++)
        {
            Destroy(_trainingObjects[i]);
        }
    }

    private void SetCurrentStage()
    {
        _trainingHint.SwapHint(_stagesTraining[_currentTrainigStage].HintStage);
        RectTransform templateRectTransform = _stagesTraining[_currentTrainigStage].TargetRectTransform;
     
        _trainingFrame.localScale = templateRectTransform.localScale;
        _trainingFrame.anchoredPosition = templateRectTransform.anchoredPosition;
        _trainingFrame.anchorMin = templateRectTransform.anchorMin;
        _trainingFrame.anchorMax = templateRectTransform.anchorMax;
        _trainingFrame.localScale = _trainingFrame.localScale * 45 * _stagesTraining[_currentTrainigStage].FrameOffset;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("CurrentTrainigStage", _currentTrainigStage);
    }

    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.SetInt("CurrentTrainigStage", _currentTrainigStage);
    }

    [Serializable]
    public struct StageTraining
    {
        public string HintStage;
        public TypeTrainingStage TypeTrainingStage;
        public RectTransform TargetRectTransform;
        public Animator PanelAnimator;
        public string FurnitureKey;
        public Recipe Recipe;
        public float FrameOffset;
    }
}
