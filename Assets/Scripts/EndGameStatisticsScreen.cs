using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameStatisticsScreen : MonoBehaviour
{
    [SerializeField] private NEW_GameProgression _gameProgressiong;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private PlayerMoney _playerMoney;

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
    [SerializeField] private TextMeshProUGUI _rewardValueText;
    [SerializeField] private TextMeshProUGUI _rewardLogo;
    [SerializeField] private TextMeshProUGUI _newItemsAvailableLogo;
    [SerializeField] private TextMeshProUGUI _rewardMultiplierValueText;
    [SerializeField] private TextMeshProUGUI _rewardMultiplierLogo;

    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _toMenuButton;

    private TextMeshProUGUI[] _textElementsArray;

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
            _rewardValueText,
            _newItemsAvailableLogo,
            _rewardMultiplierValueText,
            _rewardMultiplierLogo
        };

        _restartButton.SetActive(false);
        _toMenuButton.SetActive(false);
    }

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

        foreach (var textMesh in _textObjectArray)
        {
            textMesh.SetActive(true);
        }

        SetTextValues();
    }

    private void Hide()
    {
        SetVisibilityLevel(0);

        foreach (var textMesh in _textObjectArray)
        {
            textMesh.SetActive(false);
        }
    }

    private void HideImmediately()
    {
        foreach (var textElement in _textElementsArray)
        {
            SetElementVisibilityLevel(textElement, 0);
        }

        _restartButton.SetActive(false);
        _toMenuButton.SetActive(false);
    }

    private void SetElementVisibilityLevel(TextMeshProUGUI textElement, float level)
    {
        textElement.color = new Color(
            textElement.color.r,
            textElement.color.g,
            textElement.color.b,
            level
            );
    }

    private void ResetTextValues()
    {
        _scoreValueText.text = "";
        _roundsSurvivedValueText.text = "";
        _buttonsRemainingValueText.text = "";
        _timeValueText.text = "";
        _itemsRemainingValueText.text = "";
        _rewardValueText.text = "";
        _rewardMultiplierValueText.text = "";
    }

    private void SetTextValues()
    {
        _scoreText.text = _gameProgressiong.score.ToString(); // TODO: move score to separate script
        _roundsSurvivedText.text = _gameProgressiong.currentRound.ToString();
        _gameProgressiong.currentRound = 0;
        _buttonsRemainingText.text = _playerMoney.CurrentGameMoney.ToString();
        _timeText.text = "-not_implemented-";
        _itemsText.text = "";
        _rewardText.text = "";
        _rewardMultiplierText.text = "";
    }
}
