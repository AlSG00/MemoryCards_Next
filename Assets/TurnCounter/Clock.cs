using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Text;

public class Clock : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _GUIElements;
    [SerializeField] private TextMeshProUGUI[] _dayTextMeshCollection;
    [SerializeField] private TextMeshProUGUI _date;
    [SerializeField] private TextMeshProUGUI _weekDay;
    [SerializeField] private TextMeshProUGUI _hours;
    [SerializeField] private TextMeshProUGUI _minutes;
    [SerializeField] private TextMeshProUGUI _seconds;

    private bool isActive = true;

    private void OnEnable()
    {
        TurnCounterModeSwitcher.OnSwitchMode += ChangeClockVisiblity;
    }

    private void OnDisable()
    {
        TurnCounterModeSwitcher.OnSwitchMode -= ChangeClockVisiblity;
    }

    private void FixedUpdate()
    {
        SetTimeValue(DateTime.Now.Second, _seconds);
        SetTimeValue(DateTime.Now.Minute, _minutes);
        SetTimeValue(DateTime.Now.Hour, _hours);
        SetDayValue();
        SetDateValue();
    }

    private void SetTimeValue(int time, TextMeshProUGUI textMesh)
    {
        string resultTime = ConvertTimeValue(time);
        ConvertTimeValue(time);
        textMesh.text = resultTime;
    }

    private void SetDayValue()
    {
        if (isActive == false)
        {
            return;
        }

        foreach (var day in _dayTextMeshCollection)
        {
            day.enabled = false;
        }

        int currentDay = (int)DateTime.Today.DayOfWeek;
        _dayTextMeshCollection[currentDay].enabled = true;
    }

    private void SetDateValue()
    {
        string result = "";
        result = $"{ConvertTimeValue(DateTime.Today.Day)}." +
                 $"{ConvertTimeValue(DateTime.Today.Month)}." +
                 $"{ConvertTimeValue(DateTime.Today.Year)}";

        _date.text = result;
    }

    private string ConvertTimeValue(int time)
    {
        string result = "";
        string timeString = time.ToString();
        if (time < 10)
        {
            result += "0";
        }

        foreach (var digit in timeString)
        {
            if (digit.Equals('1'))
            {
                result += " ";
            }

            result += digit;
        }

        return result;
    }

    private void ChangeClockVisiblity(bool isVisible)
    {
        isActive = isVisible;
        foreach (var gui in _GUIElements)
        {
            gui.enabled = isVisible;
        }

        if (isVisible)
        {
            SetDayValue();
        }
    }
}
