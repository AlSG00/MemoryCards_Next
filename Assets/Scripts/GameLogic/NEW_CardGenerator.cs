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
    [HideInInspector] public List<GameObject> generatedCardPack = new List<GameObject>();
    [SerializeField] private CardData[] _tutorialCardsCollection;
    [SerializeField] private CardData[] _easyCardsCollection;
    [SerializeField] private CardData[] _mediumCardsCollection;
    [SerializeField] private CardData[] _hardCardsCollection;
    [SerializeField] private CardData[] _veryHardCardsCollection;
    [SerializeField] private List<CardData> _dataToUse;
    private CardData[] _activeCardData; // Stores data for cards that are used for generating at current game stage

    private void Start()
    {
        //_cardLayoutHandler = GameObject.Find("CardLayoutHandler").GetComponent<CardLayoutHandler>();
    }

    public void GeneratePack(int countToGenerate)
    {
        //_activeCardData = _easyCardsCollection; // TODO: TEMP
        SetCardsToGenerate();
        _dataToUse = GetCollectionToGenerate(countToGenerate);

        int index = 0;
        int dataIndex = 0;
        while (index < countToGenerate)
        {
            generatedCardPack.Add(Instantiate(_cardPrefab.gameObject, _cardLayoutHandler.cardsStartPosition.position, Quaternion.identity, _cardsParent));
            generatedCardPack.Add(Instantiate(_cardPrefab.gameObject, _cardLayoutHandler.cardsStartPosition.position, Quaternion.identity, _cardsParent));

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
            resultData.Add(_activeCardData[Random.Range(0, _activeCardData.Length)]);
        }

        return resultData;
    }

    private void SetCardsToGenerate()
    {
        switch (NEW_GameProgression.stage)
        {
            case NEW_GameProgression.GameStage.Easy:
                _activeCardData = _easyCardsCollection;
                break;

            case NEW_GameProgression.GameStage.Medium:
                _activeCardData = _mediumCardsCollection;
                break;

            case NEW_GameProgression.GameStage.Hard:
                _activeCardData = _hardCardsCollection;
                break;

            case NEW_GameProgression.GameStage.VeryEasy:
                _activeCardData = _veryHardCardsCollection;
                break;

            case NEW_GameProgression.GameStage.FullRandom:
                _activeCardData = _veryHardCardsCollection; // TODO: Temp
                break;
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

    //public void RemoveConfirmedCards(Card[] pickedCards)
    //{
    //    generatedCardPack.Remove(pickedCards[0].transform.parent.gameObject);
    //    generatedCardPack.Remove(pickedCards[1].transform.parent.gameObject);

    //    Destroy(pickedCards[0].transform.parent.gameObject, 5f);
    //    Destroy(pickedCards[1].transform.parent.gameObject, 5f);
    //}

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
}
