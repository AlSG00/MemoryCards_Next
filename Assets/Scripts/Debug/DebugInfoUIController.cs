using UnityEngine;
using TMPro;

public class DebugInfoUIController : MonoBehaviour
{
    [SerializeField] private InventoryItem _hammerPrefab;
    [SerializeField] private InventoryItem _screwdriverPrefab;

    [SerializeField] private NEW_GameProgression _gameProgression;
    [SerializeField] private PlayerMoney _playerMoney;
    [SerializeField] private Inventory _inventory;

    [SerializeField] private TextMeshProUGUI _currentRound;
    [SerializeField] private TextMeshProUGUI _currentMoney;
    [SerializeField] private TextMeshProUGUI _isTutorial;
    [SerializeField] private TextMeshProUGUI _tutorialProgress;
    [SerializeField] private TextMeshProUGUI _currentDifficulty;
    [SerializeField] private TextMeshProUGUI _isFirstTimePlaying;
    [SerializeField] private TextMeshProUGUI _isBuyRoundGoingOn;
    [SerializeField] private TextMeshProUGUI _currentScore;
    [SerializeField] private TextMeshProUGUI _elapsedTime;
    [SerializeField] private TextMeshProUGUI _startCardDifficulty;
    [SerializeField] private TextMeshProUGUI _startLayoutDifficulty;
    [SerializeField] private TextMeshProUGUI _currentCardDifficulty;
    [SerializeField] private TextMeshProUGUI _currentLayoutDifficulty;

    private void FixedUpdate()
    {
        _currentRound.text = $"Current round: {_gameProgression.currentRound.ToString()}";
        _currentMoney.text = $"Current money: {_playerMoney.CurrentGameMoney}";
        _isTutorial.text = $"Is tutorial going on: {_gameProgression.playingTutorial.ToString()}";
        _tutorialProgress.text = $"Tutorial progress: {_gameProgression._tutorialProgress}";
        //_currentDifficulty.text = $"Current difficulty: {NEW_GameProgression.stage.ToString()}";
        _isFirstTimePlaying.text = $"Is playing first time: {_gameProgression.firstTimePlaying.ToString()}";
        _isBuyRoundGoingOn.text = $"Is buy round going on: {_gameProgression.isBuyRoundGoing}";
        _currentScore.text = $"Score: {_gameProgression.score}";
        _elapsedTime.text = $"Elapsed time: {Mathf.Floor((float)_gameProgression.ElapsedPlayTime.Elapsed.TotalMinutes)} : {_gameProgression.ElapsedPlayTime.Elapsed.Seconds}";
        _startCardDifficulty.text = $"Start card difficulty: {NEW_GameProgression.StartCardDifficulty}";
        _startLayoutDifficulty.text = $"Start layout difficulty: {NEW_GameProgression.StartLayoutDifficulty}";
        _currentCardDifficulty.text = $"Current card difficulty: {NEW_GameProgression.CardDifficulty}";
        _currentLayoutDifficulty.text = $"Current layout difficulty: {NEW_GameProgression.LayoutDifficulty}";
    }

    public void AddHammer()
    {
        if (_inventory.HasFreeSlotDebug())
        {
            var hammer = Instantiate(_hammerPrefab);
            hammer.AddToInventory();
        }
    }

    public void AddScrewdriver()
    {
        if (_inventory.HasFreeSlotDebug())
        {
            var screwdriver = Instantiate(_screwdriverPrefab);
            screwdriver.AddToInventory();
        }
    }

    public void RemoveAllItems()
    {

    }
}
