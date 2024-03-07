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

    [SerializeField] private bool _isActive;
    int _seconds;
    int _minutes;
    int _remainingTime;

    private void Start()
    {
        isVisible = false;
        _isActive = false;
        _remainingTime = 75;
        Initialize(_remainingTime);
    }

    private void FixedUpdate()
    {
        if (_isActive == false)
        {
            return;
        }

        _elapsedTime += Time.fixedDeltaTime;
        if (_elapsedTime >= 1)
        {
            _elapsedTime = 0;
            _remainingTime = DecreaseTime(_remainingTime);
            if (_remainingTime <= 0)
            {
                _isActive = false;
                // TODO: Invoke lose event
            }
        }
    }

    private void Initialize(int timeInSeconds)
    {
        _minutes = timeInSeconds / 60;
        _seconds = timeInSeconds - _minutes * 60;

        SetArrowStartRotation(_minuteArrow, _minutes, _minuteArrowStep);
        SetArrowStartRotation(_secondArrow, _seconds, _secondArrowStep);
    }

    private int DecreaseTime(int timeInSeconds)
    {
        RotateArrow(_secondArrow, _secondArrowStep);
        timeInSeconds--;
        if (timeInSeconds % 60 == 0 && timeInSeconds > 0)
        {
            RotateArrow(_minuteArrow, _minuteArrowStep);
        }

        return timeInSeconds;
    }

    private void SetArrowStartRotation(Transform arrow, int arrowValue, int rotationStep)
    {
        arrow.localEulerAngles = new Vector3(
            arrow.localEulerAngles.x,
            arrow.localEulerAngles.y,
            arrowValue * rotationStep
            );
    }

    private void RotateArrow(Transform arrow, int rotationStep)
    {
        arrow.localEulerAngles = new Vector3(
            arrow.localEulerAngles.x,
            arrow.localEulerAngles.y,
            arrow.localEulerAngles.z - rotationStep
            );
    }

    private void SetMinuteArrow(int value)
    {
        //тут косяк
        _minuteArrow.localEulerAngles = new Vector3(
            _minuteArrow.localEulerAngles.x,
            _minuteArrow.localEulerAngles.y,
            _minuteArrow.localEulerAngles.z - _minuteArrowStep
            );
    }

    //private void SetSecondArrow(int value)
    //{
    //    _secondArrow.localEulerAngles = new Vector3(
    //        _secondArrow.localEulerAngles.x,
    //        _secondArrow.localEulerAngles.y,
    //        value * _secondArrowStep
    //        );

    //    Debug.Log($"o: {value * _secondArrowStep}. s: {value}");
    //}

    private void DeactivateByHammer()
    {
        if (isVisible == false)
        {
            return;
        }

        //TODO
    }
}
