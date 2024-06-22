using UnityEngine;

public class SetDifficultyButton : MenuButton
{
    [SerializeField] private GameProgression.Difficulty _difficulty;

    public static event System.Action DifficultyPicked;

    protected override void OnClickAction()
    {
        GameProgression.StartCardDifficulty = _difficulty;
        DifficultyPicked?.Invoke();
    }
}
