using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectProbablilty : UIButtonObj
{
    public int cost;

    private void Awake()
    {
        cost = GameManager.Instance.probabilityUpgrade[PlayerPrefs.GetInt("ProbabLv")].cost;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if(PlayerPrefs.GetInt("ProbabLv") >= 10)
            {
                return;
            }
            // click with left button
            if (GameManager.Instance.money >= cost)
            {
                Debug.Log("increase probablilty ");
                GameManager.Instance.money -= cost;
                PlayerPrefs.SetInt("money", GameManager.Instance.money);
                PlayerPrefs.SetInt("ProbabLv", (PlayerPrefs.GetInt("ProbabLv") + 1));
                cost = GameManager.Instance.probabilityUpgrade[PlayerPrefs.GetInt("ProbabLv")].cost;
                GameManager.Instance.settleProbablilty = GameManager.Instance.probabilityUpgrade[PlayerPrefs.GetInt("ProbabLv")].value;
                PlayerPrefs.SetFloat("Probab", GameManager.Instance.settleProbablilty);
            }
            else
            {
                Debug.Log("no money");
            }
        }      
    }
}
