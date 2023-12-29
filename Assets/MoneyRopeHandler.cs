using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyRopeHandler : MonoBehaviour
{

    // TODO: money gui counter

    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isVisible;
    [SerializeField] private Transform[] _buttonPivots;
    [SerializeField] private GameObject[] _buttonsCollection;
    [SerializeField] private MoneySlot[] _moneySlots;
    [SerializeField] private int _currentAmount;

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        PlayerMoney.OnMoneyAmountChanged += SetButtonsAmount;
    }

    private void OnDisable()
    {
        PlayerMoney.OnMoneyAmountChanged -= SetButtonsAmount;
    }

    private void Start()
    {
        
    }

    private void SetButtonsAmount(int amount)
    {
        int originalAmount = _currentAmount;
        CountCurrentAmount();
        if (amount == _currentAmount)
        {
            return;
        }

        if (amount > _currentAmount)
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
        _moneySlots = new MoneySlot[_buttonPivots.Length];
        for (int i = 0; i < _buttonPivots.Length; i++)
        {
            _moneySlots[i] = new MoneySlot(_buttonPivots[i]);
        }
    }

    private void GenerateSeveralButtons(int amount)
    {
        for (int i = _currentAmount - 1; i < amount - 1; i++)
        {
            GenerateButton(_moneySlots[i]);
        }
    }

    private void RemoveSeveralButtons(int amount)
    {
        for (int i = _currentAmount - 1; i < amount - 1; i++)
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
        //if (_previous == null)
        //{
        //    _previous = button;
        //}

        //while (button.GetComponent<MeshFilter>() == _previous.GetComponent<MeshFilter>())
        //{
        //    button = _buttonsCollection[Random.Range(0, _buttonsCollection.Length)];
        //}
        slot.button = Instantiate(button, slot.pivot.position, slot.pivot.rotation);
        _previous = slot.button;
        _currentAmount++;
    }

    private void RemoveButton(MoneySlot slot)
    {
        if (slot.button == null)
        {
            return;
        }

        Destroy(slot.button);
        slot.button = null;
        _currentAmount--;
    }

    private void CountCurrentAmount()
    {
        _currentAmount = 0;
        foreach (var slot in _moneySlots)
        {
            if (slot.button != null)
            {
                _currentAmount++;
            }
            else
            {
                return;
            }
        }
    }

    #region VISIBILITY

    private void ChangeVisibility(bool isActive)
    {
        if (isActive == _isVisible)
        {
            return;
        }

        if (isActive)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        if (_isVisible == false)
        {
            _isVisible = true;
            _animator.SetTrigger("Show");
        }
    }

    private void Hide()
    {
        if (_isVisible)
        {
            _isVisible = false;
            _animator.SetTrigger("Hide");
        }
    }

    #endregion

    [System.Serializable]
    public class MoneySlot
    {
        public Transform pivot;
        public GameObject button;

        public MoneySlot(Transform newPivot)
        {
            pivot = newPivot;
        }
    }
}
