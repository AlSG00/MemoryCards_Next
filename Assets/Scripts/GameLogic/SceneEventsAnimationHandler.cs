using UnityEngine;

public class SceneEventsAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string _leaveEndGameScreenTrigger = "";
    private const string _LeaveGameTrigger = "";
    private const string _BreakLightTrigger = "";
    private const string _FixLightTrigger = "";


    private void OnEnable()
    {
        RemainingTurnsHandler.OutOfTurns += LoseGame;
        Stopwatch.OutOfTime += LoseGame;
        GameProgression.PauseGame += GameProgression_PauseGame;
        RejectStartButton.OnGameStartReject += LeaveEndGameScreen;
        StartButton.StartPressed += LeaveEndGameScreen;
        BackToMenuButton.ReturningToMainMenu += LeaveGame;
    }

    private void OnDisable()
    {
        RemainingTurnsHandler.OutOfTurns -= LoseGame;
        Stopwatch.OutOfTime -= LoseGame;
        GameProgression.PauseGame -= GameProgression_PauseGame;
        RejectStartButton.OnGameStartReject -= LeaveEndGameScreen;
        StartButton.StartPressed -= LeaveEndGameScreen;
        BackToMenuButton.ReturningToMainMenu -= LeaveGame;
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

    private async void LeaveGame()
    {
        _animator.SetTrigger("LeaveGame");
        await System.Threading.Tasks.Task.Delay(1000);

        _animator.SetBool("IsGamePaused", false);
    }

    private void PlayAnimationAudio()
    {
        // WIP
    }

    private void BreakLightByDebuff()
    {
        //_animator.SetTrigger("")
    }

    private void FixBrokenLight()
    {

    }
}
