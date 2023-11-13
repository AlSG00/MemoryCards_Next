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
    [HideInInspector] public List<GameObject> currentCardPack = new List<GameObject>();
    [SerializeField] private CardData[] _tutorialCardsCollection;
    [SerializeField] private CardData[] _firstCardsCollection;
    [SerializeField] private CardData[] _secondCardsCollection;
    [SerializeField] private CardData[] _thirdCardsCollection;
    [SerializeField] private CardData[] _fourthCardsCollection;
    [SerializeField] private List<CardData> _dataToUse;
    private CardData[] _activeCardData;

    private void Start()
    {
        //_cardLayoutHandler = GameObject.Find("CardLayoutHandler").GetComponent<CardLayoutHandler>();
    }

    public void GeneratePack(int countToGenerate)
    {

        if (countToGenerate % 2 != 0)
        {
            Debug.LogError("Card generating error. Odd count");
            return;
        }

        // Карты сразу создаются парами, чтобы не возникло софт-лока из-за неполных пар во время игры
        //CheckCurrentRound();

        _activeCardData = _firstCardsCollection; // TODO: TEMP

        _dataToUse = GetCollectionToGenerate(countToGenerate);

        int index = 0;
        int dataIndex = 0;

        // TODO: Temporary changed for 3 cards mode
        while (index < countToGenerate)
        {
            currentCardPack.Add(Instantiate(_cardPrefab, _cardLayoutHandler.cardsStartPosition.position, Quaternion.identity, _cardsParent));
            currentCardPack.Add(Instantiate(_cardPrefab, _cardLayoutHandler.cardsStartPosition.position, Quaternion.identity, _cardsParent));
            currentCardPack.Add(Instantiate(_cardPrefab, _cardLayoutHandler.cardsStartPosition.position, Quaternion.identity, _cardsParent));

            currentCardPack[index].GetComponentInChildren<NEW_Card>().Initialize(_dataToUse[dataIndex]);
            currentCardPack[index + 1].GetComponentInChildren<NEW_Card>().Initialize(_dataToUse[dataIndex]);
            currentCardPack[index + 2].GetComponentInChildren<NEW_Card>().Initialize(_dataToUse[dataIndex]);


            //index += 2;
            index += 3; //TODO: TEMP
            dataIndex++;
        }

        MixCardPack();
    }

    public List<CardData> GetCollectionToGenerate(int countToGenerate)
    {
        //int count = countToGenerate / 2;
        int count = countToGenerate / 3; // TODO: TEMP

        List<CardData> resultData = new List<CardData>();
        for (int i = 0; i < count; i++)
        {
            resultData.Add(_activeCardData[Random.Range(0, _activeCardData.Length)]);
        }

        return resultData;
    }

    private void CheckCurrentRound()
    {
        //switch (_sessionProgress.currentRound)
        //{
        //    case 0:
        //        _activeCardData = _tutorialCardsCollection;
        //        return;
        //    case 1:
        //        _activeCardData = _firstCardsCollection;
        //        return;
        //    case 2:
        //        _activeCardData = _secondCardsCollection;
        //        return;
        //    case 3:
        //        _activeCardData = _thirdCardsCollection;
        //        return;
        //    default:
        //        _activeCardData = _fourthCardsCollection;
        //        return;
        //}
    }

    private void MixCardPack()
    {
        for (int i = 0; i < currentCardPack.Count; i++)
        {
            int j = Random.Range(0, currentCardPack.Count);
            GameObject temp = currentCardPack[j];
            currentCardPack[j] = currentCardPack[i];
            currentCardPack[i] = temp;
        }
    }

    public void RemoveConfirmedCards(Card[] pickedCards)
    {
        currentCardPack.Remove(pickedCards[0].transform.parent.gameObject);
        currentCardPack.Remove(pickedCards[1].transform.parent.gameObject);

        Destroy(pickedCards[0].transform.parent.gameObject, 5f);
        Destroy(pickedCards[1].transform.parent.gameObject, 5f);
    }

    public bool CheckRemainingCards()
    {
        if (currentCardPack.Count == 0)
        {
            return false;
        }

        return true;
    }

    public void RemoveAllCards()
    {
        for (int i = currentCardPack.Count - 1; i >= 0; i--)
        {
            Destroy(currentCardPack[i].gameObject);
            currentCardPack.Remove(currentCardPack[i]);
        }
    }
}
