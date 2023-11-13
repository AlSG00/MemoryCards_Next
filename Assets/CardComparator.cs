using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CardComparator : MonoBehaviour
{
    private List<NEW_Card> pickedCardList = null;

    public bool mimicOnTable;
    public bool extraOnTable;

    public static event Action OnMatchConfirm;

    private void OnEnable()
    {
        NEW_Card.OnCardPicked += PickCard;
        NEW_Card.OnCardUnpicked += UnpickCard;
    }

    private void OnDisable()
    {
        NEW_Card.OnCardPicked -= PickCard;
        NEW_Card.OnCardUnpicked -= UnpickCard;
    }

    private void Start()
    {
        pickedCardList = null;
        pickedCardList = new(3);
        pickedCardList = null;
    }

    public void PickCard(NEW_Card card)
    {
        if (pickedCardList == null)
        {
            pickedCardList = new List<NEW_Card>(card.requiredMatchesCount);
        }
        pickedCardList.Add(card);
        if (pickedCardList.Any(c => c.cardType != card.cardType))
        {
            UnpickAllCards();
            return;
        }

        if (pickedCardList.Count == card.requiredMatchesCount)
        {
            ConfirmMatch();
        }
    }

    private void ConfirmMatch()
    {
        foreach (var card in pickedCardList)
        {
            card.ConfirmPick();
        }

        pickedCardList = null;
        OnMatchConfirm?.Invoke();
    }

    public void UnpickCard(NEW_Card card)
    {
        if (pickedCardList.Count == 1)
        {
            pickedCardList = null;
        }
        else
        {
            pickedCardList.Remove(card);
        }
    }

    private void UnpickAllCards()
    {
        foreach (var card in pickedCardList)
        {
            card.CancelPick();
        }

        pickedCardList = null;
    }
}
