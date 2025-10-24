using System.Collections;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UIManager.Instance.ShowGameOverUI();
    }
}
