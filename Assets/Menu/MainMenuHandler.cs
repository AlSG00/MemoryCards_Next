using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] TextObjectArray;
    [SerializeField] private GraphicRaycaster _canvasRaycaster;

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
        ScaleSuspend.OnSuspendGame += ShowMenu;
    }

    private void OnDisable()
    {
        NEW_GameProgression.OnPressStart -= HideMenu;
        RejectStartButton.OnGameStartReject -= ShowMenu;
        SetLocaleButton.OnChooseLocale -= ShowMenu;
        NEW_GameProgression.FirstTimePlaying -= HideMenu;
        ScaleSuspend.OnSuspendGame -= ShowMenu;
    }

    // TODO: Make menu ui change it's visibility smoothly
    private void HideMenu()
    {
        _canvasRaycaster.enabled = false;
        SetMenuVisibility(false);
    }

    private void ShowMenu()
    {
        _canvasRaycaster.enabled = true;
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
