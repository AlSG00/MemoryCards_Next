using UnityEngine;

public class EndGameResultsCalculator : MonoBehaviour
{
    [SerializeField] private GameProgression _gameProgressiong;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private PlayerMoney _playerMoney;

    public int[] FinalScoreValuesArray { get; private set; }
    public int Score { get; private set; }
    public int RoundsSurvived { get; private set; }
    public int ButtonsRemaining { get; private set; }
    public float HoursElapsed { get; private set; }
    public int MinutesElapsed { get; private set; }
    public int SecondsElapsed { get; private set; }
    public int ItemsRemaining { get; private set; }
    public float Reward { get; private set; }
    public float BonusReward { get; private set; }
    public int MultipliedReward { get; private set; }
    public float RewardMultplier { get; private set; }

    private void Awake()
    {
        FinalScoreValuesArray = new int[5];
    }

    private void OnEnable()
    {
        SetDifficultyButton.DifficultyPicked += ResetValues;
        BonusCoins.GiveBonusCoins += GetBonusCoins;
    }

    private void OnDisable()
    {
        SetDifficultyButton.DifficultyPicked -= ResetValues;
        BonusCoins.GiveBonusCoins -= GetBonusCoins;
    }

    private void ResetValues()
    {
        BonusReward = 0;
    }

    private void GetBonusCoins(int coins)
    {
        BonusReward += coins;
        Debug.Log($"Bonus reward = {BonusReward}");
    }

    public void CalculateResults()
    {
        Score = _gameProgressiong.score;
        RoundsSurvived = _gameProgressiong.currentRound;
        if (_gameProgressiong.IsGameLost)
        {
            RoundsSurvived--; // TODO: Is it works correct?
        }

        ButtonsRemaining = _playerMoney.CurrentGameMoney;
        HoursElapsed = Mathf.Floor((float)_gameProgressiong.ElapsedPlayTime.Elapsed.TotalHours);
        MinutesElapsed = _gameProgressiong.ElapsedPlayTime.Elapsed.Minutes;
        SecondsElapsed = _gameProgressiong.ElapsedPlayTime.Elapsed.Seconds;
        ItemsRemaining = _inventory.GetItemsInInventoryCount();
        CalculateFinalScoreValue();
        CalculateRewardMultiplierValue();
        CalculateRewardValue();

        _playerMoney.MainMoney += (int)Reward;
    }

    private void CalculateFinalScoreValue()
    {
        // FinalScore is score + RoundsSurvived * bonus + buttonsRemaining + ItemsRemaining * bonus;
        FinalScoreValuesArray[0] = Score;
        FinalScoreValuesArray[1] = FinalScoreValuesArray[0] + RoundsSurvived * 100;
        FinalScoreValuesArray[2] = FinalScoreValuesArray[1] + ButtonsRemaining;
        FinalScoreValuesArray[3] = FinalScoreValuesArray[2] + ItemsRemaining * 100; 
    }

    private void CalculateRewardValue()
    {
        Reward = Mathf.Ceil((FinalScoreValuesArray[3] / 10)) + BonusReward;
        MultipliedReward = (int)Mathf.Ceil(Reward * RewardMultplier);
    }

    private void CalculateRewardMultiplierValue()
    {
        RewardMultplier = 1 + 0.01f * RoundsSurvived + 0.05f * (int)GameProgression.StartLayoutDifficulty;

        if (_gameProgressiong.IsGameLost)
        {
            RewardMultplier /= 2;
        }
    }
}
