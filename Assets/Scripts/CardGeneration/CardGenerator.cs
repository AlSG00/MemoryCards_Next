using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private StandartCardFactory _standartCardFactory;
    [SerializeField] private CardLayoutHandler _cardLayoutHandler;
    [SerializeField] private NEW_GameProgression _sessionProgress;

    [Header("Card collections")]
    [HideInInspector] public List<GameObject> generatedCardPack = new List<GameObject>();
    [SerializeField] private CardData[] _tutorialCardsCollection;

    private CardFactory _currentCardFactory;
    //private List<CardData> _activeCardData; // Stores data for cards that are used for generating at current game stage

    public List<GameObject> GeneratePack(int countToGenerate)
    {
        int index = 0;
        int tempCardsToMatch = 2; // TODO: TEMP

        _currentCardFactory = _standartCardFactory;
        _currentCardFactory.Initialize(countToGenerate, tempCardsToMatch);

        while (index < countToGenerate)
        {
            generatedCardPack.AddRange(_currentCardFactory.CreateCards(tempCardsToMatch));
            index += tempCardsToMatch;
        }

        MixCardPack();

        return generatedCardPack;
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

    internal bool CheckRemainingCards()
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
