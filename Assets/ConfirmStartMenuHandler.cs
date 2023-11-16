using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConfirmStartMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] TextObjectArray;

    private void Awake()
    {
        HideMenu();
    }

    private void OnEnable()
    {
        NEW_GameProgression.OnPressStart += ShowMenu;
        RejectStartButton.OnGameStartReject += HideMenu;
        NEW_GameProgression.OnGameStartConfirm += HideMenu;
    }

    private void OnDisable()
    {
        NEW_GameProgression.OnPressStart -= ShowMenu;
        RejectStartButton.OnGameStartReject -= HideMenu;
        NEW_GameProgression.OnGameStartConfirm -= HideMenu;
    }

    // TODO: Make menu ui change it's visibility smoothly
    private void HideMenu()
    {
        SetMenuVisibility(false);
    }

    private void ShowMenu()
    {
        SetMenuVisibility(true);
    }

    private void SetMenuVisibility(bool isVisible)
    {
        foreach (var textMesh in TextObjectArray)
        {
            textMesh.SetActive(isVisible);
        }
    }
}
