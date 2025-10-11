using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMeneral : MonoBehaviour
{
    public Text meneralText;

    
    void Update()
    {
        meneralText.text = "잃어버린 자원:" + GameManager.Instance.movedDistance.ToString();
    }
}
