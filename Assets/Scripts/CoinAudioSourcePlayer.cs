using UnityEngine;

public class CoinAudioSourcePlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _minAudioPitch;
    [SerializeField] private float _maxAudioPitch;
    [SerializeField] private AudioClip[] coinAudioClipArray;

    private void OnEnable()
    {
        MainMoneyView.UpdatingMainMoneyScaleCounter += Play;
    }

    private void OnDisable()
    {
        MainMoneyView.UpdatingMainMoneyScaleCounter -= Play;
    }

    private void Play(int foo = 0)
    {
        _audioSource.pitch = Random.Range(_minAudioPitch, _maxAudioPitch);
        AudioClip clipToPlay = coinAudioClipArray[Random.Range(0, coinAudioClipArray.Length)];
        _audioSource.PlayOneShot(clipToPlay);
    }
}
