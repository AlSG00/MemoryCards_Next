using UnityEngine;

public class ScaleSuspend : MonoBehaviour, IUsable
{
    public static System.Action OnSuspendGame;

    public void Use()
    {
        OnSuspendGame?.Invoke();
    }
}
