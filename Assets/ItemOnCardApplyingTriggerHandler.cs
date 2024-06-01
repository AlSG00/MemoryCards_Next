using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnCardApplyingTriggerHandler : ItemApplyingTriggerHandler
{
    [SerializeField] private NEW_Card _parentCard;

    public static event System.Action<NEW_Card> SetHoveredCardType;

    private protected override void OnMouseEnter()
    {
        base.OnMouseEnter();
        SetHoveredCardType?.Invoke(_parentCard);
    }

    private protected override void OnMouseExit()
    {
        base.OnMouseExit();
        SetHoveredCardType?.Invoke(null);
    }
}
