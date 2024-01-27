using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleColliderHandler : MonoBehaviour
{
    [SerializeField] private Scale _scale;
    public static event System.Action<bool> OnEnterCollider;

    private void OnMouseEnter()
    {
        if (_scale.isVisible == false)
        {
            return;
        }

        OnEnterCollider?.Invoke(true);
    }

    private void OnMouseExit()
    {
        if (_scale.isVisible == false)
        {
            return;
        }

        OnEnterCollider?.Invoke(false);
    }
}

