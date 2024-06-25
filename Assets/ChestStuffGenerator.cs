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
   // [SerializeField] private StuffSpawnpoint[] _spawnpoint;

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
        int stuffToGenerateQuantity = 0;

        // генерить число предметов - чем больше предметов, тем ниже шанс
        // генерить шанс появления расходника
        // на оставшееся место в цикле генерим либо монеты либо пуговки
        // потом рандомно выбираем пивоты
        // потом рандомно выбираем сколько будет появляться пуговиц и монет

        if (stuffChance < _stuffChance)
        {
            int stuffQuantityChance = Random.Range(1, 100);
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
                    stuffList.Add(StuffType.Buttons);
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
        //            _coinsObject[stuff].SetActive(true); // Рандомый объект с монетками
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
    }



    private void ActivateBonusEffect()
    {

    }

    //[System.Serializable]
    //public class StuffSpawnpoint
    //{
    //    public Transform Point;
    //    public bool IsAvailable;
    //    public GameObject[] PossibleCoins;
    //    public GameObject[] PossibleButtons;
    //}
}
