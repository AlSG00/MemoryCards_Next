using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampHandler : MonoBehaviour
{
    [SerializeField] private Animator _lampAnimator;

    [Header("Main Parameters")]
    [SerializeField] private Light _mainLamp;
    [SerializeField] private float _defaultIntensity;
    [SerializeField] private float _pauseIntensity;
    
    [Header("Audio")]
    [SerializeField] private AudioSource _lampAudioSource;
    [SerializeField] private AudioClip _TurnOnSound;
    [SerializeField] private AudioClip _TurnOffSound;
    [SerializeField] private AudioClip _ExplosionSound;

    private void Awake()
    {
        _defaultIntensity = _mainLamp.intensity;
        _pauseIntensity = _defaultIntensity / 2;
    }

    public void SetLightForPause(bool isPaused)
    {
        _lampAnimator.SetBool("isPaused", isPaused);
    }

    public void TurnOffBeforeMainMenu()
    {
        StopAllCoroutines();
        StartCoroutine(ExitToMainMenuRoutine());
    }

    public void TurnOffBeforeExit()
    {
        _lampAnimator.SetTrigger("Exit");
        StartCoroutine(ExitGameRoutine());
    }

    public IEnumerator ExitGameRoutine()
    {
        yield return new WaitForSecondsRealtime(3);
        Application.Quit();
    }

    public void PlayExplosionSound()
    {
        _lampAudioSource.PlayOneShot(_ExplosionSound);
    }

    public void TurnOff()
    {
        _lampAnimator.SetBool("isTurnedOff", true);
    }

    public void SwitchState()
    {
        if (!_lampAnimator.GetBool("isTurnedOff"))
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }

    public void TurnOn()
    {
        _lampAnimator.SetBool("isTurnedOff", false);
        _lampAnimator.SetTrigger("TurnOn");
    }

    private IEnumerator ExitToMainMenuRoutine()
    {
        _lampAnimator.SetTrigger("TurnOff");

        yield return new WaitForSecondsRealtime(2);
        _lampAnimator.SetBool("isPaused", false);
        TurnOn();
    }

    public void PlayTurnOnSound()
    {
        _lampAudioSource.PlayOneShot(_TurnOnSound);
    }

    public void PlayTurnOffSound()
    {
        _lampAudioSource.PlayOneShot(_TurnOffSound);
    }
}
