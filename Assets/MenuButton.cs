using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entered");
    }

    private void OnMouseEnter()
    {
        Debug.Log("Entered");
    }

    private void OnMouseExit()
    {
        Debug.Log("Leaved");
    }

}
