using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using System;

public class EndGameScreen : MonoBehaviour
{
    [SerializeField] private NEW_GameProgression _gameProgressiong;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private PlayerMoney _playerMoney;
    [SerializeField] private EndGameResultsCalculator _resultCalculator;

    [SerializeField] private TextMeshProUGUI _scoreValueText;
    [SerializeField] private TextMeshProUGUI _scoreLogo;
    [SerializeField] private TextMeshProUGUI _roundsSurvivedValueText;
    [SerializeField] private TextMeshProUGUI _roundsSurvivedLogo;
    [SerializeField] private TextMeshProUGUI _buttonsRemainingValueText;
    [SerializeField] private TextMeshProUGUI _buttonsRemainingLogo;
    [SerializeField] private TextMeshProUGUI _timeValueText;
    [SerializeField] private TextMeshProUGUI _timeLogo;
    [SerializeField] private TextMeshProUGUI _itemsRemainingValueText;
    [SerializeField] private TextMeshProUGUI _itemsRemainingLogo;
    [SerializeField] private TextMeshProUGUI _finalScoreValueText;
    [SerializeField] private TextMeshProUGUI _finalScoreLogo;
    [SerializeField] private TextMeshProUGUI _rewardValueText;
    [SerializeField] private TextMeshProUGUI _rewardLogo;
    [SerializeField] private TextMeshProUGUI _rewardMultiplierValueText;
    [SerializeField] private TextMeshProUGUI _rewardMultiplierLogo;
    [SerializeField] private TextMeshProUGUI _newItemsAvailableLogo;

    [SerializeField] private float _flickerEffectDuration;
    [SerializeField] private float _counterEffectDuration;
    [SerializeField] private int _nextElementShowDelay;

    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _toMenuButton;

    [SerializeField] private TextMeshProUGUI[] _textElementsArray;

    private void Awake()
    {
        _textElementsArray = new[] {
            _scoreValueText,
            _scoreLogo,
            _roundsSurvivedValueText,
            _roundsSurvivedLogo,
            _buttonsRemainingValueText,
            _buttonsRemainingLogo,
            _timeValueText, 
            _timeLogo,
            _itemsRemainingValueText,
            _itemsRemainingLogo,
            _finalScoreValueText,
            _finalScoreLogo,
            _rewardMultiplierValueText,
            _rewardMultiplierLogo,
            _rewardValueText,
            _rewardLogo,
            _newItemsAvailableLogo,
        };

        HideImmediately();
        //_restartButton.SetActive(false);
        //_toMenuButton.SetActive(false);
    }

    private void OnEnable()
    {
        NEW_GameProgression.OnGameFinished += DisplayFullStatistics;
        StartButton.OnGameStart += Hide;
        RejectStartButton.OnGameStartReject += Hide;
    }

    private void OnDisable()
    {
        NEW_GameProgression.OnGameFinished -= DisplayFullStatistics;
        StartButton.OnGameStart -= Hide;
        RejectStartButton.OnGameStartReject -= Hide;
    }

    private void Start()
    {
        
    }

    private async void DisplayFullStatistics()
    {
        //_resultCalculator.CalculateResults();

        ChangeTextElementVisibility(_scoreLogo, true, true);
        ChangeTextElementVisibility(_scoreValueText, true, true);
        SetTextElementValue(_scoreValueText, _gameProgressiong.score, true);
        await Task.Delay(_nextElementShowDelay);

        ChangeTextElementVisibility(_roundsSurvivedLogo, true, true);
        ChangeTextElementVisibility(_roundsSurvivedValueText, true, true);
        SetTextElementValue(_roundsSurvivedValueText, _gameProgressiong.currentRound, true);
        await Task.Delay(_nextElementShowDelay);

        ChangeTextElementVisibility(_buttonsRemainingLogo, true, true);
        ChangeTextElementVisibility(_buttonsRemainingValueText, true, true);
        SetTextElementValue(_buttonsRemainingValueText, _playerMoney.CurrentGameMoney, true);
        await Task.Delay(_nextElementShowDelay);

        ChangeTextElementVisibility(_timeLogo, true, true);
        ChangeTextElementVisibility(_timeValueText, true, true);
        SetTimeValueText();
        await Task.Delay(_nextElementShowDelay);

        ChangeTextElementVisibility(_itemsRemainingLogo, true, true);
        ChangeTextElementVisibility(_itemsRemainingValueText, true, true);
        //
        await Task.Delay(_nextElementShowDelay);

        ChangeTextElementVisibility(_finalScoreLogo, true, true);
        ChangeTextElementVisibility(_finalScoreValueText, true, true);

        await Task.Delay(_nextElementShowDelay);

        ChangeTextElementVisibility(_rewardMultiplierLogo, true, true);
        ChangeTextElementVisibility(_rewardValueText, true, true);
        await Task.Delay(_nextElementShowDelay);

        ChangeTextElementVisibility(_rewardLogo, true, true);
        ChangeTextElementVisibility(_rewardValueText, true, true);
        await Task.Delay(_nextElementShowDelay);

        _restartButton.SetActive(true);
        _toMenuButton.SetActive(true);
        SetTextElementVisibilityLevel(_restartButton.GetComponent<TextMeshProUGUI>(), 1);
        SetTextElementVisibilityLevel(_toMenuButton.GetComponent<TextMeshProUGUI>(), 1);
    }

