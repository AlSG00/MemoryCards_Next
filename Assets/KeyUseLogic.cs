using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUseLogic : MonoBehaviour, IUsable
{
    public delegate void UseAction();

    public static UseAction OnUseKey;

    public void Use()
    {
        OnUseKey?.Invoke();
    }
}
