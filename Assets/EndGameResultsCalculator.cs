using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameResultsCalculator : MonoBehaviour
{
    [SerializeField] private NEW_GameProgression _gameProgressiong;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private PlayerMoney _playerMoney;

    public int Score;
    public int RoundsSurvived;
    public int ButtonsRemaining;
    public float HoursElapsed;
    public int MinutesElapsed;
    public int SecondsElapsed;
    public int ItemsRemaining;
    public int FinalScore;
    public float Reward;
    public float RewardMultplier;

    //public int Score { get => _score; }
    //public int Score { get; private set; }
    //public 

    public void CalculateResults()
    {
        Score = _gameProgressiong.score;
        RoundsSurvived = _gameProgressiong.currentRound;
        ButtonsRemaining = _playerMoney.CurrentGameMoney;
        HoursElapsed = Mathf.Floor((float)_gameProgressiong.ElapsedPlayTime.Elapsed.TotalHours);
        MinutesElapsed = _gameProgressiong.ElapsedPlayTime.Elapsed.Minutes;
        SecondsElapsed = _gameProgressiong.ElapsedPlayTime.Elapsed.Seconds;
        ItemsRemaining = _inventory.GetItemsInInventoryCount();
        CalculateFinalScoreValue();
        CalculateRewardMultiplierValue();
        CalculateRewardValue();
    }

    private void CalculateFinalScoreValue()
    {
        FinalScore += Score + RoundsSurvived * 100 + ButtonsRemaining + ItemsRemaining * 100;
    }

    private void CalculateRewardValue()
    {
        Reward =  Mathf.Ceil((FinalScore / 10) * RewardMultplier);
    }

    private void CalculateRewardMultiplierValue()
    {
        RewardMultplier = 1 + 0.01f * RoundsSurvived;

        if (_gameProgressiong.IsGameLost)
        {
            RewardMultplier = (RewardMultplier / 2) + (RewardMultplier % 2);
        }
    }
}
