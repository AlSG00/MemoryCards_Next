using UnityEngine;

[RequireComponent(typeof(InventoryItem))]
public class ScrewdriverUseLogic : MonoBehaviour, IUsable
{
    public delegate void UseAction();
    public static UseAction OnUseScrewdriver;

    public void Use()
    {
        OnUseScrewdriver?.Invoke();
    }
}
