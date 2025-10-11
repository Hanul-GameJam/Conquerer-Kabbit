using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    public void ClearAllData()
    {
        Debug.Log("Clearing all data.");
        PlayerPrefs.DeleteAll();
        GameManager.Instance.money = 0;

        Application.Quit();
    }

    public void ExitGame()
    {
        Debug.Log("Quitting game.");
        Application.Quit();
    }
}
