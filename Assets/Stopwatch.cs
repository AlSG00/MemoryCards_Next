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

    [SerializeField] private GameObject _lowTimeZone;

    private int _secondArrowStep = 6;
    private int _minuteArrowStep = 45;

    // TEMP
    private float _elapsedTime = 0;

    [SerializeField] private bool _isActive;
    private bool _isDeactivatedByDebuff;
    int _seconds;
    int _minutes;
    int _remainingTime;

    public static event System.Action OutOfTime;
    public static event System.Action<bool> StopwatchActivated;

    private void OnEnable()
    {
        NEW_GameProgression.ActivateStopwatch += ChangeVisibility;
        NEW_GameProgression.PauseGame += Pause;
        HammerUseLogic.OnUseHammer += DeactivateByHammer;
    }

    private void OnDisable()
    {
        NEW_GameProgression.ActivateStopwatch -= ChangeVisibility;
        NEW_GameProgression.PauseGame += Pause;
        HammerUseLogic.OnUseHammer -= DeactivateByHammer;
    }

    private void Start()
    {
        isVisible = false;
        _isActive = false;
        _isDeactivatedByDebuff = false;
        _minuteWarning.enabled = false;
        _secondWarning.enabled = false;
        _lowTimeZone.SetActive(false);
    }

    private void FixedUpdate()
    {
        UpdateRemainingTime();
    }

    private void UpdateRemainingTime()
    {
        if (_isActive == false ||
            _isDeactivatedByDebuff)
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
                OutOfTime?.Invoke();
                _isActive = false;
                _secondWarning.enabled = true;
            }
        }
    }

    private void Pause(bool setPause)
    {
        if (isVisible == false ||
            _isDeactivatedByDebuff)
        {
            return;
        }
        
        _isActive = !setPause;
    }

    private void SetActive(bool setActive)
    {
        _isActive = setActive;
    }

    private void Initialize(int timeInSeconds)
    {
        _remainingTime = timeInSeconds;
        _elapsedTime = 0;
        _minuteWarning.enabled = false;
        _secondWarning.enabled = false;
        _lowTimeZone.SetActive(false);

        _minutes = timeInSeconds / 60;
        _seconds = timeInSeconds - _minutes * 60;

        if (_remainingTime <= 60)
        {
            _lowTimeZone.SetActive(true);
        }

        SetArrowStartRotation(_minuteArrow, _minutes, _minuteArrowStep);
        SetArrowStartRotation(_secondArrow, _seconds, _secondArrowStep);

        _isActive = true;
        Debug.Log($"{_remainingTime}");
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
            _lowTimeZone.SetActive(true);
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
            0,
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

    private void DeactivateByHammer()
    {
        if (isVisible == false)
        {
            return;
        }

        isVisible = false;
        _isActive = false;
        _animator.SetTrigger("Deactivate");
        StopwatchActivated?.Invoke(false); 
    }

    private void ChangeVisibility(bool setActive, int timeInSeconds = 0)
    {
        base.ChangeVisibility(setActive);

        if (timeInSeconds != 0)
        {
            Initialize(timeInSeconds);
        }
        else
        {
            _isActive = false;
        }
    }
}