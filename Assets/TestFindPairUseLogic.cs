using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFindPairUseLogic : MonoBehaviour, IUsable
{
    [SerializeField] private CardData.Type _hoveredType;
    [SerializeField] private NEW_Card _hoveredCard;

    public static event System.Action<NEW_Card> OnUseTestItem;

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
        _hoveredCard = hoveredCard;
        _hoveredType = hoveredCard.cardType;
    }

    public void Use()
    {
        OnUseTestItem?.Invoke(_hoveredCard);
    }
}
