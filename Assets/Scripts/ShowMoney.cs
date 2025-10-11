using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMoney : MonoBehaviour
{
    public Text moneyText;
    public Text UPgrade1;
    public Text UPgrade2;
    public Text UPgrade3;

    void Update()
    {
        moneyText.text = "" + GameManager.Instance.money.ToString();
        if(PlayerPrefs.GetInt("FuelLv") < 10)
        {
            UPgrade1.text = "���: " + GameManager.Instance.fuelUpgrade[PlayerPrefs.GetInt("FuelLv")].cost.ToString();
        }
        else
        {
            UPgrade1.text = "�ִ� Lv";
        }
        if(PlayerPrefs.GetInt("ConsumpLv") < 10)
        {
            UPgrade2.text = "���: " + GameManager.Instance.consumptionUpgrade[PlayerPrefs.GetInt("ConsumpLv")].cost.ToString();
        }
        else
        {
            UPgrade2.text = "�ִ� Lv";
        }
        if(PlayerPrefs.GetInt("ProbabLv") < 10)
        {
            UPgrade3.text = "���: " + GameManager.Instance.probabilityUpgrade[PlayerPrefs.GetInt("ProbabLv")].cost.ToString();
        }
        else
        {
            UPgrade3.text = "�ִ� Lv";
        }
        
    }
}
