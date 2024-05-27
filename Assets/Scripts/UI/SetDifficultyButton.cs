using UnityEngine;

public class SetDifficultyButton : MenuButton
{
    [SerializeField] private NEW_GameProgression.Difficulty _difficulty;

    public static event System.Action DifficultyPicked;

    protected override void OnClickAction()
    {
        NEW_GameProgression.StartCardDifficulty = _difficulty;
        DifficultyPicked?.Invoke();
    }
}
