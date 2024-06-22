using System.Collections.Generic;
using UnityEngine;

public class StandartCardFactory : CardFactory
{
    [SerializeField] private CardLayoutHandler _cardLayoutHandler;
    [SerializeField] private CardDataSet[] cardDataSetArray;

    private int _dataIndex = 0;
    private int _cardsToMatch = 0;
    private List<CardData> _dataToUse;
    private List<CardData> _currrentCardData;
    private GameProgression.Difficulty _currentCardDifficulty;

    public override void Initialize(int expectedCardCount, int cardsToMatch)
    {
        if (expectedCardCount == 0)
        {
            return;
        }

        _currentCardDifficulty = GameProgression.CardDifficulty;
        SetCurrentCardData();

        _cardsToMatch = cardsToMatch;
        _dataToUse = GetCardsToGenerate(expectedCardCount);
        _dataIndex = 0;
    }

    public override List<GameObject> CreateCards(int cardsToMatch)
    {
        List<GameObject> resultCardPack = new List<GameObject>();
        for (int i = 0; i < cardsToMatch; i++)
        {
            var card = Instantiate(_cardPrefab.gameObject, _cardLayoutHandler.CardsStartPosition.position, Quaternion.identity, _cardsParent);
            card.GetComponentInChildren<NEW_Card>().Initialize(_dataToUse[_dataIndex]);
            resultCardPack.Add(card);
        }
        _dataIndex++;

        return resultCardPack;
    }

    private void SetCurrentCardData()
    {
        if (_currentCardDifficulty == GameProgression.CardDifficulty &&
            _currrentCardData != null)
        {
            return;
        }

        _currrentCardData = new List<CardData>();
        for (int i = 0; i <= (int)_currentCardDifficulty; i++)
        {
            _currrentCardData.AddRange(cardDataSetArray[i].Set);
        }
    }

    //переименовать и переработать
    private List<CardData> GetCardsToGenerate(int countToGenerate)
    {
        if (countToGenerate % _cardsToMatch != 0)
        {
            throw new System.Exception($"Wrong CountToGenerate and cardsToMatch values: {countToGenerate}_{_cardsToMatch}. Must be multiple.");
        }

        int count = countToGenerate / _cardsToMatch;
        int cardDataIndex = Random.Range(0, _currrentCardData.Count);
        int cardDataIndexStep = 0;
        while (cardDataIndexStep == 0 || _currrentCardData.Count % cardDataIndexStep == 0)
        {
            cardDataIndexStep = Random.Range(0, _currrentCardData.Count);
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
