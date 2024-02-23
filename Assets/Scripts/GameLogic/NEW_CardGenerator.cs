using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEW_CardGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform _cardsParent;

    [Header("References")]
    [SerializeField] private NEW_CardLayoutHandler _cardLayoutHandler;
    [SerializeField] private NEW_GameProgression _sessionProgress;

    [Header("Card collections")]
    [SerializeField] private CardDataSet[] cardDataSetArray;

    [HideInInspector] public List<GameObject> generatedCardPack = new List<GameObject>();
    [SerializeField] private CardData[] _tutorialCardsCollection;
    //[SerializeField] private CardData[] _easyCardsCollection;
    //[SerializeField] private CardData[] _mediumCardsCollection;
    //[SerializeField] private CardData[] _hardCardsCollection;
    //[SerializeField] private CardData[] _veryHardCardsCollection;
    [SerializeField] private List<CardData> _dataToUse;
    private List<CardData> _activeCardData; // Stores data for cards that are used for generating at current game stage

    public void GeneratePack(int countToGenerate)
    {
        SetAvailableCardsToGenerate();
        _dataToUse = GetCollectionToGenerate(countToGenerate);

        int index = 0;
        int dataIndex = 0;
        while (index < countToGenerate)
        {
            generatedCardPack.Add(Instantiate(_cardPrefab.gameObject, _cardLayoutHandler.CardsStartPosition.position, Quaternion.identity, _cardsParent));
            generatedCardPack.Add(Instantiate(_cardPrefab.gameObject, _cardLayoutHandler.CardsStartPosition.position, Quaternion.identity, _cardsParent));

            generatedCardPack[index].GetComponentInChildren<NEW_Card>().Initialize(_dataToUse[dataIndex]);
            generatedCardPack[index + 1].GetComponentInChildren<NEW_Card>().Initialize(_dataToUse[dataIndex]);

            index += 2;
            dataIndex++;
        }

        MixCardPack();
        _cardLayoutHandler.ReceiveNewCardPack(generatedCardPack);
    }

    public List<CardData> GetCollectionToGenerate(int countToGenerate)
    {
        int count = countToGenerate / 2;

        List<CardData> resultData = new List<CardData>();
        for (int i = 0; i < count; i++)
        {
            resultData.Add(_activeCardData[Random.Range(0, _activeCardData.Count)]);
        }

        return resultData;
    }

    private void SetAvailableCardsToGenerate()
    {
        // TODO: Check if here would be problems with empty elements
        _activeCardData.Clear();
        for (int i = 0; i <= (int)NEW_GameProgression.CardDifficulty; i++)
        {
            _activeCardData.AddRange(cardDataSetArray[i].Set);
        }
    }

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

    public bool CheckRemainingCards()
    {
        if (generatedCardPack.Count == 0)
        {
            return false;
        }

        return true;
    }

    public void RemoveAllCards()
    {
        for (int i = generatedCardPack.Count - 1; i >= 0; i--)
        {
            Destroy(generatedCardPack[i].gameObject);
            generatedCardPack.Remove(generatedCardPack[i]);
        }
    }

    [System.Serializable]
    public struct CardDataSet
    {
        public CardData[] Set;
    }
}
