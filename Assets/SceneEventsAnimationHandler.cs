using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEventsAnimationHandler : MonoBehaviour
{
    [SerializeField] Animator _animator;

    private void OnEnable()
    {
        RemainingTurnsHandler.OutOfTurns += LoseGame;
        NEW_GameProgression.PauseGame += GameProgression_PauseGame;
        RejectStartButton.OnGameStartReject += LeaveEndGameScreen;
        StartButton.OnGameStart += LeaveEndGameScreen;
    }

    private void OnDisable()
    {
        RemainingTurnsHandler.OutOfTurns -= LoseGame;
        NEW_GameProgression.PauseGame -= GameProgression_PauseGame;
        RejectStartButton.OnGameStartReject -= LeaveEndGameScreen;
        StartButton.OnGameStart -= LeaveEndGameScreen;
    }

    private void LoseGame()
    {
        _animator.Play("SceneEvent_Lose");
    }

    private void GameProgression_PauseGame(bool isPaused)
    {
        _animator.SetBool("IsGamePaused", isPaused);
    }

    private void LeaveEndGameScreen()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("SceneEvent_Lose"))
        {
            _animator.SetTrigger("LeaveEndGameScreen");
        }
    }

    private void PlayAnimationAudio()
    {
        // WIPw
    }
}
