using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Text score;
    public Text percent;
    public GameObject selectionPage1;
    public GameObject mainCanvas;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        score.text = GameManager.Instance.movedDistance.ToString();
        percent.text = "정착 성공 확률\n" + (GameManager.Instance.settleProbablilty * 100 + GameManager.Instance.probabilityUpgrade[PlayerPrefs.GetInt("ProbabLv")].value) + " %";
    }

    public void DiscoverNewPlanet()
    {
        selectionPage1.SetActive(true);
    }
}
