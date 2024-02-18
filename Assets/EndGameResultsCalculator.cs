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
    //public int FinalScoreBasic;
    //public int FinalScorePlusRounds;
    //public int FinalScore_;
    //public int FinalScore;
    public float Reward;
    public float RewardMultplier;

    public int[] FinalScoreValuesArray = new int[5];

    //public int Score { get => _score; }
    //public int Score { get; private set; }
    //public 

    public void CalculateResults()
    {
        Score = _gameProgressiong.score;
        RoundsSurvived = _gameProgressiong.currentRound - 1;
        ButtonsRemaining = _playerMoney.CurrentGameMoney;
        HoursElapsed = Mathf.Floor((float)_gameProgressiong.ElapsedPlayTime.Elapsed.TotalHours);
        MinutesElapsed = _gameProgressiong.ElapsedPlayTime.Elapsed.Minutes;
        SecondsElapsed = _gameProgressiong.ElapsedPlayTime.Elapsed.Seconds;
        ItemsRemaining = _inventory.GetItemsInInventoryCount();
        CalculateFinalScoreValue();
        CalculateRewardMultiplierValue();
        CalculateRewardValue();
        _playerMoney.CurrentGameMoney = -5;
    }

    private void CalculateFinalScoreValue()
    {
        FinalScoreValuesArray[0] = Score;
        FinalScoreValuesArray[1] = FinalScoreValuesArray[0] + RoundsSurvived * 100;
        FinalScoreValuesArray[2] = FinalScoreValuesArray[1] + ButtonsRemaining;
        FinalScoreValuesArray[3] = FinalScoreValuesArray[2] + ItemsRemaining * 100;
        FinalScoreValuesArray


        //FinalScoreBasic = Score;

        //FinalScoreWithRounds = RoundsSurvived * 100;
        //FinalScoreWithButtons = 
        //FinalScore += Score + RoundsSurvived * 100 + ButtonsRemaining + ItemsRemaining * 100;
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
