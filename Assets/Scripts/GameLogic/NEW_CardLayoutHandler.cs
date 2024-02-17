using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NEW_CardLayoutHandler : MonoBehaviour
{
   // public GameObject TEMP_testTripleLayout;

    [Header("Main parameters")]
    public Transform CardsStartPosition;
    [SerializeField] private float _cardPlacingSpeed = 1f; 
    [SerializeField] private float _cardPlacementDelay = 0.1f;

    [Header("References")]
    [SerializeField] private NEW_CardGenerator cardGenerator;
    //[SerializeField] private NEW_GameProgression sessionProgress;

    [Header("Layouts collections")]
    [SerializeField] private GameObject _currentLayout;
    [SerializeField] private GameObject _twoCardLayout;
    [SerializeField] private List<GameObject> _tutorialLayouts;
    [SerializeField] private List<GameObject> _veryEasyLayoutSet = new List<GameObject>();
    [SerializeField] private List<GameObject> _easyLayoutSet = new List<GameObject>();
    [SerializeField] private List<GameObject> _mediumLayoutSet = new List<GameObject>();
    [SerializeField] private List<GameObject> _hardLayoutSet = new List<GameObject>();
    [SerializeField] private List<GameObject> _veryHardLayoutSet = new List<GameObject>();
    
    private List<Transform> _cardPlacePoints = new List<Transform>();
    private List<GameObject> _cardsInLayout;
    private bool _isPreparing = false;
    private bool _isPlacing = false;

    public static event System.Action CancelAllPicks;
    public static event System.Action<int> OnSetRemainingTurns;

    private void OnEnable()
    {
        NEW_GameProgression.OnPlayTutorial += PlayTutorialRound;
        RemainingTurnsHandler.OutOfTurns += TakeCardsBack;
    }

    private void OnDisable()
    {
        NEW_GameProgression.OnPlayTutorial -= PlayTutorialRound;
        RemainingTurnsHandler.OutOfTurns -= TakeCardsBack;
    }

    public void ReceiveNewCardPack(List<GameObject> newCardPack)
    {
        _cardsInLayout = newCardPack;
    }

    public void RemoveCertainCards(List<GameObject> cardsToRemove)
    {
        _cardsInLayout.RemoveAll(c => cardsToRemove.Contains(c));

        foreach (var card in cardsToRemove)
        {
            Destroy(card, 5f);
        }
    }

    private void PlayTutorialRound(int tutorialIndex)
    {
        if (_isPreparing == false)
        {
            _isPreparing = true;
            SetPlacePointsList(_tutorialLayouts[tutorialIndex]);
            MixPlacePoints();
            cardGenerator.GeneratePack(_cardPlacePoints.Count);
            PlaceCards();
            OnSetRemainingTurns?.Invoke(_cardPlacePoints.Count);
        }
    }

    public void PrepareStartLayout()
    {
        _cardPlacePoints.Clear();
        if (_isPreparing == false)
        {
            _isPreparing = true;
            _cardPlacePoints.Add(_twoCardLayout.transform.GetChild(0));
            _cardPlacePoints.Add(_twoCardLayout.transform.GetChild(1));
            cardGenerator.GeneratePack(_cardPlacePoints.Count);
            PlaceCards();
        }
    }

    public void PrepareNewLayout()
    {
        if (_isPreparing == false)
        {
            _isPreparing = true;
            SetPlasePoints();
            cardGenerator.GeneratePack(_cardPlacePoints.Count);
            PlaceCards();
            OnSetRemainingTurns?.Invoke(_cardPlacePoints.Count);
        }
    }

    public void ActivateCardColliders(bool activate)
    {
        for (int i = 0; i < _cardsInLayout.Count; i++)
        {
            _cardsInLayout[i].GetComponentInChildren<BoxCollider>().enabled = activate;
        }
    }

    public void SetPlasePoints(GameObject layout = null)
    {
        if (layout == null)
        {
            layout = _currentLayout;
        }

        if (_currentLayout != null)
        {
            _currentLayout.SetActive(false);
        }

        SetCurrentLayout();
        SetPlacePointsList(_currentLayout);
        MixPlacePoints();
    }

    private void SetPlacePointsList(GameObject layout)
    {
        if (layout.transform.childCount == 0)
        {
            throw new System.Exception("Layout object doesn't contain any child objects.");
        }

        _cardPlacePoints.Clear();
        for (int i = 0; i < layout.transform.childCount; i++)
        {
            _cardPlacePoints.Add(layout.transform.GetChild(i));
        }
    }

    #region SET LAYOUT
    private void SetCurrentLayout()
    {
        switch (NEW_GameProgression.LayoutDifficulty)
        {
            case NEW_GameProgression.Difficulty.VeryEasy:
                SetRandomLayout(_easyLayoutSet);
                break;

            case NEW_GameProgression.Difficulty.Easy:
                SetRandomLayout(_easyLayoutSet);
                break;

            case NEW_GameProgression.Difficulty.Medium:
                SetRandomLayout(_mediumLayoutSet);
                break;

            case NEW_GameProgression.Difficulty.Hard:
                SetRandomLayout(_hardLayoutSet);
                break;

            case NEW_GameProgression.Difficulty.VeryHard:
                SetRandomLayout(_veryHardLayoutSet);
                break;
        }

        _currentLayout.SetActive(true);
    }

    private void SetRandomLayout(List<GameObject> list)
    {
        _currentLayout = list[Random.Range(0, list.Count)];
    }
    #endregion

    public void PlaceCards()
    {
        StopAllCoroutines();
        StartCoroutine(MoveCardsRoutine());
    }

    public void TakeCardsBack()
    {
        StopAllCoroutines();
        ActivateCardColliders(false);
        CancelAllPicks?.Invoke();
        StartCoroutine(MoveCardsBackRoutine());
    }

    public void MixCards()
    {
        // TODO: Future random debuff
    }

    private IEnumerator MoveCardsRoutine()
    {
        _isPlacing = true;
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
        _isPreparing = false;
    }

    private IEnumerator MoveCardsBackRoutine()
    {
        _isPlacing = true;
        for (int i = 0; i < _cardsInLayout.Count; i++)
        {
            StartCoroutine(MoveCardToPositionRoutine(_cardsInLayout[i].gameObject, CardsStartPosition));

            if (i % 5 == 0)
            {
                yield return new WaitForSecondsRealtime(_cardPlacementDelay);
            }

            if (i == _cardsInLayout.Count - 1)
            {
                _isPlacing = false;
            }
        }

        //while (_isPlacing)
        //{
        //    yield return new WaitForSecondsRealtime(0.1f);
        //}

        //ActivateCardColliders(true);
        RemoveAllCards();
        _isPreparing = false;
    }

    private IEnumerator MoveCardToPositionRoutine(GameObject card, Transform positionToPlace)
    {
        //float speed = Random.Range(_cardPlacingSpeed - 0.02f, _cardPlacingSpeed + 0.02f);
        NEW_Card tempCard = card.GetComponentInChildren<NEW_Card>();
        tempCard.cardAudioSource.PlayOneShot(tempCard.PlaceSound);
        tempCard.cardAudioSource.pitch = Random.Range(0.95f, 1.05f);
        float elapsedTime = 0f;

        while (elapsedTime < _cardPlacingSpeed)
        {
            elapsedTime += Time.deltaTime;
            card.transform.position = Vector3.Lerp(card.transform.position, positionToPlace.position, elapsedTime / _cardPlacingSpeed);
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

    private void RemoveAllCards()
    {
        foreach (var card in _cardsInLayout)
        {
            Destroy(card, 5f);
        }

        _cardsInLayout.Clear();
    }
}