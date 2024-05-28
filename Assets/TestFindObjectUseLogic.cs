using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFindObjectUseLogic : MonoBehaviour, IUsable
{
    public static event System.Action OnUseTestItem;

    public void Use()
    {
        OnUseTestItem?.Invoke();
    }
}
