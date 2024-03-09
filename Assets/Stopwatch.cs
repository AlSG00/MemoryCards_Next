using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopwatch : TableItem
{
    [SerializeField] private Transform _secondArrow;
    [SerializeField] private Transform _minuteArrow;

    [SerializeField] private MeshRenderer _minuteWarning;
    [SerializeField] private MeshRenderer _secondWarning;
    [SerializeField] private float _minuteOneFlickedDuration;
    [SerializeField] private int _minuteFlickerCount;
    [SerializeField] private float _secondOneFlickedDuration;
    [SerializeField] private int _secondFlickerCount;


    private int _secondArrowStep = 6;
    private int _minuteArrowStep = 45;

    // TEMP
    private float _elapsedTime = 0;
    //private int _tempTime;

    [SerializeField] private bool _isActive;
    int _seconds;
    int _minutes;
    int _remainingTime;

    public static event System.Action OutOfTime;

    private void OnEnable()
    {
        NEW_GameProgression.ActivateStopwatch += Initialize;
    }

    private void OnDisable()
    {
        NEW_GameProgression.ActivateStopwatch -= Initialize;
    }

    private void Start()
    {
        isVisible = false;
        _isActive = false;
        //_remainingTime = ;
        //Initialize(_remainingTime);

        _minuteWarning.enabled = false;
        _secondWarning.enabled = false;
    }

    private void FixedUpdate()
    {
        UpdateRemainingTime();
    }

    private void UpdateRemainingTime()
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
                //StopAllCoroutines();
                //StartCoroutine(LoseGameRoutine());
                _isActive = false;
                _secondWarning.enabled = true;
                OutOfTime?.Invoke();
            }
        }
    }

    private void Initialize(int timeInSeconds)
    {
        _remainingTime = timeInSeconds;
        _elapsedTime = 0;
        _minuteWarning.enabled = false;
        _secondWarning.enabled = false;

        _minutes = timeInSeconds / 60;
        _seconds = timeInSeconds - _minutes * 60;

        SetArrowStartRotation(_minuteArrow, _minutes, _minuteArrowStep);
        SetArrowStartRotation(_secondArrow, _seconds, _secondArrowStep);

        _isActive = true;
    }

    private int DecreaseTime(int timeInSeconds)
    {
        RotateArrow(_secondArrow, _secondArrowStep);
        timeInSeconds--;
        if (timeInSeconds % 60 == 0 && timeInSeconds > 0)
        {
            RotateArrow(_minuteArrow, _minuteArrowStep);
        }

        if (timeInSeconds == 60)
        {
            StartCoroutine(WarnPlayerRoutine(_minuteWarning, _minuteOneFlickedDuration, _minuteFlickerCount));
        }

        if (timeInSeconds == 15)
        {
            StartCoroutine(WarnPlayerRoutine(_secondWarning, _secondOneFlickedDuration, _secondFlickerCount));
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

    float oneFlickerDuration;
 
    private IEnumerator WarnPlayerRoutine(MeshRenderer warningObject, float oneFlickerDuration, int flickerCount)
    {
        for (int i = 0; i < flickerCount; i++)
        {
            warningObject.enabled = true;
            yield return new WaitForSeconds(oneFlickerDuration);
            warningObject.enabled = false;
            yield return new WaitForSeconds(oneFlickerDuration);
        }
    }

    //private IEnumerator LoseGameRoutine()
    //{
    //    _isActive = false;
    //    _secondWarning.enabled = true;
    //    OutOfTime?.Invoke();
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
