using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopwatch : TableItem
{
    [SerializeField] private Transform _secondArrow;
    [SerializeField] private Transform _minuteArrow;

    private int _secondArrowStep = 6;
    private int _minuteArrowStep = 45;

    // TEMP
    private float _elapsedTime = 0;
    private int _tempTime;

    private bool _isActive;

    private void Start()
    {
        isVisible = false;
        _isActive = false;
    }

    private void Update()
    {
        if (_isActive == false)
        {
            return;
        }

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= 1)
        {
            _elapsedTime = 0;
            SetSecondArrow(_tempTime);
            _tempTime--;
        }
    }

    private void Initialize(int timeInSeconds)
    {
        int minutes = timeInSeconds / 60;
        int seconds = timeInSeconds - minutes * 60;

        SetMinuteArrow(minutes);
        SetSecondArrow(seconds);

        //_isActive = true;
    }

    private void SetTime(int timeInSeconds)
    {
        int minutes = timeInSeconds / 60;
        int seconds = timeInSeconds - minutes * 60;

        //SetMinuteArrow();
        //SetSecondArrow();

        ��� ����������� ��� �������
    }

    private void SetArrowRotation()
    {
        ��� ���� �������� ������ ��� �������� � ��������� �������
    }

    private void SetMinuteArrow(int value)
    {
        ��� �����
        _minuteArrow.localEulerAngles = new Vector3(
            _minuteArrow.localEulerAngles.x,
            _minuteArrow.localEulerAngles.y,
            _minuteArrow.localEulerAngles.z - _minuteArrowStep
            );
    }

    private void SetSecondArrow(int value)
    {
        � ��� �����
        _secondArrow.localEulerAngles = new Vector3(
            _secondArrow.localEulerAngles.x,
            _secondArrow.localEulerAngles.y,
            _secondArrow.localEulerAngles.z - _secondArrowStep
            );

    }

    private void DeactivateByHammer()
    {
        if (isVisible == false)
        {
            return;
        }

        //TODO
    }
}
