using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestStuffGenerator : MonoBehaviour
{
    [SerializeField] private int _stuffChance;
    [SerializeField] private int _buffChance;
    [SerializeField] private int _debuffChance;
    [SerializeField] private int _inventoryItemChance;
    [SerializeField] private int _bonusButtonsChance;
    [SerializeField] private int _bonusCoinsChance;

    [SerializeField] private GameObject CoinsObject;
    [SerializeField] private GameObject ButtonsObject;

    private int _stuffToGenerateCount;
    private int _currentStuffChance;
    private int _currentBuffChance;
    private int _currentDebuffChance;
    private int _currentInventoryItemChance;
    private int _currentBonusButtonsChance;
    private int _currentBonusCoinsChance;

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
    }

    private void OnDisable()
    {
        Chest.OnOpenChest -= ChooseStuff;
    }

    private void ChooseStuff(ItemType usedKeyType)
    {
        ResetChestState();

        int chance = Random.Range(1, 100);
        List<StuffType> stuff = new List<StuffType>();

        if (usedKeyType == ItemType.Key_GuaranteedStuff_Safe)
        {
            _currentDebuffChance = 0;
            _currentStuffChance = 100;
        }

        if (chance < _stuffChance)
        {
            while (stuff.Count < _stuffToGenerateCount)
            {
                if (chance < _bonusCoinsChance)
                {
                    AddToStuffList(StuffType.Coins, ref stuff);
                }
                else if (chance < _bonusButtonsChance)
                {
                    AddToStuffList(StuffType.Buttons, ref stuff);
                }
                else if (chance < _buffChance)
                {
                    AddToStuffList(StuffType.Buff, ref stuff);
                }
                else if (chance < _debuffChance)
                {
                    AddToStuffList(StuffType.Debuff, ref stuff);
                }
                else if (chance < _inventoryItemChance)
                {
                    AddToStuffList(StuffType.InventoryItem, ref stuff);
                }

                chance = Random.Range(1, 100);
            }
        }

        if (stuff.Count > 0)
        {
            Generate(stuff);
        }
    }

    private void AddToStuffList(StuffType stuff, ref List<StuffType> stuffList)
    {
        if (stuffList.Contains(stuff))
        {
            return;
        }

        stuffList.Add(stuff);
    }

    private void Generate(List<StuffType> stuffToGenerate)
    {
        foreach (var stuff in stuffToGenerate)
        {
            switch (stuff)
            {
                case (StuffType.Coins):
                    CoinsObject.SetActive(true); // Рандомый объект с монетками
                    break;

                case (StuffType.Buttons):
                    ButtonsObject.SetActive(true);
                    break;

                case (StuffType.Buff):
                    break;

                case (StuffType.Debuff):
                    break;

                case (StuffType.InventoryItem):
                    break;
            }
        }
    }

    private void ResetChestState()
    {
        CoinsObject.SetActive(false);
        ButtonsObject.SetActive(false);

        _stuffToGenerateCount = 1;
        _currentStuffChance = _stuffChance;
        _currentBuffChance = _buffChance;
        _currentDebuffChance = _debuffChance;
        _currentInventoryItemChance = _inventoryItemChance;
        _currentBonusButtonsChance = _bonusButtonsChance;
        _currentBonusCoinsChance = _bonusCoinsChance;
    }
}
