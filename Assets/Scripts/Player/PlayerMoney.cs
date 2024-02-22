using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private int _mainMoney;
    [SerializeField] private int _currentGameMoney;

    public int MainMoney { 
        get => _mainMoney; 
        set
        {
            if (value < 0 || (_mainMoney - value) < 0)
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
        NEW_GameProgression.AddCurrentMoney += AddCurrentGameMoney;
        NEW_GameProgression.ResetCurrentMoney += SaveAndClear;
        ScaleSuspend.OnSuspendGame += SaveAndClear;
    }

    private void OnDisable()
    {
        NEW_GameProgression.AddCurrentMoney -= AddCurrentGameMoney;
        NEW_GameProgression.ResetCurrentMoney += SaveAndClear;
        ScaleSuspend.OnSuspendGame -= SaveAndClear;
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

    //internal void AddMainMoney(int amount)
    //{
    //    _currentGameMoney += amount;
    //    OnMoneyAmountChanged?.Invoke(_currentGameMoney);
    //    sdfiku;
    //}

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
