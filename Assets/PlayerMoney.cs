using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private int _mainMoney;
    public int _currentGameMoney;

    public static event Action<int> OnMoneyAmountChanged;

    private void OnEnable()
    {
        NEW_GameProgression.OnAddMoney += AddMoney;
    }

    private void OnDisable()
    {
        NEW_GameProgression.OnAddMoney -= AddMoney;
    }

    private void AddMoney(int amount)
    {
        _currentGameMoney += amount;
        OnMoneyAmountChanged?.Invoke(_currentGameMoney);
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

    internal void GetCurrentGameMoney(int itemPrice)
    {
        _currentGameMoney -= itemPrice;
        Debug.Log($"Remaining money: {_currentGameMoney}");
        // TODO: Call method for handling pugovki
    }
}
