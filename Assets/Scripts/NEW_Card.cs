using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class NEW_Card : MonoBehaviour
{
    [Header("Main")]
    public CardData.Type cardType;
    public int scoreValue = 1;
    public Image cardDrawing;
    public BoxCollider cardCollider;
    public Animator cardAnimator;

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
    public static event System.Action<CardData.Type> SetHoveredCardType;

    private bool _wasPicked;

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
        SetHoveredCardType?.Invoke(cardType);
    }

    private void OnMouseExit()
    {
        cardAnimator.SetBool("mouseInArea", false);
        SetHoveredCardType?.Invoke(cardType);
    }

    private void OnMouseDown()
    {
        if (_wasPicked == false)
        {
            _wasPicked = true;
            TurnOver(PickSound, "picked");
            OnCardPicked?.Invoke(this);
        }
        else
        {
            _wasPicked = false;
            TurnOver(CancelSound, "unpicked");
            OnCardUnpicked?.Invoke(this);
        }

        OnHideFullList?.Invoke(false);
    }

    public void CancelPick()
    {
        if (_wasPicked == false)
        {
            return;
        }

        cardCollider.enabled = false;
        StartCoroutine(CancelCardRoutine());
    }

    public void ConfirmPick()
    {
        cardCollider.enabled = false;
        StartCoroutine(ConfirmCardRoutine());
    }

    public void TurnOver(AudioClip sound, string animationTrigger)
    {
        cardAudioSource.pitch = Random.Range(0.9f, 1.1f);
        cardAudioSource.PlayOneShot(PickSound);
        cardAnimator.SetTrigger(animationTrigger);
    }

    public async void TurnOverTemporarily()
    {
        TurnOver(PickSound, "picked");
        await Task.Delay(1000); // TODO: Make as customizable variable;
        TurnOver(CancelSound, "unpicked");
    }

    public IEnumerator ConfirmCardRoutine()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        cardAnimator.SetTrigger("confirmed");
    }

    public IEnumerator CancelCardRoutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        cardAudioSource.PlayOneShot(CancelSound);
        cardAnimator.SetTrigger("unpicked");
        _wasPicked = !_wasPicked;
        cardCollider.enabled = true;
    }
}
