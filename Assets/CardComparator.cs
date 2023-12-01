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

    public delegate void CardMatch(List<GameObject> matchedCards/*, NEW_GameProgression.RoundType roundType*/);
    public static event CardMatch OnPickConfirm;

    private void OnEnable()
    {
        NEW_Card.OnCardPicked += PickCard;
        NEW_Card.OnCardUnpicked += UnpickCard;
        RejectStartButton.OnGameStartReject += UnpickAllCards;
    }

    private void OnDisable()
    {
        NEW_Card.OnCardPicked -= PickCard;
        NEW_Card.OnCardUnpicked -= UnpickCard;
        RejectStartButton.OnGameStartReject += UnpickAllCards;
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
            OnPickConfirm?.Invoke(null);
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
        List<GameObject> confirmedCards = new List<GameObject>();
        foreach (var card in pickedCardList)
        {
            card.ConfirmPick();
            confirmedCards.Add(card.transform.parent.gameObject);
        }

        OnPickConfirm?.Invoke(confirmedCards);
        pickedCardList = null;
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
        OnPickConfirm?.Invoke(null);
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
