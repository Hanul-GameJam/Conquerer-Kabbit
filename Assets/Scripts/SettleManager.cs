using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettleManager : MonoBehaviour
{
    public static SettleManager Instance;
    public bool isSuccess;

    public GameObject player, planet, background;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        isSuccess = GameManager.Instance.settleResult;
        player = GameManager.Instance.PlayerController.gameObject;
        planet = WaveGenerate.Instance.currentPlanet;
    }

    void Update()
    {
        if ((player.transform.position.x != planet.transform.position.x &&
                 player.transform.position.y != planet.transform.position.y) ||
                (player.transform.localScale.x >= 0.1f &&
                 player.transform.localScale.y >= 0.1f))
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, planet.transform.position, 0.1f);
            player.transform.localScale -= new Vector3(0.01f, 0.01f, 0f);
        }

        if (player.transform.localScale.x <= 0.1f && player.transform.localScale.y <= 0.1f)
        {
            player.SetActive(false);

            if (isSuccess)
            {
                // TODO 정착지 성공 효과 추가

            }
            else
            {
                // TODO 정착지 실패 효과 추가

            }

            Invoke(nameof(ShowSettleResult), 2);
            Invoke(nameof(ShowResourceGain), 2);

            GameManager.Instance.MoveSceneWithString("TitleScene");
            GameManager.Instance.inTitle = true;
        }
    }

    private void ShowSettleResult()
    {
        if (isSuccess)
        {
            GameObject.Find("SettleResult").GetComponent<Text>().text = "정착 성공!";
        }
        else
        {
            GameObject.Find("SettleResult").GetComponent<Text>().text = "정착 실패!";
        }

        GameObject.Find("SettleResult").SetActive(true);
    }

    private void ShowResourceGain()
    {
        if (isSuccess)
        {
            GameObject.Find("GainResource").GetComponent<Text>().text = "획득한 자원: " + GameManager.Instance.maxDistance * 2;
        }
        else
        {
            GameObject.Find("GainResource").GetComponent<Text>().text = "획득한 자원: " + GameManager.Instance.maxDistance;
        }

        GameObject.Find("GainResource").SetActive(true);
    }
}
