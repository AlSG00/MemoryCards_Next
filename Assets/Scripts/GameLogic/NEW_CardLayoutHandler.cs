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

    private List<GameObject> _availableLayouts;
    private NEW_GameProgression.Difficulty _currentLayoutDifficulty;
    //[SerializeField] private List<GameObject> _veryEasyLayoutSet = new List<GameObject>();
    //[SerializeField] private List<GameObject> _easyLayoutSet = new List<GameObject>();
    //[SerializeField] private List<GameObject> _mediumLayoutSet = new List<GameObject>();
    //[SerializeField] private List<GameObject> _hardLayoutSet = new List<GameObject>();
    //[SerializeField] private List<GameObject> _veryHardLayoutSet = new List<GameObject>();

    [SerializeField] private Layout[] _layoutDifficultyVariantSet;

    private List<Transform> _cardPlacePoints = new List<Transform>();
    private List<GameObject> _cardsInLayout;
    private bool _isPreparing = false;
    private bool _isPlacing = false;

    public static event System.Action CancelAllPicks;
    public static event System.Action<int, int> OnSetRemainingTurns;

    private void OnEnable()
    {
        NEW_GameProgression.OnPlayTutorial += PlayTutorialRound;
        RemainingTurnsHandler.OutOfTurns += TakeCardsBack;
        Stopwatch.OutOfTime += TakeCardsBack;
    }

    private void OnDisable()
    {
        NEW_GameProgression.OnPlayTutorial -= PlayTutorialRound;
        RemainingTurnsHandler.OutOfTurns -= TakeCardsBack;
        Stopwatch.OutOfTime -= TakeCardsBack;
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

    private void PlayTutorialRound(int tutorialIndex, int currentRound)
    {
        if (_isPreparing == false)
        {
            _isPreparing = true;
            SetPlacePointsList(_tutorialLayouts[tutorialIndex]);
            MixPlacePointsOrder();
            cardGenerator.GeneratePack(_cardPlacePoints.Count);
            PlaceCards();
            OnSetRemainingTurns?.Invoke(_cardPlacePoints.Count, currentRound);
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

    public void PrepareNewLayout(int currentRound)
    {
        if (_isPreparing)
        {
            return;
        }

        _isPreparing = true;
        InitializeLayout();
        cardGenerator.GeneratePack(_cardPlacePoints.Count);
        PlaceCards();

        OnSetRemainingTurns?.Invoke(_cardPlacePoints.Count, currentRound);
    }

    public void ActivateCardColliders(bool activate)
    {
        for (int i = 0; i < _cardsInLayout.Count; i++)
        {
            _cardsInLayout[i].GetComponentInChildren<BoxCollider>().enabled = activate;
        }
    }

    public void InitializeLayout(GameObject layout = null)
    {
        if (layout == null)
        {
            layout = _currentLayout;
        }

        if (_currentLayout != null)
        {
            _currentLayout.SetActive(false);
        }

        CheckAvailableLayouts();
        //SetAvailableLayouts();
        GetRandomAvailableLayout();
        SetPlacePointsList(_currentLayout);
        MixPlacePointsOrder();
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
    private void CheckAvailableLayouts()
    {
        if (_currentLayoutDifficulty == NEW_GameProgression.LayoutDifficulty &&
            _availableLayouts != null)
        {
            return;
        }

        if (NEW_GameProgression.LayoutDifficulty == NEW_GameProgression.Difficulty.Random)
        {
            if (NEW_GameProgression.CardDifficulty == NEW_GameProgression.Difficulty.Random)
            {
                return;
            }

            NEW_GameProgression.LayoutDifficulty = NEW_GameProgression.StartLayoutDifficulty;
        }

        SetAvailableLayouts();
    }

    private void SetAvailableLayouts()
    {
        _currentLayoutDifficulty = NEW_GameProgression.LayoutDifficulty;
        _availableLayouts = new List<GameObject>();

        if (_currentLayoutDifficulty == NEW_GameProgression.Difficulty.Random)
        {
            for (int i = 0; i <= (int)_currentLayoutDifficulty; i++)
            {
                _availableLayouts.AddRange(_layoutDifficultyVariantSet[i].ArrayOfSets);
            }
        }
        else
        {
            _availableLayouts.AddRange(_layoutDifficultyVariantSet[(int)_currentLayoutDifficulty].ArrayOfSets);
        }
    }

    private void GetRandomAvailableLayout()
    {
        _currentLayout = _availableLayouts[Random.Range(0, _availableLayouts.Count)];
        _currentLayout.SetActive(true);
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

        RemoveAllCards();
        _isPreparing = false;
    }

    private IEnumerator MoveCardToPositionRoutine(GameObject card, Transform positionToPlace)
    {
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

    private void MixPlacePointsOrder()
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

    [System.Serializable]
    public struct Layout
    {
        public string Name;
        public GameObject[] ArrayOfSets;
    }
}