using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] _textElementArray;
    [SerializeField] private GraphicRaycaster _canvasRaycaster;

    protected virtual void Awake()
    {
        HideMenu();
    }

    protected void HideMenu()
    {
        _canvasRaycaster.enabled = false;
        SetMenuVisibility(false);
    }

    protected void ShowMenu()
    {
        _canvasRaycaster.enabled = true;
        SetMenuVisibility(true);
    }

    // TODO: Make menu ui change it's visibility smoothly
    private void SetMenuVisibility(bool isVisible)
    {
        foreach (var textElement in _textElementArray)
        {
            textElement.SetActive(isVisible);
        }
    }
}
