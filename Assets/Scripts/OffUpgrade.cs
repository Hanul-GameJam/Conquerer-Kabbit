using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OffUpgrade : UIButtonObj
{

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            FindObjectOfType<OnUpgrade>().isUpgrade = false;
        }    
    }
}
