using UnityEngine;
using TMPro;

public class TurnCounterMainScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _roundCounter;
    [SerializeField] private TextMeshProUGUI[] _GUIElements;
    [SerializeField] private int _counterTextLength = 3;

    private void OnEnable()
    {
        TurnCounterModeSwitcher.OnSwitchMode += ChangeScreenVisiblity;
        NEW_GameProgression.OnNextRound += SetCurrentRoundGUI;
    }

    private void OnDisable()
    {
        TurnCounterModeSwitcher.OnSwitchMode -= ChangeScreenVisiblity;
        NEW_GameProgression.OnNextRound -= SetCurrentRoundGUI;
    }

    private void ChangeScreenVisiblity(bool isVisible)
    {
        isVisible = !isVisible;
        foreach (var element in _GUIElements)
        {
            element.enabled = isVisible;
        }
    }

    private void SetCurrentRoundGUI(int round)
    {
        _roundCounter.text = ConvertToRequiredFormat(round);
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
