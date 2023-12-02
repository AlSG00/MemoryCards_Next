using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialHint : MonoBehaviour
{
    [SerializeField] private int _hintIndex;
    [SerializeField] private TextMeshProUGUI _hintTextMesh;
    private bool isVisible;

    private void OnEnable()
    {
        //NEW_GameProgression.OnStartTutorialPhase += Show;
        NEW_GameProgression.OnPlayTutorial += EnableHint;


    }

    private void OnDisable()
    {
        NEW_GameProgression.OnPlayTutorial += EnableHint;
        //NEW_GameProgression.OnStartTutorialPhase -= Show;
    }

    private void Start()
    {
        isVisible = false;
        _hintTextMesh.enabled = false;
    }

    private void EnableHint(int tutorialProgress)
    {
        if (_hintIndex == tutorialProgress)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show(/*int requiredIndex*/)
    {
        if (isVisible)
        {
            return;
        }
        isVisible = true;
        _hintTextMesh.enabled = true;
        Debug.Log($"Hint: {_hintIndex} shown");
    }

    private void Hide()
    {
        if (isVisible == false)
        {
            return;
        }
        isVisible = false;
        _hintTextMesh.enabled = false;
    }
}
