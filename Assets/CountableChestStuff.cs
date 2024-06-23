using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CountableChestStuff : MonoBehaviour
{
    [SerializeField] private GameObject[] _objects;
    [SerializeField] private protected AudioSource _audioSource;
    /*[SerializeField]*/ private protected int _quantity;

    internal int Quantity {
        get => _quantity;
        set
        {
            if (value < 0 || value > _objects.Length)
            {
                throw new System.ArgumentOutOfRangeException($"Wrong button count: {value}");
            }

            _quantity = value;
        }
    }

    private protected void OnEnable()
    {
        for (int i = 0; i < _quantity; i++)
        {
            _objects[i].SetActive(true);
        }
    }

    private protected void OnDisable()
    {
        foreach (var obj in _objects)
        {
            obj.SetActive(false);
        }
    }

    private protected abstract void OnMouseDown();
}
