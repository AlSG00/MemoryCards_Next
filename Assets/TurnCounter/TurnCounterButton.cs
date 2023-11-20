using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnCounterButton : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnMouseDown()
    {
        if (_animator)
        {
            _animator.SetTrigger("Click");
        }

        GetComponent<IButtonAction>().OnClickAction();
    }
}
