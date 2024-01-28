using UnityEngine;

public class ItemApplyingTriggerHandler : MonoBehaviour
{
    [SerializeField] private ItemType _applyableItem;
    [SerializeField] private Collider _collider;

    private void Awake()
    {
        _collider ??= GetComponent<Collider>();
    }

    public static System.Action<bool, ItemType> OnEnterTrigger;

    private void OnMouseEnter()
    {
        OnEnterTrigger?.Invoke(true, _applyableItem);
    }
    private void OnMouseExit()
    {
        OnEnterTrigger?.Invoke(false, _applyableItem);
    }

}
