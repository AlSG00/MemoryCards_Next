using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleSuspend : MonoBehaviour
{
    public static System.Action OnSuspendGame;

    public void Use()
    {
        OnSuspendGame?.Invoke();
    }
}
