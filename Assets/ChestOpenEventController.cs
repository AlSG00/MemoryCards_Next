using UnityEngine;

public class ChestOpenEventController : MonoBehaviour
{
    public static event System.Action EnableParticleSystem;

    public void TryActivateParticleSystem()
    {
        EnableParticleSystem?.Invoke();
    }
}
