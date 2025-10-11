using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonObj : MonoBehaviour, IPointerClickHandler
{
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // click with right button
        }
        else if(eventData.button == PointerEventData.InputButton.Left)
        {
            // click with left button
        }
    }
}
