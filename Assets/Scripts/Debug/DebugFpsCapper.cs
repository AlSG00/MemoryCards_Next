using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFpsCapper : MonoBehaviour
{
    public int cap;

    public void SetCap()
    {
        Application.targetFrameRate = cap;
    }
}
