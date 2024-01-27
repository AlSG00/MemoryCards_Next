using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] TextObjectArray;

    private void Awake()
    {
        ShowMenu();
    }

    private void OnEnable()
    {
        NEW_GameProgression.OnPressStart += HideMenu;
        RejectStartButton.OnGameStartReject += ShowMenu;
        SetLocaleButton.OnChooseLocale += ShowMenu;
        NEW_GameProgression.FirstTimePlaying += HideMenu;
    }

    private void OnDisable()
    {
        NEW_GameProgression.OnPressStart -= HideMenu;
        RejectStartButton.OnGameStartReject -= ShowMenu;
        SetLocaleButton.OnChooseLocale -= ShowMenu;
        NEW_GameProgression.FirstTimePlaying -= HideMenu;
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
