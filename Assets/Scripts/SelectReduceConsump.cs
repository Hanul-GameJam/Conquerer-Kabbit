using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectReduceConsump : UIButtonObj
{
    public int cost;

    private void Awake()
    {
        cost = GameManager.Instance.consumptionUpgrade[PlayerPrefs.GetInt("ConsumpLv")].cost;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if(PlayerPrefs.GetInt("ConsumpLv") >= 10)
            {
                return;
            }
            // click with left button
            if (GameManager.Instance.money >= cost)
            {
                Debug.Log("reduce fuelConsumptionRate ");
                GameManager.Instance.money -= cost;
                PlayerPrefs.SetInt("money", GameManager.Instance.money);
                PlayerPrefs.SetInt("ConsumpLv", PlayerPrefs.GetInt("ConsumpLv") + 1);
                cost = GameManager.Instance.consumptionUpgrade[PlayerPrefs.GetInt("ConsumpLv")].cost;
                GameManager.Instance.PlayerController.fuelConsumptionRate = GameManager.Instance.consumptionUpgrade[PlayerPrefs.GetInt("ConsumpLv")].value;
            }
            else
            {
                Debug.Log("no money");
            }
        }      
    }
}
