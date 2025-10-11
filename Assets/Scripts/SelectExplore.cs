using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectExplore : UIButtonObj
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // click with left button
            Debug.Log("Do Explore");

            if (WaveGenerate.Instance.currentPlanet != null)
            {
                Destroy(WaveGenerate.Instance.currentPlanet);
            }

            GameManager.Instance.stopped = false;

            GameManager.Instance.PlayerController.fuel += 50;
            GameManager.Instance.maxDistance += 50;
            WaveGenerate.Instance.waveRate += 1;
            GameManager.Instance.settleProbablilty += 0.5f;
            
            UIManager.instance.selectionPage1.SetActive(false);
        }
    }
}
