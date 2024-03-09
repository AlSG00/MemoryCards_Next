using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using System;

public class EndGameScreen : MonoBehaviour
{
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

    [SerializeField] private GraphicRaycaster _canvasRaycaster;

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
    }

    private void OnEnable()
    {
        NEW_GameProgression.LoseGame += DisplayFullStatisticsDelayed;
        NEW_GameProgression.OnGameFinished += DisplayFullStatistics;
        StartButton.StartPressed += Hide;
        RejectStartButton.OnGameStartReject += Hide;
    }

    private void OnDisable()
    {
        NEW_GameProgression.LoseGame -= DisplayFullStatisticsDelayed;
        NEW_GameProgression.OnGameFinished -= DisplayFullStatistics;
        StartButton.StartPressed -= Hide;
        RejectStartButton.OnGameStartReject -= Hide;
    }

    private async void DisplayFullStatisticsDelayed()
    {
        await Task.Delay(5000);

        DisplayFullStatistics();
    }

    private async void DisplayFullStatistics()
    {
        _canvasRaycaster.enabled = true;

        _resultCalculator.CalculateResults();

        ChangeTextElementVisibility(_scoreLogo, true, true);
        ChangeTextElementVisibility(_scoreValueText, true, true);
        ChangeTextElementVisibility(_finalScoreLogo, true, true);
        ChangeTextElementVisibility(_finalScoreValueText, true, true);
        SetTextElementValue(_scoreValueText, _resultCalculator.Score, true);
        SetTextElementValue(_finalScoreValueText, _resultCalculator.FinalScoreValuesArray[0], true);
        await Task.Delay(_nextElementShowDelay);

        ChangeTextElementVisibility(_roundsSurvivedLogo, true, true);
        ChangeTextElementVisibility(_roundsSurvivedValueText, true, true);
        SetTextElementValue(_roundsSurvivedValueText, _resultCalculator.RoundsSurvived, true);
        SetTextElementValue(_finalScoreValueText, _resultCalculator.FinalScoreValuesArray[1], true, _resultCalculator.FinalScoreValuesArray[0]);
        await Task.Delay(_nextElementShowDelay);

        ChangeTextElementVisibility(_buttonsRemainingLogo, true, true);
        ChangeTextElementVisibility(_buttonsRemainingValueText, true, true);
        SetTextElementValue(_buttonsRemainingValueText,_resultCalculator.ButtonsRemaining, true);
        SetTextElementValue(_finalScoreValueText, _resultCalculator.FinalScoreValuesArray[2], true, _resultCalculator.FinalScoreValuesArray[1]);
        await Task.Delay(_nextElementShowDelay);

        ChangeTextElementVisibility(_timeLogo, true, true);
        ChangeTextElementVisibility(_timeValueText, true, true);
        SetTimeValueText();
        await Task.Delay(_nextElementShowDelay);

        ChangeTextElementVisibility(_itemsRemainingLogo, true, true);
        ChangeTextElementVisibility(_itemsRemainingValueText, true, true);
        SetTextElementValue(_itemsRemainingValueText, _resultCalculator.ItemsRemaining, true);
        SetTextElementValue(_finalScoreValueText, _resultCalculator.FinalScoreValuesArray[3], true, _resultCalculator.FinalScoreValuesArray[2]);
        await Task.Delay(_nextElementShowDelay);

        ChangeTextElementVisibility(_rewardLogo, true, true);
        ChangeTextElementVisibility(_rewardValueText, true, true);
        SetTextElementValue(_rewardValueText, _resultCalculator.Reward, true);
        await Task.Delay(_nextElementShowDelay);

        ChangeTextElementVisibility(_rewardMultiplierLogo, true, true);
        ChangeTextElementVisibility(_rewardMultiplierValueText, true, true);
        SetRewardMultiplierValueText();
        SetTextElementValue(_rewardValueText, _resultCalculator.MultipliedReward, true, _resultCalculator.Reward);
        await Task.Delay(_nextElementShowDelay);

        _restartButton.SetActive(true);
        _toMenuButton.SetActive(true);
        ChangeTextElementVisibility(_restartButton.GetComponent<TextMeshProUGUI>(), true, true);
        ChangeTextElementVisibility(_toMenuButton.GetComponent<TextMeshProUGUI>(), true, true);
    }

    private void SetTextElementValue(TextMeshProUGUI textElement, float value, bool counterEffect = false, float startValue = 0, float valueChangeStep = 1)
    {
        if (counterEffect && value != startValue)
        {
            StartCoroutine(TextElementCounterEffectRoutine(textElement, startValue, value, valueChangeStep));
        }
        else
        {
            textElement.text = value.ToString();
        }
    }

    private void SetRewardMultiplierValueText()
    {
        _rewardMultiplierValueText.text = _resultCalculator.RewardMultplier.ToString("F2");
    }

    private void SetTimeValueText()
    {
        string hours = _resultCalculator.HoursElapsed.ToString();
        string minutes = _resultCalculator.MinutesElapsed.ToString("D2");
        string seconds = _resultCalculator.SecondsElapsed.ToString("D2");

        if (_resultCalculator.HoursElapsed == 0)
        {
            _timeValueText.text = $"{minutes}:{seconds}";
        }
        else
        {
            _timeValueText.text = $"{hours}:{minutes}:{seconds}";
        }
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
        HideImmediately();
    }

    private void HideImmediately()
    {
        _canvasRaycaster.enabled = false;
        foreach (var textElement in _textElementsArray)
        {
            SetTextElementVisibilityLevel(textElement, 0);
        }

        SetTextElementVisibilityLevel(_restartButton.GetComponent<TextMeshProUGUI>(), 0);
        SetTextElementVisibilityLevel(_toMenuButton.GetComponent<TextMeshProUGUI>(), 0);

        _restartButton.SetActive(false);
        _toMenuButton.SetActive(false);
    }

    // TODO: Probabaly dedicate to class
    private IEnumerator TextElementFlickeringEffectRoutine(TextMeshProUGUI textElement, float resultValue)
    {
        float elapsedTime = 0;
        System.Random random = new System.Random();
        while (elapsedTime <= _flickerEffectDuration)
        {
            elapsedTime += 0.05f;
            SetTextElementVisibilityLevel(textElement, GenerateRandomFloat(0f, 1f));
            yield return new WaitForSeconds(0.05f);
        }

        SetTextElementVisibilityLevel(textElement, resultValue);
    }

    // TODO: Probabaly dedicate to class
    private IEnumerator TextElementCounterEffectRoutine(TextMeshProUGUI textElement, float startValue, float finalValue, float valueChangeStep)
    {
        int step = 0;
        int stepsCount = Mathf.Abs((int)((finalValue - startValue) / valueChangeStep));
        float nextStepDelay = _counterEffectDuration / stepsCount; 
        int currentValue = (int)startValue;
        int valueChangeStepInt = Mathf.Abs((int)valueChangeStep);
        if (finalValue < startValue)
        {
            valueChangeStepInt *= (-1);
        }

        while (step < stepsCount)
        {
            step++;
            textElement.text = currentValue.ToString();
            currentValue += valueChangeStepInt;
            yield return new WaitForSeconds(nextStepDelay);   
        }

        textElement.text = finalValue.ToString();
    }

    // TODO: Probabaly dedicate to class
    private void SetTextElementVisibilityLevel(TextMeshProUGUI textElement, float level)
    {
        textElement.color = new Color(
            textElement.color.r,
            textElement.color.g,
            textElement.color.b,
            level
            );
    }

    // TODO: Probabaly dedicate to class
    private float GenerateRandomFloat(float min, float max)
    {
        System.Random random = new System.Random();
        double val = (random.NextDouble() * (max - min) + min);
        return (float)val;
    }
}
