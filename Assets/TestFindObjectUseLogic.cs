using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFindObjectUseLogic : MonoBehaviour, IUsable
{
    [SerializeField] private int _cardShowDuration = 1000;
    [SerializeField] private CardData.Type _hoveredType;

    public static event System.Action<CardData.Type, int> OnUseTestItem;

    private void OnEnable()
    {
        ItemOnCardApplyingTriggerHandler.SetHoveredCardType += OnReceiveHoveredCardType;
    }

    private void OnDisable()
    {
        ItemOnCardApplyingTriggerHandler.SetHoveredCardType -= OnReceiveHoveredCardType;
    }

    private void OnReceiveHoveredCardType(NEW_Card hoveredCard)
    {
        _hoveredType = hoveredCard.cardType;
    }

    public void Use()
    {
        OnUseTestItem?.Invoke(_hoveredType, _cardShowDuration);
    }
}
