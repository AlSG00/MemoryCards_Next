using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDifficultyButton : MenuButton
{
    [SerializeField] private NEW_GameProgression.Difficulty _difficulty;

    public static event System.Action DifficultyPicked;

    protected override void OnClickAction()
    {
        NEW_GameProgression.StartLayoutDifficulty = _difficulty;
        DifficultyPicked?.Invoke();
    }
}
