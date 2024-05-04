using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAudioSourcePlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _minAudioPitch;
    [SerializeField] private float _maxAudioPitch;
    [SerializeField] private AudioClip[] coinAudioClipArray;

    private void OnEnable()
    {
        MainMoneyView.UpdatingMainMoneyCounter += Play;
    }

    private void OnDisable()
    {
        MainMoneyView.UpdatingMainMoneyCounter -= Play;
    }

    //int count = 0;
    private void Play(int foo = 0)
    {
        _audioSource.pitch = Random.Range(_minAudioPitch, _maxAudioPitch);
        AudioClip clipToPlay = coinAudioClipArray[Random.Range(0, coinAudioClipArray.Length)];
        _audioSource.PlayOneShot(clipToPlay);
    }
}
