using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyRopeHandler : MonoBehaviour
{
    [SerializeField] private int _currentButtonsAmount;
    [SerializeField] private Visibility _visibility;
    [SerializeField] private Animator _visibilityAnimator;
    [SerializeField] private Animator _visualFeedbackAnimator;
    [SerializeField] private Transform[] _buttonPivots;
    [SerializeField] private GameObject[] _buttonsCollection;
    [SerializeField] private MoneySlot[] _moneySlots;
    
    public enum Visibility
    {
        Visible,
        PartiallyVisible,
        Hidden
    }

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        PlayerMoney.OnMoneyAmountChanged += SetButtonsAmount;
        NEW_GameProgression.OnActivateMoneyRope += ChangeVisibility;
    }

    private void OnDisable()
    {
        PlayerMoney.OnMoneyAmountChanged -= SetButtonsAmount;
        NEW_GameProgression.OnActivateMoneyRope -= ChangeVisibility;
    }

    private void SetButtonsAmount(int amount)
    {
        _visualFeedbackAnimator.SetTrigger("MoneyChanged");

        int originalAmount = _currentButtonsAmount;
        CountCurrentAmount();
        if (amount == _currentButtonsAmount)
        {
            return;
        }

        if (amount > _currentButtonsAmount)
        {
            if (amount >= _buttonPivots.Length)
            {
                for (int i = originalAmount; i < _buttonPivots.Length; i++)
                {
                    GenerateButton(_moneySlots[i]);
                }
            }
            else
            {
                for (int i = originalAmount; i < amount; i++)
                {
                    GenerateButton(_moneySlots[i]);
                }
            }
        }
        else
        {
            for (int i = amount; i < originalAmount; i++)
            {
                RemoveButton(_moneySlots[i]);
            }
        }
    }

    public void DebugForceGenerateButtons()
    {
        foreach (var slot in _moneySlots)
        {
            ForceGenerateButton(slot);
        }
    }

    public void DebugRemoveButtons()
    {
        foreach (var slot in _moneySlots)
        {
            RemoveButton(slot);
        }
    }

    private void Initialize()
    {
        _visibility = Visibility.Hidden;
        _moneySlots = new MoneySlot[_buttonPivots.Length];
        for (int i = 0; i < _buttonPivots.Length; i++)
        {
            _moneySlots[i] = new MoneySlot(_buttonPivots[i]);
        }
    }

    private void GenerateSeveralButtons(int amount)
    {
        for (int i = _currentButtonsAmount - 1; i < amount - 1; i++)
        {
            GenerateButton(_moneySlots[i]);
        }
    }

    private void RemoveSeveralButtons(int amount)
    {
        for (int i = _currentButtonsAmount - 1; i < amount - 1; i++)
        {
            GenerateButton(_moneySlots[i]);
        }
    }

    private void ForceGenerateButton(MoneySlot slot)
    {
        if (slot.button != null)
        {
            RemoveButton(slot);
        }

        GenerateButton(slot);
    }

    private GameObject _previous;
    private void GenerateButton(MoneySlot slot)
    {
        GameObject button = _buttonsCollection[Random.Range(0, _buttonsCollection.Length)];
        slot.button = Instantiate(button, slot.pivot.position, slot.pivot.rotation);
        slot.button.transform.SetParent(slot.pivot);
        _previous = slot.button;
        _currentButtonsAmount++;
    }

    private void RemoveButton(MoneySlot slot)
    {
        if (slot.button == null)
        {
            return;
        }

        Destroy(slot.button);
        slot.button = null;
        _currentButtonsAmount--;
    }

    private void CountCurrentAmount()
    {
        _currentButtonsAmount = 0;
        foreach (var slot in _moneySlots)
        {
            if (slot.button != null)
            {
                _currentButtonsAmount++;
            }
            else
            {
                return;
            }
        }
    }

    #region VISIBILITY

    private void ChangeVisibility(Visibility visibility)
    {
        if (visibility == _visibility)
        {
            return;
        }
        
        switch (visibility)
        {
            case Visibility.Visible:
                Show();
                break;

            case Visibility.PartiallyVisible:
                SetPartialVisibility();
                break;

            case Visibility.Hidden:
                Hide();
                break;
        }

        _visibility = visibility;
    }

    private void Show()
    {
        if (_visibility == Visibility.Hidden)
        {
            _visibilityAnimator.SetTrigger("ShowPartially");
        }

        _visibilityAnimator.SetTrigger("Show");
    }

    private void Hide()
    {
        if (_visibility == Visibility.Visible)
        {
            _visibilityAnimator.SetTrigger("HidePartially");
        }

        _visibilityAnimator.SetTrigger("Hide");
    }

    private void SetPartialVisibility()
    {
        if (_visibility == Visibility.Hidden)
        {
            _visibilityAnimator.SetTrigger("ShowPartially");
        }
        else if (_visibility == Visibility.Visible)
        {
            _visibilityAnimator.SetTrigger("HidePartially");
        }
    }

    #endregion

    [System.Serializable]
    public sealed class MoneySlot
    {
        public Transform pivot;
        public GameObject button;

        public MoneySlot(Transform newPivot)
        {
            pivot = newPivot;
        }
    }
}
