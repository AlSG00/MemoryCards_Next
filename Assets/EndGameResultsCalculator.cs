using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameResultsCalculator : MonoBehaviour
{
    [SerializeField] private NEW_GameProgression _gameProgressiong;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private PlayerMoney _playerMoney;

    private int _score;
    private int _roundsSurvived;
    private int _buttonsRemaining;
    private float _hoursElapsed;
    private int _minutesElapsed;
    private int _secondsElapsed;
    private int _itemsRemaining;
    private int _finalScore;
    private int _reward;
    private float _rewardMultplier;

    public int Score { get => _score; }

    public void CalculateResults()
    {
        _score = _gameProgressiong.score;
        _roundsSurvived = _gameProgressiong.currentRound;
        _buttonsRemaining = _playerMoney.CurrentGameMoney;
        _hoursElapsed = Mathf.Floor((float)_gameProgressiong.ElapsedPlayTime.Elapsed.TotalHours);
        _minutesElapsed = _gameProgressiong.ElapsedPlayTime.Elapsed.Minutes;
        _secondsElapsed = _gameProgressiong.ElapsedPlayTime.Elapsed.Seconds;
        _itemsRemaining = _inventory.GetItemsInInventoryCount();
    }
}
