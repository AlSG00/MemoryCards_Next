using UnityEngine;

public class ItemApplyingTriggerHandler : MonoBehaviour
{
    [SerializeField] private ItemType _applyableItem;
    [SerializeField] private Collider _collider;

    public static System.Action<bool, ItemType> OnEnterTrigger;

    private void Awake()
    {
        _collider ??= GetComponent<Collider>();
        SetColliderEnabled(false);
    }

    private void OnEnable()
    {
        InventoryItem.OnPick += SetColliderEnabled;
    }

    private void OnDisable()
    {
        InventoryItem.OnPick -= SetColliderEnabled;
    }

    private void OnMouseEnter()
    {
        OnEnterTrigger?.Invoke(true, _applyableItem);
    }
    private void OnMouseExit()
    {
        OnEnterTrigger?.Invoke(false, _applyableItem);
    }

    private void SetColliderEnabled(bool isEnabled)
    {
        _collider.enabled = isEnabled;
    }
}
