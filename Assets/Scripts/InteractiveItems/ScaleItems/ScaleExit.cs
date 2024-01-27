using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScaleItem))]
public class ScaleExit : MonoBehaviour, IUsable
{
    public static System.Action OnFinishGame;
    public void Use()
    {
        OnFinishGame?.Invoke();
    }
}
