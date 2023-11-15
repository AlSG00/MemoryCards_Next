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
    [SerializeField] private CardData[] _firstCardsCollection;
    [SerializeField] private CardData[] _secondCardsCollection;
    [SerializeField] private CardData[] _thirdCardsCollection;
    [SerializeField] private CardData[] _fourthCardsCollection;
    [SerializeField] private List<CardData> _dataToUse;
    private CardData[] _activeCardData; // Stores data for cards that are used for generating at current game stage

    private void Start()
    {
        //_cardLayoutHandler = GameObject.Find("CardLayoutHandler").GetComponent<CardLayoutHandler>();
    }

    public void GeneratePack(int countToGenerate)
    {
        _activeCardData = _firstCardsCollection; // TODO: TEMP
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
       // int count = countToGenerate / 3; // TODO: TEMP

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
