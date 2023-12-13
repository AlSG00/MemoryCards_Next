using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleColliderHandler : MonoBehaviour
{
    public static event System.Action<bool> OnEnterCollider;

    private void OnMouseEnter()
    {
        Debug.Log($"{gameObject.name} entered mouse");
        OnEnterCollider?.Invoke(true);
    }

    private void OnMouseExit()
    {
        Debug.Log($"{gameObject.name} leaved mouse");
        OnEnterCollider?.Invoke(false);
    }
}

