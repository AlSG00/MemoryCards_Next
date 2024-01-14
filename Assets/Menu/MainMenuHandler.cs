using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] TextObjectArray;

    private void Awake()
    {
        ShowMenu();
    }

    private void OnEnable()
    {
        NEW_GameProgression.OnPressStart += HideMenu;
        RejectStartButton.OnGameStartReject += ShowMenu;
        SetLocaleButton.OnChooseLocale += ShowMenu;
        NEW_GameProgression.FirstTimePlaying += HideMenu;
    }

    private void OnDisable()
    {
        NEW_GameProgression.OnPressStart -= HideMenu;
        RejectStartButton.OnGameStartReject -= ShowMenu;
        SetLocaleButton.OnChooseLocale -= ShowMenu;
        NEW_GameProgression.FirstTimePlaying -= HideMenu;
    }

    // TODO: Make menu ui change it's visibility smoothly
    private void HideMenu()
    {
        //Debug.Log("Hide");
        SetMenuVisibility(false);
    }

    private void ShowMenu()
    {
        Debug.Log($"{this.name} Show");
        SetMenuVisibility(true);
    }

    private void SetMenuVisibility(bool isVisible)
    {
        //Debug.Log("MainMenu");
        foreach (var textMesh in TextObjectArray)
        {
            textMesh.SetActive(isVisible);
        }
    }

    //[SerializeField] private RectTransform _optionsMenuUi;
    //[SerializeField] private GameObject _mainMenuUi;
    //[SerializeField] private Animator _menuAnimator;

    //[Header("References")]
    //[SerializeField] private CardLayoutHandler _cardLayoutHandler;
    //[SerializeField] private LampHandler _lampHandler;
    //[SerializeField] private SessionProgressHandler _sessionProgress;
    //[SerializeField] private ScoreCounter _scoreCounter;

    //private bool _coroutineStarted = false;

    //private void Start()
    //{
    //    HideOptionsMenu();
    //    _sessionProgress.gameEnded = false;
    //}

    //public void StartGame()
    //{
    //    if (!_coroutineStarted)
    //    {
    //        _coroutineStarted = true;
    //        StartCoroutine(startGameRoutine());
    //    }
    //}

    //public void ShowOptionsMenu()
    //{
    //    _optionsMenuUi.position = new Vector2(
    //        Screen.currentResolution.width / 2,
    //        Screen.currentResolution.height / 2
    //        );
    //}

    //public void HideOptionsMenu()
    //{
    //    _optionsMenuUi.position = new Vector2(
    //        _optionsMenuUi.position.x,
    //        Screen.currentResolution.height * 2
    //        );
    //}

    //public void ExitGame()
    //{
    //    PlayerPrefs.Save();
    //    _lampHandler.TurnOffBeforeExit();
    //    _menuAnimator.SetTrigger("gameExit");
    //    gameObject.SetActive(false);
    //}

    //private IEnumerator startGameRoutine()
    //{
    //    _scoreCounter.UpdateScore(0);
    //    _menuAnimator.SetTrigger("gameStart");
    //    yield return new WaitForSecondsRealtime(2);
    //    _sessionProgress.ResetDebuff();
    //    _cardLayoutHandler.PrepareNewLayout();
    //    _coroutineStarted = false;
    //}
}
