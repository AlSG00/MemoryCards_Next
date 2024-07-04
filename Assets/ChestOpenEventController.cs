using UnityEngine;

public class ChestOpenEventController : MonoBehaviour
{
    public static event System.Action EnableParticleSystem;
    public static event System.Action ActivateBonusEffect;

    public void TryActivateParticleSystem()
    {
        EnableParticleSystem?.Invoke();
    }

    public void TryActivateBonusEffect()
    {
        ActivateBonusEffect?.Invoke();
    }
}
