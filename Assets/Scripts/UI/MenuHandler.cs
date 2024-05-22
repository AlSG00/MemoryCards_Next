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

    private protected void SetMenuVisibility(bool setVisible)
    {
        if (setVisible)
        {
            ShowMenu();
        }
        else
        {
            HideMenu();
        }
    }

    protected void HideMenu()
    {
        _canvasRaycaster.enabled = false;
        SetMenuElementsVisibility(false);
    }

    protected void ShowMenu()
    {
        _canvasRaycaster.enabled = true;
        SetMenuElementsVisibility(true);
    }

    // TODO: Make menu ui change it's visibility smoothly
    private void SetMenuElementsVisibility(bool isVisible)
    {
        foreach (var textElement in _textElementArray)
        {
            textElement.SetActive(isVisible);
        }
    }
}
