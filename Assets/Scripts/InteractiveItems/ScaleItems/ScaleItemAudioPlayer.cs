using UnityEngine;

public class ScaleItemAudioPlayer : InteractiveItemAudioPlayer
{
    [SerializeField] private AudioClip _placeOnScaleSound;

    public void OnPlaceOnScale()
    {
        _audioSource.PlayOneShot(_placeOnScaleSound);
    }
}
