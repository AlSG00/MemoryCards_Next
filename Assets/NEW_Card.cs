using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NEW_Card : MonoBehaviour
{
    [Header("Main")]
    public CardData.Type cardType;
    public int scoreValue = 1;
    //public bool isDark = false;
    //public bool isStrange = false;
    public Image cardDrawing;
    public BoxCollider cardCollider;
    public Animator cardAnimator;

    // Various mathce
    public int requiredMatchesCount = 3;

    //[Header("References")]
    //public CardHandler cardHandler;

    [Header("Audio")]
    public AudioSource cardAudioSource;
    public AudioClip PickSound;
    public AudioClip CancelSound;
    public AudioClip PlaceSound;

    private bool _wasPicked;

    public delegate void ClickAction(NEW_Card card);
    public static event ClickAction OnCardPicked;
    public static event ClickAction OnCardUnpicked;

    //private void Start()
    //{
    //    cardHandler = GameObject.Find("CardHandler").GetComponent<CardHandler>();
    //}

    public void Initialize(CardData cardData)
    {
        cardType = cardData.type;
        cardDrawing.sprite = cardData.cardSprite[Random.Range(0, cardData.cardSprite.Length)];
        scoreValue = cardData.scoreValue;
        //isDark = cardData.isDarkCard;
        //isStrange = cardData.isStrangeCard;
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
        if (_wasPicked == false)
        {
            cardAudioSource.PlayOneShot(PickSound);
            cardAnimator.SetTrigger("picked");
            //cardHandler.Pick(this);
            OnCardPicked?.Invoke(this);
        }
        else
        {
            cardAudioSource.PlayOneShot(CancelSound);
            cardAnimator.SetTrigger("unpicked");
            //cardHandler.Unpick();
            OnCardUnpicked?.Invoke(this);
        }

        _wasPicked = !_wasPicked;
    }

    public void CancelPick()
    {
        cardCollider.enabled = false;
        StartCoroutine(CancelCardRoutine());
    }

    public void ConfirmPick()
    {
        cardCollider.enabled = false;
        StartCoroutine(ConfirmCardRoutine());
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
