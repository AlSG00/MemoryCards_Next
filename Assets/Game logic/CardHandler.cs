using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandler : MonoBehaviour
{
    public Card[] pickedCard = new Card[2];
    [SerializeField] private CardLayoutHandler _cardLayoutHandler;
    [SerializeField] private CardGenerator _cardGenerator;
    [SerializeField] private SessionProgressHandler _sessionProgress;

    //private void Start()
    //{
    //    //_cardLayoutHandler = GameObject.Find("CardLayoutHandler").GetComponent<CardLayoutHandler>();
    //    //_cardGenerator = GameObject.Find("CardGenerator").GetComponent<CardGenerator>();
    //}

    public void Pick(Card card)
    {
        if (!_sessionProgress.firstStrangeCardMet &&
                card.isStrange)
        {
            _sessionProgress.StrangeCardMet();
        }

        if (!_sessionProgress.firstDarkCardMet &&
            card.isDark)
        {
            _sessionProgress.DarkCardMet();
        }

        if (pickedCard[0] == null)
        {
            pickedCard[0] = card;
        }
        else
        {
            pickedCard[1] = card;
            CompareCards();
        }
    }

    public void Unpick()
    {
        if (pickedCard[1] != null)
        {
            pickedCard[1] = null;
        }
        else
        {
            pickedCard[0] = null;
        }

        _sessionProgress.AddUnpickDebuff();
    }

    private void CompareCards()
    {
        Card[] tempCard = new Card[2];
        pickedCard.CopyTo(tempCard, 0);
        pickedCard[0] = null;
        pickedCard[1] = null;

        if (tempCard[0].cardType == tempCard[1].cardType)
        {   
            tempCard[0].ConfirmPick();
            tempCard[1].ConfirmPick();
            _sessionProgress.AddScore(tempCard[0].scoreValue);
            _sessionProgress.AddScore(tempCard[1].scoreValue);
            _cardGenerator.RemoveConfirmedCards(tempCard);
            _sessionProgress.AddTime(tempCard[0].scoreValue);
            if (!_cardGenerator.CheckRemainingCards())
            {
                _cardLayoutHandler.PrepareNewLayout();
            }
        }
        else
        {
            tempCard[0].CancelPick();
            tempCard[1].CancelPick();
            _sessionProgress.AddCancelDebuff();
        }
    }
}
