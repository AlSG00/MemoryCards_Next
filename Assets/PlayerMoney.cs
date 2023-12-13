using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private int _mainMoney;
    [SerializeField] private int _currentGameMoney;

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
