using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] TextObjectArray;

    private void OnEnable()
    {
        NEW_GameProgression.FirstTimePlaying += ShowMenu;
        SetLocaleButton.OnChooseLocale += HideMenu;
    }

    private void OnDisable()
    {
        NEW_GameProgression.FirstTimePlaying -= ShowMenu;
        SetLocaleButton.OnChooseLocale += HideMenu;
    }

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
