using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveItemAudioPlayer : MonoBehaviour
{
    [SerializeField] private protected AudioSource _audioSource;
    [SerializeField] private protected float minAudioPitch;
    [SerializeField] private protected float maxAudioPitch;
    [SerializeField] private AudioClip[] _onMouseEnterSoundArray;
    [SerializeField] private AudioClip[] _onMouseExitSoundArray;
    [SerializeField] private AudioClip[] _onMouseDownSoundArray;
    [SerializeField] private AudioClip[] _onMouseUpSoundArray;

    protected internal void MouseEnter()
    {
        Play(_onMouseEnterSoundArray);
    }

    protected internal void MouseExit()
    {
        Play(_onMouseExitSoundArray);
    }

    protected internal void MouseDown()
    {
        Play(_onMouseDownSoundArray);
    }

    protected internal void MouseUp()
    {
        Play(_onMouseUpSoundArray);
    }

    private void Play(AudioClip[] audioClipSet)
    {
        if (audioClipSet is null)
        {
            return;
        }
        _audioSource.pitch = Random.Range(minAudioPitch, maxAudioPitch);
        AudioClip soundToPlay = audioClipSet[Random.Range(0, audioClipSet.Length)];
        _audioSource.PlayOneShot(soundToPlay);
    }
}
