using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class NEW_Card : MonoBehaviour
{
    [Header("Main")]
    public CardData.Type cardType;
    public int scoreValue = 1;
    public Image cardDrawing;
    public BoxCollider cardCollider;
    public ItemOnCardApplyingTriggerHandler _itemApplyingTrigger;
    public Animator cardAnimator;
    private bool isAffectingByItem = false;
    
    public int requiredMatchesCount = 2;

    [Header("Audio")]
    public AudioSource cardAudioSource;
    public AudioClip PickSound;
    public AudioClip CancelSound;
    public AudioClip PlaceSound;

    public delegate void ClickAction(NEW_Card card);
    public static event ClickAction OnCardPicked;
    public static event ClickAction OnCardUnpicked;
    public static event System.Action<bool> OnHideFullList; //TODO: Rename
                                                            // public static event System.Action<CardData.Type> SetHoveredCardType
    protected internal bool IsPicked { get; private set; }

    private void OnEnable()
    {
        CardLayoutHandler.CancelAllPicks += CancelPick;
    }

    private void OnDisable()
    {
        CardLayoutHandler.CancelAllPicks -= CancelPick;
    }

    public void Initialize(CardData cardData)
    {
        cardType = cardData.type;
        cardDrawing.sprite = cardData.cardSprite[Random.Range(0, cardData.cardSprite.Length)];
        scoreValue = cardData.scoreValue;
    }

    private void OnMouseOver()
    {
        cardAnimator.SetBool("mouseInArea", true);
    }

    private void OnMouseEnter()
    {
        cardAnimator.SetBool("mouseInArea", true);
    }

    private void OnMouseExit()
    {
        cardAnimator.SetBool("mouseInArea", false);
    }

    private void OnMouseDown()
    {
        if (IsPicked == false)
        {
            Pick();
        }
        else
        {
            Unpick();
        }

        OnHideFullList?.Invoke(false);
    }

    internal void Pick()
    {
        if (IsPicked)
        {
            return;
        }

        IsPicked = true;
        TurnOver(PickSound, "picked");
        OnCardPicked?.Invoke(this);
    }

    internal void Unpick()
    {
        if (IsPicked == false)
        {
            return;
        }

        IsPicked = false;
        TurnOver(CancelSound, "unpicked");
        OnCardUnpicked?.Invoke(this);
    }

    public void CancelPick()
    {
        if (IsPicked == false)
        {
            return;
        }

        cardCollider.enabled = false;
        StartCoroutine(CancelCardPickRoutine());
    }

    public void ConfirmPick()
    {
        cardCollider.enabled = false;
        StartCoroutine(ConfirmCardPickRoutine());
    }

    public void TurnOver(AudioClip sound, string animationTrigger)
    {
        cardAudioSource.pitch = Random.Range(0.9f, 1.1f);
        cardAudioSource.PlayOneShot(PickSound);
        cardAnimator.SetTrigger(animationTrigger);
    }

    public async void TurnOverTemporarily(int showDuration)
    {
        if (_itemApplyingTrigger.IsActivated)
        {
            return;
        }

        if (IsPicked)
        {
            return;
        }

        _itemApplyingTrigger.IsActivated = true;
        cardCollider.enabled = false;
        TurnOver(PickSound, "picked");
        await Task.Delay(showDuration);

        TurnOver(CancelSound, "unpicked");
        if (GameProgression.IsGamePaused)
        {
            return;
        }

        cardCollider.enabled = true;
        _itemApplyingTrigger.IsActivated = false;
    }

    public IEnumerator ConfirmCardPickRoutine()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        cardAnimator.SetTrigger("confirmed");
    }

    public IEnumerator CancelCardPickRoutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        cardAudioSource.PlayOneShot(CancelSound);
        cardAnimator.SetTrigger("unpicked");
        IsPicked = !IsPicked;
        cardCollider.enabled = true;
    }
}
