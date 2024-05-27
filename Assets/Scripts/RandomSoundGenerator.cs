using System.Collections.Generic;
using UnityEngine;

public class RandomSoundGenerator : MonoBehaviour
{
    public AudioSource RandomAudioSource;
    [SerializeField] private float _minTimeBeforeNextSound;
    [SerializeField] private float _maxTimeBeforeNextSound;
    [SerializeField] private List<AudioClip> _clipsForStart;
    [SerializeField] private List<AudioClip> _clipsForMiddle;
    [SerializeField] private List<AudioClip> _clipsForEnd;
    [SerializeField] private List<AudioClip> _currentClipList = new List<AudioClip>();
    private float _timeRemaining = 0;

    private void FixedUpdate()
    {
        _timeRemaining -= Time.deltaTime;

        if (_timeRemaining <= 0)
        {
            if (_currentClipList.Count != 0)
            {
                RandomAudioSource.PlayOneShot(_currentClipList[Random.Range(0, _currentClipList.Count)]);
            }

            _timeRemaining = GetTimeRemaining();
        }
    }

    private float GetTimeRemaining()
    {
        return _timeRemaining = Random.Range(_minTimeBeforeNextSound, _maxTimeBeforeNextSound);
    }

    public void SetCurrentClipList(int currentRound)
    {
        switch (currentRound)
        {
            case 0:
                _currentClipList = new List<AudioClip>();
                break;
            case 1:
                _currentClipList = new List<AudioClip>();
                break;
            case 2:
                _currentClipList = _clipsForStart;
                break;
            case 3:
                _currentClipList = _clipsForMiddle;
                break;
            default:
                _currentClipList = _clipsForEnd;
                break;
        }
    }
}
