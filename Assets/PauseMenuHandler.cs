using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _testButton;
    private GraphicRaycaster _raycaster;

    private void OnEnable()
    {
        NEW_GameProgression.PauseGame += Test;
    }

    private void OnDisable()
    {
        NEW_GameProgression.PauseGame -= Test;
    }

    private void Start()
    {
        Test(false);
        
    }

    private void Test(bool enabled)
    {
        _raycaster.enabled = enabled;
        _testButton.SetActive(enabled);
    }
}
