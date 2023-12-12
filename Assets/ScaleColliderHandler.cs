using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleColliderHandler : MonoBehaviour
{
    private void OnMouseEnter()
    {
        Debug.Log($"{gameObject.name} entered mouse");
    }

    private void OnMouseExit()
    {
        Debug.Log($"{gameObject.name} leaved mouse");
    }
}
