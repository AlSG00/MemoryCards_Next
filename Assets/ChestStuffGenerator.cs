using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestStuffGenerator : MonoBehaviour
{
    [SerializeField] private int _stuffChance;
    [SerializeField] private int _buffChance;
    [SerializeField] private int _bonusEffectChance;
    [SerializeField] private int _debuffChance;
    [SerializeField] private int _inventoryItemChance;
    [SerializeField] private int _bonusButtonsChance;
    [SerializeField] private int _bonusCoinsChance;
    [SerializeField] private int _oneItemChance;
    [SerializeField] private int _twoItemChance;
    [SerializeField] private int _threeItemChance;
    [SerializeField] private int _fourItemChance;



    [SerializeField] private GameObject[] _coinsObject;
    [SerializeField] private GameObject[] _buttonsObject;
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private Color _particleBuffColor;
    [SerializeField] private Color _particleDebuffColor;

    // [SerializeField] private StuffSpawnpoint[] _spawnpoint;

    private int _stuffToGenerateQuantity;
    private int _currentStuffChance;
    private int _currentBuffChance;
    private int _currentDebuffChance;
    private int _currentInventoryItemChance;
    private int _currentBonusButtonsChance;
    private int _currentBonusCoinsChance;
    private bool _hasBonusEffect;
    private ActivateEffectAction _effect;

    public delegate void ActivateEffectAction();
    public static event ActivateEffectAction test_1;
    public static event ActivateEffectAction test_2;

    public enum StuffType
    {
        None,
        Buff,
        Debuff,
        InventoryItem,
        Buttons,
        Coins
    }

    private void OnEnable()
    {
        Chest.OnOpenChest += ChooseStuff;
        ChestOpenEventController.EnableParticleSystem += ActivateParticleSystem;
    }

    private void OnDisable()
    {
        Chest.OnOpenChest -= ChooseStuff;
        ChestOpenEventController.EnableParticleSystem -= ActivateParticleSystem;
    }

    private void ChooseStuff(ItemType usedKeyType)
    {
        List<StuffType> stuffToGenerate = new List<StuffType>();
        // TODO: Add variable for storing bonus effect (need to make an interface)

        ResetChestState();
        SetChanceValues(usedKeyType);
        GenerateBonusEffect(ref stuffToGenerate);
        GenerateFutureStuffList(ref stuffToGenerate);
        Generate(stuffToGenerate);
    }

    private void AddToStuffList(StuffType stuff, ref List<StuffType> stuffList)
    {
        if (stuffList.Contains(stuff))
        {
            return;
        }

        stuffList.Add(stuff);
    }

    private void GenerateBonusEffect(ref List<StuffType> stuffList)
    {
        int bonusEffectChance = Random.Range(1, 100);
        int debuffChange = Random.Range(1, 100);
        Debug.Log($"Bonus effect chance: {bonusEffectChance}");

        if (bonusEffectChance <= _bonusEffectChance)
        {
            Debug.Log($"Debuff chanceL: {debuffChange}");
            _hasBonusEffect = true;
            var main = _particles.main;
            if (debuffChange <= _debuffChance)
            {
                _effect = test_1;
                main.startColor = _particleDebuffColor;
            }
            else
            {
                _effect = test_2;
                main.startColor = _particleBuffColor;
            }
        }
    }

    private void SetChanceValues(ItemType usedKeyType)
    {
        if (usedKeyType == ItemType.Key_GuaranteedStuff_Safe)
        {
            _currentDebuffChance = 0;
            _currentStuffChance = 100;
        }
    }

    private void GenerateFutureStuffList(ref List<StuffType> stuffList)
    {
        int stuffChance = Random.Range(1, 100);
        int chance;
        int stuffToGenerateQuantity = 0;
        Debug.Log($"Stuff chance: {stuffChance}");
        // �������� ����� ��������� - ��� ������ ���������, ��� ���� ����
        // �������� ���� ��������� ����������
        // �� ���������� ����� � ����� ������� ���� ������ ���� �������
        // ����� �������� �������� ������
        // ����� �������� �������� ������� ����� ���������� ������� � �����

        if (stuffChance < _stuffChance)
        {
            int stuffQuantityChance = Random.Range(1, 100);
            Debug.Log($"Stuff qantity chance: {stuffQuantityChance}");
            if (stuffQuantityChance < _fourItemChance)
            {
                stuffToGenerateQuantity = 4;
            }
            else if (stuffQuantityChance < _threeItemChance)
            {
                stuffToGenerateQuantity = 3;
            }
            else if (stuffQuantityChance < _twoItemChance)
            {
                stuffToGenerateQuantity = 2;
            }
            else
            {
                stuffToGenerateQuantity = 1;
            }

            while (stuffList.Count < stuffToGenerateQuantity)
            {
                chance = Random.Range(1, 100);
                if (chance < _bonusButtonsChance && stuffList.Count < stuffToGenerateQuantity)
                {
                    stuffList.Add(StuffType.Buttons);
                }

                chance = Random.Range(1, 100);
                if (chance < _bonusCoinsChance && stuffList.Count < stuffToGenerateQuantity)
                {
                    stuffList.Add(StuffType.Coins);
                }

                chance = Random.Range(1, 100);
                if (chance < _inventoryItemChance && stuffList.Count < stuffToGenerateQuantity)
                {
                    AddToStuffList(StuffType.InventoryItem, ref stuffList);
                }
            }
        }
    }

    private void Generate(List<StuffType> stuffToGenerate)
    {
        //foreach (var stuff in stuffToGenerate)
        //{
        //    switch (stuff)
        //    {
        //        case (StuffType.Coins):
        //            _coinsObject[stuff].SetActive(true); // �������� ������ � ���������
        //            break;

        //        case (StuffType.Buttons):
        //            ButtonsObject.SetActive(true);
        //            break;

        //        case (StuffType.Buff):
        //            break;

        //        case (StuffType.Debuff):
        //            break;

        //        case (StuffType.InventoryItem):
        //            break;
        //    }
        //}

        for (int i = 0; i < stuffToGenerate.Count; i++)
        {
            if (stuffToGenerate[i] == StuffType.Coins)
            {
                Debug.Log("Generated coin");
                _coinsObject[i].GetComponent<BonusCoins>().Quantity = 3;
                _coinsObject[i].SetActive(true);
            }
            else if (stuffToGenerate[i] == StuffType.Buttons)
            {
                Debug.Log("Generated buttons");
                _buttonsObject[i].GetComponent<BonusButtons>().Quantity = 2;
                _buttonsObject[i].SetActive(true);
            }
            else if (stuffToGenerate[i] == StuffType.InventoryItem)
            {
                // TODO: Place item on spawnpoint transform
                Debug.Log("Tried to generate InventoryItem");
            }
        }
    }

    private void ResetChestState()
    {
        foreach (var obj in _coinsObject)
        {
            obj.SetActive(false);
        }

        foreach (var obj in _buttonsObject)
        {
            obj.SetActive(false);
        }

        _stuffToGenerateQuantity = 1;
        _currentStuffChance = _stuffChance;
        _currentBuffChance = _buffChance;
        _currentDebuffChance = _debuffChance;
        _currentInventoryItemChance = _inventoryItemChance;
        _currentBonusButtonsChance = _bonusButtonsChance;
        _currentBonusCoinsChance = _bonusCoinsChance;
        _hasBonusEffect = false;
    }

    private void ActivateParticleSystem()
    {
        Debug.Log("Activating particle system");
        if (_hasBonusEffect == false)
        {
            return;
        }
        Debug.Log("Activated particle system");
        _particles.Play();
    }

    private void ActivateBonusEffect()
    {

    }

}
