using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnCounterGUIController : MonoBehaviour
{
    [SerializeField] private int _counterTextLength = 3;
    [SerializeField] private TextMeshProUGUI[] _turnCounter;

    private void OnEnable()
    {
        NEW_GameProgression.OnTurnsChanged += UpdateRemainingTurns;
    }

    private void OnDisable()
    {
        NEW_GameProgression.OnTurnsChanged -= UpdateRemainingTurns;
    }

    private void UpdateRemainingTurns(int turnsLeft)
    {
        string turnsLeftString = ConvertToRequiredFormat(turnsLeft);

        foreach (var turnCounter in _turnCounter)
        {
            turnCounter.text = turnsLeftString;
        }
    }

    private string ConvertToRequiredFormat(int turnsLeft)
    {
        string turnString = turnsLeft.ToString();
        string result = "";

        int counterLength = turnString.Length;
        while (counterLength < _counterTextLength)
        {
            result += "0";
            counterLength++;
        }

        foreach (var digit in turnString)
        {
            if (digit.Equals('1'))
            {
                result += " ";
            }

            result += digit;
        }

        return result;
    }
}