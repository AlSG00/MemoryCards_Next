using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAudioSourcePlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _minAudioPitch;
    [SerializeField] private float _maxAudioPitch;
    [SerializeField] private AudioClip[] coinAudioClipArray;
    //private CoinAudioSet _currentCoinAudioSet;

    private void OnEnable()
    {
        MainMoneyView.UpdatingMainMoneyCounter += Play;
    }

    private void OnDisable()
    {
        MainMoneyView.UpdatingMainMoneyCounter -= Play;
    }

    //private void Awake()
    //{
    //    _currentCoinAudioSet = CoinAudioSetCollection[0];
    //}

    int count = 0;
    private void Play(int foo = 0)
    {
        //AudioClip clipToPlay = _currentCoinAudioSet.coinAudioClipArray[Random.Range(0, _currentCoinAudioSet.coinAudioClipArray.Length)];
        _audioSource.pitch = Random.Range(_minAudioPitch, _maxAudioPitch);
        AudioClip clipToPlay = coinAudioClipArray[Random.Range(0, coinAudioClipArray.Length)];
        _audioSource.PlayOneShot(clipToPlay);
    }

    //[System.Serializable]
    //private class CoinAudioSet
    //{
    //    [SerializeField] private string _name;
    //    [SerializeField] protected internal AudioClip[] coinAudioClipArray;
    //}
}
