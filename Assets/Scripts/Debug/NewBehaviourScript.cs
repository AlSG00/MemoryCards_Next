using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class NewBehaviourScript : MonoBehaviour
{
    public Transform[] buttons;
    public Transform[] pivots;

    public bool activate = false;

    private void Update()
    {
        if (activate)
        {
            SyncPivots();
        }
    }

    private void SyncPivots()
    {
        activate = false;

        for (int i = 0; i < pivots.Length; i++)
        {
            pivots[i].position = buttons[i].position;
            pivots[i].rotation = buttons[i].rotation;
        }
    }
}
