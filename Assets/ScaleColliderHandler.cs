using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleColliderHandler : MonoBehaviour
{
    public static event System.Action<bool> OnEnterCollider;
    [SerializeField] private Scale _scale;


    private void OnMouseEnter()
    {
        if (_scale._isVisible == false)
        {
            return;
        }

        //Debug.Log($"{gameObject.name} entered mouse");
        OnEnterCollider?.Invoke(true);
    }

    private void OnMouseExit()
    {
        if (_scale._isVisible == false)
        {
            return;
        }

        //Debug.Log($"{gameObject.name} leaved mouse");
        OnEnterCollider?.Invoke(false);
    }
}

