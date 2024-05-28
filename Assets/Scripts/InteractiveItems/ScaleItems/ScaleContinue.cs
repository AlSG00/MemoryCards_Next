using UnityEngine;

[RequireComponent(typeof(ScaleItem))]
public class ScaleContinue : MonoBehaviour, IUsable
{
    public static System.Action OnContinueGame;

    public void Use()
    {
        OnContinueGame?.Invoke();
    }
}
