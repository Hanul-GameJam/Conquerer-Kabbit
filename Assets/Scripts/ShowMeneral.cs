using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMeneral : MonoBehaviour
{
    public Text meneralText;

    
    void Update()
    {
        meneralText.text = "�Ҿ���� �ڿ�:" + GameManager.Instance.movedDistance.ToString();
    }
}
