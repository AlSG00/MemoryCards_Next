using System.Collections.Generic;
using UnityEngine;

public class MainMoneyView : MonoBehaviour
{
    [Tooltip("Time in milliseconds")]
    [SerializeField] private int singleCoinAppearanceDelay;
    [SerializeField] private List<MeshRenderer> _coinMeshList;
    private int _currentValue;

    public static System.Action UpdatingMainMoneyView;
    public static System.Action<int> UpdatingMainMoneyScaleCounter; // TODO: Rename event

    private void OnEnable()
    {
        EndGameScreen.GiveReward += VisualizeRewardAccrual;
        RejectStartButton.OnGameStartReject += HideAllDelayed;
        StartButton.StartPressed += HideAllDelayed;
    }

    private void OnDisable()
    {
        EndGameScreen.GiveReward -= VisualizeRewardAccrual;
        RejectStartButton.OnGameStartReject -= HideAllDelayed;
        StartButton.StartPressed -= HideAllDelayed;
    }

    private void Start()
    {
        _currentValue = 0; // TODO: Save value somewhere
        HideAll();
    }

    private async void HideAllDelayed()
    {
        await System.Threading.Tasks.Task.Delay(1500);
        HideAll();
    }

    private void HideAll()
    {
        foreach (var mesh in _coinMeshList)
        {
            mesh.enabled = false;
        }
    }

    private void Show()
    {
        for (int i = 0; i < _currentValue; i++)
        {
            _coinMeshList[i].enabled = true;
        }
    }

    private async void Increase(int valueToIncrease)
    {
        int newValue = _currentValue + valueToIncrease;
        for (int i = Mathf.Clamp(_currentValue - 1, 0, _coinMeshList.Count); i < Mathf.Clamp(newValue, 0, _coinMeshList.Count); i++)
        {
            await System.Threading.Tasks.Task.Delay(singleCoinAppearanceDelay);
            UpdatingMainMoneyScaleCounter?.Invoke(i + 1);
            _coinMeshList[i].enabled = true;
        }

        _currentValue = newValue;
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

    private async void VisualizeRewardAccrual(int reward)
    {
        UpdatingMainMoneyView?.Invoke();
        Show();
        await System.Threading.Tasks.Task.Delay(2500); // TODO: Calibrate

        Increase(reward);
    }
}
