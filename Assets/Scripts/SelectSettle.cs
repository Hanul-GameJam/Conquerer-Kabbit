using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectSettle : UIButtonObj
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // click with left button
            Debug.Log("settle planet");
            
            if (Random.Range(0, 100) < (GameManager.Instance.settleProbablilty * 100 + GameManager.Instance.probabilityUpgrade[PlayerPrefs.GetInt("ProbabLv")].value))
            {
                Debug.Log("success");

                GameManager.Instance.settleResult = true;

                GameManager.Instance.money += GameManager.Instance.maxDistance * 2;
                PlayerPrefs.SetInt("Money", GameManager.Instance.money);

                GameManager.Instance.MoveSceneWithString("SettleScene");
            }
            else
            {
                Debug.Log("Failed");

                GameManager.Instance.settleResult = false;

                GameManager.Instance.money += GameManager.Instance.maxDistance;
                PlayerPrefs.SetInt("Money", GameManager.Instance.money);
                
                GameManager.Instance.MoveSceneWithString("SettleScene");
            }        
        }
    }
}
