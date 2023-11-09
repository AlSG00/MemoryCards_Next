using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "SavedSettings")]
public class SavedSettings : ScriptableObject
{
    public float masterVolume = 1;
    public int currentResolutionPreset = 4;
}
