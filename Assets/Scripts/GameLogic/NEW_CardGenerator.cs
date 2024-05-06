using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEW_CardGenerator : MonoBehaviour
{
    private CardFactory _currentCardFactory;
    [SerializeField] private StandartCardFactory _standartCardFactory;

    //[SerializeField] private GameObject _cardPrefab;
    //[SerializeField] private Transform _cardsParent;

    [Header("References")]
    [SerializeField] private NEW_CardLayoutHandler _cardLayoutHandler;
    [SerializeField] private NEW_GameProgression _sessionProgress;

   

    [Header("Card collections")]
    [SerializeField] private CardDataSet[] cardDataSetArray;

    [HideInInspector] public List<GameObject> generatedCardPack = new List<GameObject>();
    [SerializeField] private CardData[] _tutorialCardsCollection;
    [SerializeField] private List<CardData> _dataToUse;
    private List<CardData> _activeCardData; // Stores data for cards that are used for generating at current game stage
    //NEW_GameProgression.Difficulty _currentCardDifficulty;

    public void GeneratePack(int countToGenerate)
    {
        //CheckAvailableCards();
        _dataToUse = GetCardsToGenerate(countToGenerate);

        int index = 0;
        //int dataIndex = 0;

        _currentCardFactory = _standartCardFactory;
        _currentCardFactory.Initialize(countToGenerate);

        // TODO: Rework this function for handling more than two cards
        while (index < countToGenerate)
        {
           // тут косяк, карты 
            generatedCardPack.AddRange(_currentCardFactory.CreateCards(2));
           // generatedCardPack.Add(_currentCardFactory.CreateCard());

            //generatedCardPack.Add(Instantiate(_cardPrefab.gameObject, _cardLayoutHandler.CardsStartPosition.position, Quaternion.identity, _cardsParent));
            //generatedCardPack.Add(Instantiate(_cardPrefab.gameObject, _cardLayoutHandler.CardsStartPosition.position, Quaternion.identity, _cardsParent));

            //generatedCardPack[index].GetComponentInChildren<NEW_Card>().Initialize(_dataToUse[dataIndex]);
            //generatedCardPack[index + 1].GetComponentInChildren<NEW_Card>().Initialize(_dataToUse[dataIndex]);

            index += 2;
            //dataIndex++;
        }

        MixCardPack();
        _cardLayoutHandler.ReceiveNewCardPack(generatedCardPack); // TODO: Rename method
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

    //private void SetAvailableCards()
    //{
    //    _currentCardDifficulty = NEW_GameProgression.CardDifficulty;
    //    _activeCardData = new List<CardData>();
    //    for (int i = 0; i <= (int)_currentCardDifficulty; i++)
    //    {
    //        _activeCardData.AddRange(cardDataSetArray[i].Set);
    //    }
    //}

    //private List<CardData> GetCardsToGenerate(int countToGenerate)
    //{
    //    int count = countToGenerate / 2;
    //    int cardDataIndex = Random.Range(0, _activeCardData.Count);
    //    int cardDataIndexStep = 0;

    //    int debugAttempt = 0;
    //    while (cardDataIndexStep == 0 || _activeCardData.Count % cardDataIndexStep == 0)
    //    {
    //        cardDataIndexStep = Random.Range(0, _activeCardData.Count);
            
    //        Debug.Log($"<color=orange>Attempt: {debugAttempt++}</color>");
    //    }

    //    List <CardData> resultData = new List<CardData>();
    //    for (int i = 0; i < count; i++)
    //    {
    //        resultData.Add(_activeCardData[cardDataIndex]);
    //        cardDataIndex = (cardDataIndex + cardDataIndexStep) % _activeCardData.Count;
    //    }

    //    return resultData;
    //}

    private void MixCardPack()
    {
        for (int i = 0; i < generatedCardPack.Count; i++)
        {
            int j = Random.Range(0, generatedCardPack.Count);
            GameObject temp = generatedCardPack[j];
            generatedCardPack[j] = generatedCardPack[i];
            generatedCardPack[i] = temp;
        }
    }

    internal bool CheckRemainingCards()
    {
        if (generatedCardPack.Count == 0)
        {
            return false;
        }

        return true;
    }

    //public void RemoveAllCards()
    //{
    //    for (int i = generatedCardPack.Count - 1; i >= 0; i--)
    //    {
    //        Destroy(generatedCardPack[i].gameObject);
    //        generatedCardPack.Remove(generatedCardPack[i]);
    //    }
    //}


}
