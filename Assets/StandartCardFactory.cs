using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartCardFactory : CardFactory
{
    //[SerializeField] private GameObject _cardPrefab;
    //[SerializeField] private Transform _cardsParent;

    [SerializeField] private List<CardData> _dataToUse;
    private NEW_GameProgression.Difficulty _currentCardDifficulty;
    private List<CardData> _currrentCardData;
    [SerializeField] private CardDataSet[] cardDataSetArray;
    private int _dataIndex = 0;
    public override void Initialize(int expectedCardCount)
    {
        if (expectedCardCount == 0)
        {
            return;
        }

        if (_currentCardDifficulty == NEW_GameProgression.CardDifficulty &&
            _currrentCardData != null)
        {
            return;
        }

        _currentCardDifficulty = NEW_GameProgression.CardDifficulty;
        SetCurrentCardData();

        _dataToUse = GetCardsToGenerate(expectedCardCount);
        _dataIndex = 0;
    }

    
    public override List<GameObject> CreateCards(int cardsToMatch)
    {
        List<GameObject> resultCardPack = new List<GameObject>();
        for (int i = 0; i < cardsToMatch; i++)
        {
            var card = Instantiate(_cardPrefab.gameObject, NEW_CardLayoutHandler.CardsStartPosition.position, Quaternion.identity, _cardsParent);
            card.GetComponentInChildren<NEW_Card>().Initialize(_dataToUse[_dataIndex]);
            resultCardPack.Add(card);
        }
        dataIndex++;

        return resultCardPack;
    }

    //private void CheckAvailableCards()
    //{
    //    if (_currentCardDifficulty == NEW_GameProgression.CardDifficulty &&
    //        _activeCardData != null)
    //    {
    //        return;
    //    }

    //    SetAvailableCards();
    //}

    private void SetCurrentCardData()
    {
        _currrentCardData = new List<CardData>();
        for (int i = 0; i <= (int)_currentCardDifficulty; i++)
        {
            _currrentCardData.AddRange(cardDataSetArray[i].Set);
        }
    }

    переименовать и переработать
    private List<CardData> GetCardsToGenerate(int countToGenerate)
    {
        int count = countToGenerate / 2;
        int cardDataIndex = Random.Range(0, _currrentCardData.Count);
        int cardDataIndexStep = 0;

        int debugAttempt = 0;
        while (cardDataIndexStep == 0 || _currrentCardData.Count % cardDataIndexStep == 0)
        {
            cardDataIndexStep = Random.Range(0, _currrentCardData.Count);

           // Debug.Log($"<color=orange>Attempt: {debugAttempt++}</color>");
        }

        List<CardData> resultData = new List<CardData>();
        for (int i = 0; i < count; i++)
        {
            resultData.Add(_currrentCardData[cardDataIndex]);
            cardDataIndex = (cardDataIndex + cardDataIndexStep) % _currrentCardData.Count;
        }

        return resultData;
    }

    [System.Serializable]
    public struct CardDataSet
    {
        public string Name;
        public CardData[] Set;
    }
}
