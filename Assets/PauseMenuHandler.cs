using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _testButton;

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
        _testButton.SetActive(enabled);
    }
}
