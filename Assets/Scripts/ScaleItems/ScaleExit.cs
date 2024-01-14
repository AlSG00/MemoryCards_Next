using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScaleItem))]
public class ScaleExit : MonoBehaviour, IScaleItem
{
    public static System.Action OnFinishGame;
    public void Use()
    {
        OnFinishGame?.Invoke();
    }
}
