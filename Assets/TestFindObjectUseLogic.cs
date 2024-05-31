using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFindObjectUseLogic : MonoBehaviour, IUsable
{
    public static event System.Action<CardData.Type> OnUseTestItem;
    [SerializeField] private CardData.Type _hoveredType;

    private void OnEnable()
    {
        NEW_Card.SetHoveredCardType += OnReceiveHoveredCardType;
    }

    private void OnDisable()
    {
        NEW_Card.SetHoveredCardType -= OnReceiveHoveredCardType;
    }

    private void OnReceiveHoveredCardType(CardData.Type hoveredCardType)
    {
        _hoveredType = hoveredCardType;
    }

    public void Use()
    {
        OnUseTestItem?.Invoke(_hoveredType);
    }
}
