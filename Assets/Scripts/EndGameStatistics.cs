using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameStatistics : MonoBehaviour
{
    [SerializeField] private NEW_GameProgression _gameProgressiong;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private PlayerMoney _playerMoney;

    [SerializeField] private GameObject[] TextObjectArray;

    //[SerializeField] private TextMeshProUGUI[] _uiElementsCollection;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _roundsSurvivedText;
    [SerializeField] private TextMeshProUGUI _buttonsRemainingText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _itemsText;
    [SerializeField] private TextMeshProUGUI _rewardText;
    [SerializeField] private TextMeshProUGUI _rewardMultiplierText;

    private void OnEnable()
    {
        NEW_GameProgression.OnGameFinished += Show;
        StartButton.OnGameStart += Hide;
        RejectStartButton.OnGameStartReject += Hide;
    }

    private void OnDisable()
    {
        NEW_GameProgression.OnGameFinished -= Show;
        StartButton.OnGameStart -= Hide;
        RejectStartButton.OnGameStartReject -= Hide;
    }

    private void Start()
    {
        Hide();
    }

    private void Show()
    {
        //ResetTextValues();
        SetVisibilityLevel(1);

        foreach (var textMesh in TextObjectArray)
        {
            textMesh.SetActive(true);
        }

        SetTextValues();
    }

    private void Hide()
    {
        SetVisibilityLevel(0);

        foreach (var textMesh in TextObjectArray)
        {
            textMesh.SetActive(false);
        }
    }

    private void SetVisibilityLevel(float level)
    {
        //foreach (var textMesh in _uiElementsCollection)
        //{
        //    element.color = new Color(
        //        element.color.r,
        //        element.color.g,
        //        element.color.b,
        //        level
        //        );
        //}

        
    }

    private void ResetTextValues()
    {
        _scoreText.text = "";
        _roundsSurvivedText.text = "";
        _buttonsRemainingText.text = "";
        _timeText.text = "";
        _itemsText.text = "";
        _rewardText.text = "";
        _rewardMultiplierText.text = "";
    }

    private void SetTextValues()
    {
        _scoreText.text = _gameProgressiong.score.ToString(); // TODO: move score to separate script
        _roundsSurvivedText.text = _gameProgressiong.currentRound.ToString();
        _buttonsRemainingText.text = _playerMoney.CurrentGameMoney.ToString();
        _timeText.text = "-not_implemented-";
        _itemsText.text = "";
        _rewardText.text = "";
        _rewardMultiplierText.text = "";
    }
}
