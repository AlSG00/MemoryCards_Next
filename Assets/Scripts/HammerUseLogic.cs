using UnityEngine;

[RequireComponent(typeof(InventoryItem))]
public class HammerUseLogic : MonoBehaviour, IUsable
{
    [SerializeField] private Animator _animator;

    public delegate void UseAction();

    public static UseAction OnUseHammer;

    private void Awake()
    {
        _animator ??= GetComponentInChildren<Animator>();
    }

    public void Use()
    {
        _animator.SetTrigger("Use");
        OnUseHammer?.Invoke(); // TODO: Finish logic after scriupting the stopwatch
    }
}