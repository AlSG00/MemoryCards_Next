using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NEW_CardLayoutHandler : MonoBehaviour
{
    public GameObject TEMP_testTripleLayout;

    [Header("Main parameters")]
    public Transform cardsStartPosition;
    [SerializeField] private float _cardPlacingSpeed = 1f;
    [SerializeField] private float _cardPlacementDelay = 0.1f;

    [Header("References")]
    [SerializeField] private NEW_CardGenerator cardGenerator;
    //[SerializeField] private SessionProgressHandler sessionProgress;
    [SerializeField] private NEW_GameProgression sessionProgress;

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

    [SerializeField] private List<GameObject> _cardsInLayout;
    //private int 

    //private void OnEnable()
    //{
    //    CardComparator.OnMatchConfirm += RemoveConfirmedCards;
    //}

    //private void OnDisable()
    //{
    //    CardComparator.OnMatchConfirm -= RemoveConfirmedCards;
    //}

    private void Start()
    {
        cardGenerator = GameObject.Find("CardGenerator").GetComponent<NEW_CardGenerator>();
        sessionProgress = GameObject.Find("SessionProgressHandler").GetComponent<NEW_GameProgression>();
    }

    public void ReceiveNewCardPack(List<GameObject> newCardPack)
    {
        _cardsInLayout = newCardPack;
    }

    public void RemoveConfirmedCards(List<GameObject> cardsToRemove)
    {
        _cardsInLayout.RemoveAll(c => cardsToRemove.Contains(c)); // TODO: Check

        foreach (var card in cardsToRemove)
        {
            Destroy(card, 5f);
        }
    }

    public void PrepareNewLayout()
    {
        if (_isPreparing == false)
        {
            //sessionProgress.SetRound();
            _isPreparing = true;
            //if (sessionProgress.currentRound >= sessionProgress.roundToactivateTimer)
            //{
            //    sessionProgress.counterStarted = false;
            //}

            //sessionProgress.ResetDebuff();
            SetPlasePoints_TEMP();
            cardGenerator.GeneratePack(_cardPlacePoints.Count);
            PlaceCards();
        }
    }

    public void ActivateCardColliders(bool activate)
    {
        //for (int i = 0; i < cardGenerator.currentCardPack.Count; i++)
        //{
        //    cardGenerator.currentCardPack[i].GetComponentInChildren<BoxCollider>().enabled = activate;
        //}

        for (int i = 0; i < _cardsInLayout.Count; i++)
        {
            _cardsInLayout[i].GetComponentInChildren<BoxCollider>().enabled = activate;
        }
    }

    public void SetPlasePoints_TEMP()
    {
        _cardPlacePoints.Clear();
        for (int i = 0; i < TEMP_testTripleLayout.transform.childCount; i++)
        {
            _cardPlacePoints.Add(TEMP_testTripleLayout.transform.GetChild(i));
        }

        MixPlacePoints();
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
        //switch (sessionProgress.currentRound)
        //{
        //    case 0:
        //        _currentLayout = _tutorialLayout;
        //        break;

        //    case 1:
        //        _currentLayout = _firstPreparedLayouts[Random.Range(0, _firstPreparedLayouts.Count)];
        //        break;

        //    case 2:
        //        _currentLayout = _secondPreparedLayouts[Random.Range(0, _secondPreparedLayouts.Count)];
        //        break;

        //    case 3:
        //        _currentLayout = _thirdPreparedLayouts[Random.Range(0, _thirdPreparedLayouts.Count)];
        //        break;

        //    default:
        //        _currentLayout = _fourthPreparedLayouts[Random.Range(0, _fourthPreparedLayouts.Count)];
        //        break;
        //}

        //_currentLayout.SetActive(true);
    }

    public void PlaceCards()
    {
        StartCoroutine(MoveCardsRoutine());
    }

    private IEnumerator MoveCardsRoutine()
    {
        _isPlacing = true;
        //for (int i = 0; i < cardGenerator.currentCardPack.Count; i++)
        //{
        //    StartCoroutine(MoveCardToPositionRoutine(cardGenerator.currentCardPack[i], _cardPlacePoints[i]));
        //    yield return new WaitForSecondsRealtime(_cardPlacementDelay);
        //    if (i == cardGenerator.currentCardPack.Count - 1)
        //    {
        //        _isPlacing = false;
        //    }
        //}
        for (int i = 0; i < _cardsInLayout.Count; i++)
        {
            StartCoroutine(MoveCardToPositionRoutine(_cardsInLayout[i].gameObject, _cardPlacePoints[i]));
            yield return new WaitForSecondsRealtime(_cardPlacementDelay);
            if (i == _cardsInLayout.Count - 1)
            {
                _isPlacing = false;
            }
        }

        while (_isPlacing)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }

        ActivateCardColliders(true);

        //if (sessionProgress.currentRound >= sessionProgress.roundToactivateTimer)
        //{
        //    sessionProgress.counterStarted = true;
        //}
        _isPreparing = false;
    }

    private IEnumerator MoveCardToPositionRoutine(GameObject card, Transform positionToPlace)
    {
        float speed = Random.Range(_cardPlacingSpeed - 0.02f, _cardPlacingSpeed + 0.02f);
        NEW_Card tempCard = card.GetComponentInChildren<NEW_Card>();
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
