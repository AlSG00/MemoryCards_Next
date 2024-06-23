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

    [SerializeField] private GameObject[] CoinsObjects;
    [SerializeField] private GameObject[] ButtonsObjects;

    private int _stuffToGenerateQuantity;
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
        List<StuffType> stuffToGenerate = new List<StuffType>();
        // TODO: Add variable for storing bonus effect (need to make an interface)

        ResetChestState();
        SetChanceValues(usedKeyType);
        GenerateBonusEffect(ref stuffToGenerate);
        GenerateFutureStuffList(ref stuffToGenerate);
        Generate(stuffToGenerate);

        //if (stuffToGenerate.Count > 0)
        //{
        //    Generate(stuffToGenerate);
        //}
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
        if (bonusEffectChance <= _bonusEffectChance)
        {
            if (debuffChange <= _debuffChance)
            {
                stuffList.Add(StuffType.Debuff);
            }
            else
            {
                stuffList.Add(StuffType.Buff);
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
        int stuffToGenerateQuantity = Random.Range(1, 4);

        // генерить число предметов - чем больше предметов, тем ниже шанс
        // генерить шанс появления расходника
        // на оставшееся место в цикле генерим либо монеты либо пуговки
        // потом рандомно выбираем пивоты
        // потом рандомно выбираем сколько будет появляться пуговиц и монет

        if (stuffChance < _stuffChance)
        {
            while (stuffList.Count < stuffToGenerateQuantity)
            {
                chance = Random.Range(1, 100);
                if (chance < _bonusButtonsChance)
                {
                    stuffList.Add(StuffType.Buttons)
                    AddToStuffList(StuffType.Buttons, ref stuff);
                }

                chance = Random.Range(1, 100);
                if (chance < _bonusCoinsChance)
                {
                    AddToStuffList(StuffType.Coins, ref stuff);
                }


                //else if (chance < _buffChance)
                //{
                //    AddToStuffList(StuffType.Buff, ref stuff);
                //}
                //else if (chance < _debuffChance)
                //{
                //    AddToStuffList(StuffType.Debuff, ref stuff);
                //}
                else if (chance < _inventoryItemChance)
                {
                    AddToStuffList(StuffType.InventoryItem, ref stuff);
                }

                chance = Random.Range(1, 100);
            }
        }
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
        foreach (var obj in CoinsObjects)
        {
            obj.SetActive(false);
        }

        foreach (var obj in ButtonsObjects)
        {
            obj.SetActive(false);
        }

        _stuffToGenerateCount = 1;
        _currentStuffChance = _stuffChance;
        _currentBuffChance = _buffChance;
        _currentDebuffChance = _debuffChance;
        _currentInventoryItemChance = _inventoryItemChance;
        _currentBonusButtonsChance = _bonusButtonsChance;
        _currentBonusCoinsChance = _bonusCoinsChance;
    }

    private void ActivateBonusEffect()
    {

    }
}
