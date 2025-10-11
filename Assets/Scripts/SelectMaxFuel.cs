using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectMaxFuel : UIButtonObj
{
    public int cost;
    private void Awake()
    {
        cost = GameManager.Instance.fuelUpgrade[PlayerPrefs.GetInt("FuelLv")].cost;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(PlayerPrefs.GetInt("FuelLv") >= 10)
            {
                return;
            }
            // click with left button
            if (GameManager.Instance.money >= cost)
            {
                Debug.Log("increase max fuel");
                GameManager.Instance.money -= cost;
                PlayerPrefs.SetInt("money", GameManager.Instance.money);
                PlayerPrefs.SetInt("FuelLv", PlayerPrefs.GetInt("FuelLv") + 1);
                cost = GameManager.Instance.fuelUpgrade[PlayerPrefs.GetInt("FuelLv")].cost;
                GameManager.Instance.PlayerController.maxFuel = GameManager.Instance.fuelUpgrade[PlayerPrefs.GetInt("FuelLv")].value;
            }
            else
            {
                Debug.Log("no money");
            }
        } 
    }
}
