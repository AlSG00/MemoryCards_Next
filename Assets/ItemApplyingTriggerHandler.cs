using UnityEngine;

public class ItemApplyingTriggerHandler : MonoBehaviour
{
    [SerializeField] private stri
    [SerializeField] private Collider _collider;

    private void Awake()
    {
        _collider ??= GetComponent<Collider>();
    }

    public static System.Action<bool> OnEnterTrigger;

    private void OnMouseEnter()
    {
        OnEnterTrigger?.Invoke(true);
    }
    private void OnMouseExit()
    {
        OnEnterTrigger?.Invoke(false);
    }

}
