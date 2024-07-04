using System;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private int _mainMoney;
    [SerializeField] private int _currentGameMoney;

    public int MainMoney
    {
        get => _mainMoney;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            _mainMoney = value;
        }
    }

    public int CurrentGameMoney { get => _currentGameMoney; }

    public static event Action<int> OnMoneyAmountChanged;

    private void OnEnable()
    {
        GameProgression.AddCurrentMoney += AddCurrentGameMoney;
        GameProgression.ResetCurrentMoney += SaveAndClear;
        ScaleSuspend.OnSuspendGame += SaveAndClear;
        BonusButtons.GiveBonusButtons += AddCurrentGameMoney;
    }

    private void OnDisable()
    {
        GameProgression.AddCurrentMoney -= AddCurrentGameMoney;
        GameProgression.ResetCurrentMoney -= SaveAndClear;
        ScaleSuspend.OnSuspendGame -= SaveAndClear;
        BonusButtons.GiveBonusButtons -= AddCurrentGameMoney;
    }

    internal bool IsEnoughtMainMoney(int value)
    {
        if (value > _mainMoney)
        {
            return false;
        }

        return true;
    }

    internal bool IsEnoughCurrentGameMoney(int value)
    {
        if (value > _currentGameMoney)
        {
            return false;
        }

        return true;
    }

    internal void AddCurrentGameMoney(int amount)
    {
        _currentGameMoney += amount;
        OnMoneyAmountChanged?.Invoke(_currentGameMoney);
    }

    internal void GetCurrentGameMoney(int itemPrice)
    {
        _currentGameMoney -= itemPrice;
        OnMoneyAmountChanged?.Invoke(_currentGameMoney);
    }

    private void SaveAndClear()
    {
        _currentGameMoney = 0;
        OnMoneyAmountChanged?.Invoke(_currentGameMoney);
    }
}
