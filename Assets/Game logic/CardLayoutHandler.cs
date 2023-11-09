//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardLayoutHandler : MonoBehaviour
{
    [Header("Main parameters")]
    public Transform cardsStartPosition;
    [SerializeField] private float _cardPlacingSpeed = 1f;
    [SerializeField] private float _cardPlacementDelay = 0.1f;

    [Header("References")]
    [SerializeField] private CardGenerator cardGenerator;
    [SerializeField] private SessionProgressHandler sessionProgress;

    [Header("Layouts collections")]
    [SerializeField] private GameObject _currentLayout;
    [SerializeField] private GameObject _tutorialLayout;
    [SerializeField] private List<GameObject> _firstPreparedLayouts = new List<GameObject>();
    [SerializeField] private List<GameObject> _secondPreparedLayouts = new List<GameObject>();
    [SerializeField] private List<GameObject> _thirdPreparedLayouts = new List<GameObject>();
    [SerializeField] private List<GameObject> _fourthPreparedLayouts = new List<GameObject>();
    [SerializeField] private List<Transform> _cardPlacePoints = new List<Transform>();
    
    private bool _isPreparing = false;
    private bool _isPlacing = false;

    private void Start()
    {
        cardGenerator = GameObject.Find("CardGenerator").GetComponent<CardGenerator>();
        sessionProgress = GameObject.Find("SessionProgressHandler").GetComponent<SessionProgressHandler>();
    }

    public void PrepareNewLayout()
    {
        if (!_isPreparing)
        {
            sessionProgress.SetRound();
            _isPreparing = true;
            if (sessionProgress.currentRound >= sessionProgress.roundToactivateTimer)
            {
                sessionProgress.counterStarted = false;
            }
            
            sessionProgress.ResetDebuff();
            SetPlasePoints();
            cardGenerator.GeneratePack(_cardPlacePoints.Count);
            PlaceCards();
        }
    }

    public void ActivateCardColliders(bool activate)
    {
        for (int i = 0; i < cardGenerator.currentCardPack.Count; i++)
        {
            cardGenerator.currentCardPack[i].GetComponentInChildren<BoxCollider>().enabled = activate;
        }
    }

    public void SetPlasePoints()
    {
        if (_currentLayout != null)
        {
            _currentLayout.SetActive(false);
            _cardPlacePoints.Clear();
        }

        SetCurrentLayout();
        
        for (int i = 0; i < _currentLayout.transform.childCount; i++)
        {
            _cardPlacePoints.Add(_currentLayout.transform.GetChild(i));
        }

        MixPlacePoints();
    }

    private void SetCurrentLayout()
    {
        switch(sessionProgress.currentRound)
        {
            case 0:
                _currentLayout = _tutorialLayout;
                break;

            case 1:
                _currentLayout = _firstPreparedLayouts[Random.Range(0, _firstPreparedLayouts.Count)];
                break;

            case 2:
                _currentLayout = _secondPreparedLayouts[Random.Range(0, _secondPreparedLayouts.Count)];
                break;

            case 3:
                _currentLayout = _thirdPreparedLayouts[Random.Range(0, _thirdPreparedLayouts.Count)];
                break;

            default:
                _currentLayout = _fourthPreparedLayouts[Random.Range(0, _fourthPreparedLayouts.Count)];
                break;
        }

        _currentLayout.SetActive(true);
    }

    public void PlaceCards()
    {
        StartCoroutine(MoveCardsRoutine());
    }

    private IEnumerator MoveCardsRoutine()
    {
        _isPlacing = true;
        for (int i = 0; i < cardGenerator.currentCardPack.Count; i++)
        {
            StartCoroutine(MoveCardToPositionRoutine(cardGenerator.currentCardPack[i], _cardPlacePoints[i]));
            yield return new WaitForSecondsRealtime(_cardPlacementDelay);
            if (i == cardGenerator.currentCardPack.Count - 1)
            {
                _isPlacing = false;
            }
        }

        while (_isPlacing)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }

        ActivateCardColliders(true);

        if (sessionProgress.currentRound >= sessionProgress.roundToactivateTimer)
        {
            sessionProgress.counterStarted = true;
        }
        _isPreparing = false;
    }

    private IEnumerator MoveCardToPositionRoutine(GameObject card, Transform positionToPlace)
    {
        float speed = Random.Range(_cardPlacingSpeed - 0.02f, _cardPlacingSpeed + 0.02f);
        Card tempCard = card.GetComponentInChildren<Card>();
        tempCard.cardAudioSource.PlayOneShot(tempCard.PlaceSound);
        float elapsedTime = 0f;

        while (elapsedTime < speed)
        {
            elapsedTime += Time.deltaTime;
            card.transform.position = Vector3.Lerp(card.transform.position, positionToPlace.position, elapsedTime / speed);
            yield return null;
        }
        
        card.transform.position = positionToPlace.position;
    }

    private void MixPlacePoints()
    {
        for (int i = 0; i < _cardPlacePoints.Count; i++)
        {
            int j = Random.Range(0, _cardPlacePoints.Count);
            var temp = _cardPlacePoints[j];
            _cardPlacePoints[j] = _cardPlacePoints[i];
            _cardPlacePoints[i] = temp;
        }
    }
}
