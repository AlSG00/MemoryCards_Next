using UnityEngine;

[RequireComponent(typeof(InventoryItem))]
public class ScrewdriverUseLogic : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public delegate void UseAction();

    public static UseAction OnUseScrewdriver;

    private void Awake()
    {
        _animator ??= GetComponentInChildren<Animator>();
    }

    public void Use()
    {
        //_animator.SetTrigger("Use");
        OnUseScrewdriver?.Invoke();
        Debug.Log($"{gameObject.name} Use()");
    }
}