    private void SetTextElementValue(TextMeshProUGUI textElement, int value, bool counterEffect, int startValue = 0)
    {
        if (counterEffect)
        {
            StartCoroutine(TextElementCounterEffectRoutine(textElement, startValue, value));
        }
        else
        {
            textElement.text = value.ToString();
        }
    }



    private void SetTimeValueText()
    {
        string hours = Mathf.Floor((float)_gameProgressiong.ElapsedPlayTime.Elapsed.TotalHours).ToString();
        string minutes = _gameProgressiong.ElapsedPlayTime.Elapsed.Minutes.ToString("D2");
        string seconds = _gameProgressiong.ElapsedPlayTime.Elapsed.Seconds.ToString("D2");

        _timeValueText.text = $"{hours}:{minutes}:{seconds}";
    }

    private void ChangeTextElementVisibility(TextMeshProUGUI textElement, bool enableElement, bool enableFlickering)
    {
        float visibility = 0f;
        if (enableElement)
        {
            visibility = 1;
        }

        if (enableFlickering)
        {
            StartCoroutine(TextElementFlickeringEffectRoutine(textElement, visibility));
        }
        else
        {
            SetTextElementVisibilityLevel(textElement, visibility);
        }
    }

    private void Hide()
    {
        //SetVisibilityLevel(0);

        //foreach (var textMesh in _textObjectArray)
        //{
        //    textMesh.SetActive(false);
        //}

    }

    private void HideImmediately()
    {
        foreach (var textElement in _textElementsArray)
        {
            SetTextElementVisibilityLevel(textElement, 0);
        }

        SetTextElementVisibilityLevel(_restartButton.GetComponent<TextMeshProUGUI>(), 0);
        SetTextElementVisibilityLevel(_toMenuButton.GetComponent<TextMeshProUGUI>(), 0);

        _restartButton.SetActive(false);
        _toMenuButton.SetActive(false);
    }



    private void ResetTextElementsValues()
    {
        _scoreValueText.text = "";
        _roundsSurvivedValueText.text = "";
        _buttonsRemainingValueText.text = "";
        _timeValueText.text = "";
        _itemsRemainingValueText.text = "";
        _rewardValueText.text = "";
        _rewardMultiplierValueText.text = "";
    }

    private IEnumerator TextElementFlickeringEffectRoutine(TextMeshProUGUI textElement, float resultValue)
    {
        float elapsedTime = 0;
        System.Random random = new System.Random();
        while (elapsedTime <= _flickerEffectDuration)
        {
            elapsedTime += 0.05f;
            Debug.Log($"{elapsedTime} : {_flickerEffectDuration}");
            SetTextElementVisibilityLevel(textElement, GenerateRandomFloat(0f, 1f));
            yield return new WaitForSeconds(0.05f);
        }

        SetTextElementVisibilityLevel(textElement, resultValue);
        //yield return null;
    }

    private IEnumerator TextElementCounterEffectRoutine(TextMeshProUGUI textElement, int startValue, int finalValue)
    {
        int currentValue = startValue;
        int valueIncreaseStep = (int)((finalValue - startValue) / (_counterEffectDuration * 20));
        while (currentValue < finalValue)
        {
            //effectCurrentValue += effectStep;
            textElement.text = currentValue.ToString();
            currentValue += valueIncreaseStep;
            yield return new WaitForSeconds(0.05f);   
        }

        textElement.text = finalValue.ToString();
        //yield return null;
    }

    private void SetTextElementVisibilityLevel(TextMeshProUGUI textElement, float level)
    {
        textElement.color = new Color(
            textElement.color.r,
            textElement.color.g,
            textElement.color.b,
            level
            );
    }

    private float GenerateRandomFloat(float min, float max)
    {
        System.Random random = new System.Random();
        double val = (random.NextDouble() * (max - min) + min);
        return (float)val;
    }
}
