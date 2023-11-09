using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuUi;
    [SerializeField] private Animator _pauseMenuAnimator;

    [Header("References")]
    [SerializeField] private LampHandler _lampHandler;
    [SerializeField] private CardGenerator _cardGenerator;
    [SerializeField] private CardLayoutHandler _cardLayoutHandler;
    [SerializeField] private TimeLineHandler _timeLine;
    [SerializeField] private SessionProgressHandler _sessionProgressHandler;

    private void Start()
    {
        _pauseMenuUi.SetActive(false);
    }

    public void EnterPauseMenu()
    {
        _sessionProgressHandler.isGamePaused = true;
        _cardLayoutHandler.ActivateCardColliders(false);
        _timeLine.Hide();
        _pauseMenuAnimator.SetBool("isPaused", true);
        _lampHandler.SetLightForPause(true);
    }

    public void ExitPauseMenu()
    {
        _sessionProgressHandler.isGamePaused = false;
        _cardLayoutHandler.ActivateCardColliders(true);
        if (_sessionProgressHandler.counterStarted)
        {
            _timeLine.Show();
        }
        _pauseMenuAnimator.SetBool("isPaused", false);
        _lampHandler.SetLightForPause(false);
    }

    public void ExitMainMenu()
    {
        StopAllCoroutines();
        StartCoroutine(ExitMainMenuRoutine());
    }

    private IEnumerator ExitMainMenuRoutine()
    {
        _lampHandler.TurnOffBeforeMainMenu();
        _pauseMenuAnimator.SetTrigger("gameExitToMenu");
        _sessionProgressHandler.ResetSound();
        yield return new WaitForSecondsRealtime(1);
        
        _cardGenerator.RemoveAllCards();
        yield return new WaitForSecondsRealtime(1);

        _pauseMenuAnimator.SetBool("isPaused", false);
        _sessionProgressHandler.ResetProgress();
    }
}
