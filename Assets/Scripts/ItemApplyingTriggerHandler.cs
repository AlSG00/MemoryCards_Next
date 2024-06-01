using UnityEngine;

public class ItemApplyingTriggerHandler : MonoBehaviour
{
    [SerializeField] private protected ItemType[] _applyableItem;
    [SerializeField] private protected Collider _collider;
    internal bool IsActivated = false;

    public static System.Action<bool, ItemType[]> OnEnterTrigger;

    private protected void Awake()
    {
        _collider ??= GetComponent<Collider>();
        SetColliderEnabled(false);
    }

    private protected void OnEnable()
    {
        InventoryItem.OnPick += SetColliderEnabled;
    }

    private protected void OnDisable()
    {
        InventoryItem.OnPick -= SetColliderEnabled;
    }

    private protected virtual void OnMouseEnter()
    {
        OnEnterTrigger?.Invoke(true, _applyableItem);
    }
    private protected virtual void OnMouseExit()
    {
        OnEnterTrigger?.Invoke(false, _applyableItem);
    }

    private protected void SetColliderEnabled(bool isEnabled)
    {
        if (IsActivated)
        {
            return;
        }

        _collider.enabled = isEnabled;
    }
}
