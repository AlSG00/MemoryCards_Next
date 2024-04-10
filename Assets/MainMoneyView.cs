using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMoneyView : MonoBehaviour
{
    [Tooltip("Time in milliseconds")]
    [SerializeField] private int singleCoinAppearanceDelay;
    [SerializeField] private List<MeshRenderer> _coinMeshList;
    private int _currentValue;

    public static System.Action<int> OnMainMoneyViewUpdate;

    private bool TestDebugIsReady = true;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {

    }

    private async void Increase(int newValue)
    {
        for (int i = _currentValue - 1; i < Mathf.Clamp(newValue, 0, _coinMeshList.Count); i++)
        {
            await System.Threading.Tasks.Task.Delay(singleCoinAppearanceDelay);
            _coinMeshList[i].enabled = true;
        }

        _currentValue += newValue;
        // TODO: Change visual amount of coins
    }

    private async void Decrease(int newValue)
    {
        for (int i = newValue - 1; i > Mathf.Clamp(newValue, 0, _coinMeshList.Count); i++)
        {
            await System.Threading.Tasks.Task.Delay(singleCoinAppearanceDelay);
            _coinMeshList[i].enabled = true;
        }

        _currentValue -= newValue;
        // TODO: Change visual amount of coins
    }
}
