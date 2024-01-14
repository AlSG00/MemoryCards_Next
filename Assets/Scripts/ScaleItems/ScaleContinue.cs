using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScaleItem))]
public class ScaleContinue : MonoBehaviour, IScaleItem
{
    public static System.Action OnContinueGame;

    public void Use()
    {
        OnContinueGame?.Invoke();
    }
}
