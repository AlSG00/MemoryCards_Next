using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEventsAnimationHandler : MonoBehaviour
{
    [SerializeField] Animator _animator;

    private void OnEnable()
    {
        RemainingTurnsHandler.OnOutOfTurns += PlayLoseGameEvent;
    }

    private void OnDisable()
    {
        RemainingTurnsHandler.OnOutOfTurns -= PlayLoseGameEvent;
    }

    private void PlayLoseGameEvent()
    {
        _animator.Play("SceneEvent_Lose");
    }
}
