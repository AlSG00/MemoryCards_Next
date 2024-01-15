using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugFpsCounter : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Update()
    {
        text.text = $"Fps: {(int)(1.0f / Time.smoothDeltaTime)}";
    }
}
