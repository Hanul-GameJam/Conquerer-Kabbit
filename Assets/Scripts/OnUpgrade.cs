using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnUpgrade : ButtonObj
{
    public bool isUpgrade;

    public GameObject upgradeWindow;
    public GameObject upgrade01;
    public GameObject upgrade02;
    public GameObject upgrade03;
    public GameObject exit;

    public override void OnMouseDown()
    {
        base.OnMouseDown();

        isUpgrade = true;
    }
    void Update()
    {
        if (isUpgrade)
        {
            upgradeWindow.SetActive(true);
            upgrade01.SetActive(true);
            upgrade02.SetActive(true);
            upgrade03.SetActive(true);
            exit.SetActive(true);

        }
        else
        {
            upgradeWindow.SetActive(false);
            upgrade01.SetActive(false);
            upgrade02.SetActive(false);
            upgrade03.SetActive(false);
            exit.SetActive(false);

        }
    }
}
