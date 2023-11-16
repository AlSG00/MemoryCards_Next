using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenuHandler : MonoBehaviour
{
    public Animator pauseMenuAnimator;

    [Header("References")]
    [SerializeField] private SessionProgressHandler _sessionProgress;
    [SerializeField] private MainMenuHandler _mainMenuHandler;
    [SerializeField] private LampHandler _lampHandler;
    [SerializeField] private CardGenerator _cardGenerator;
    [SerializeField] private TimeLineHandler _timeLine;

    public void RestartGame()
    {
        _sessionProgress.ResetProgress();
        //_mainMenuHandler.StartGame();
    }

    public void HandleLight()
    {
        _lampHandler.SwitchState();
        _sessionProgress.ResetSound();
    }

    public void ExitToMainMenu()
    {
        StopAllCoroutines();
        StartCoroutine(ExitMainMenuRoutine());
    }

    private IEnumerator ExitMainMenuRoutine()
    {
        pauseMenuAnimator.SetTrigger("gameExitToMenu");
        _timeLine.Hide();
        yield return new WaitForSecondsRealtime(1);

        _cardGenerator.RemoveAllCards();
        _lampHandler.TurnOn();
        yield return new WaitForSecondsRealtime(1);

        pauseMenuAnimator.SetBool("isPaused", false);
        _sessionProgress.ResetProgress();
    }

    public void ExitToDesktop()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
