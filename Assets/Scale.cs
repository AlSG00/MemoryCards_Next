using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scale : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private bool _isItemOnScale;

    private void Awake()
    {
        _isItemOnScale = false;
    }
}
