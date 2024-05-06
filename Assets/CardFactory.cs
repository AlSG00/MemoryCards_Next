using System.Collections.Generic;
using UnityEngine;

public abstract class CardFactory : MonoBehaviour
{
    [SerializeField] private protected GameObject _cardPrefab;
    [SerializeField] private protected Transform _cardsParent;

    public abstract void Initialize(int expectedCardCount);

    public abstract List<GameObject> CreateCards(int cardsToMatch);
}
