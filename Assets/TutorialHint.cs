using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHint : MonoBehaviour
{
    [SerializeField] private int _hintIndex;

    private void OnEnable()
    {
        NEW_GameProgression.OnStartTutorialPhase += Show;
    }

    private void OnDisable()
    {
        NEW_GameProgression.OnStartTutorialPhase -= Show;
    }

    private void Show(int requiredIndex)
    {
        if (requiredIndex != _hintIndex)
        {
            return;
        }

        Debug.Log($"Hint: {_hintIndex}");
    }

    private void Hide()
    {

    }
}
