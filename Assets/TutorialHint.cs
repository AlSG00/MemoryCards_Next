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
        NEW_GameProgression.OnShowHint += EnableHint;
    }

    private void OnDisable()
    {
        NEW_GameProgression.OnShowHint += EnableHint;
    }

    private void Start()
    {
        isVisible = false;
        _hintTextMesh.enabled = false;
    }

    private void EnableHint(int hintIndex)
    {
        if (_hintIndex == hintIndex)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        if (isVisible)
        {
            return;
        }
        isVisible = true;
        _hintTextMesh.enabled = true;
        //Debug.Log($"Hint: {_hintIndex} shown");
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
