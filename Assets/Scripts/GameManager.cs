using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerController PlayerController;
    public int money;
    public float startDistance;
    public int movedDistance;
    public int maxDistance;
    public float timer;
    public bool stopped;
    public bool settleResult;
    public bool inTitle = true;
    public bool inEnding = false;
    public float settleProbablilty;
    public int currentFuelUpgrade, currentConsumptionUpgrade, currentPercentUpgrade;

    public AudioClip explosion;
    private AudioSource source;

    public struct FuelUpgrade
    {
        public int level;
        public int cost;
        public int value;
    }
    public FuelUpgrade[] fuelUpgrade;

    public struct ConsumptionUpgrade
    {
        public int level;
        public int cost;
        public float value;
    }
    public ConsumptionUpgrade[] consumptionUpgrade;

    public struct ProbabilityUpgrade
    {
        public int level;
        public int cost;
        public float value;
    }
    public ProbabilityUpgrade[] probabilityUpgrade;

    public GameObject explosionPrefab;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (!stopped)
        {
            timer += Time.deltaTime;
            CalcDistance();
        }

        if (SceneManager.GetActiveScene().Equals("TitleScene"))
        {
            GameObject.Find("BestRecord").GetComponent<Text>().text = "최고 거리: " + PlayerPrefs.GetInt("BestRecord").ToString();
        }
    }

    public void Init()
    {
        source = GetComponent<AudioSource>();
        source.clip = explosion;
        
        if (PlayerPrefs.HasKey("Money"))
        {
            money = PlayerPrefs.GetInt("Money");
        }
        else
        {
            PlayerPrefs.SetInt("Money", 0);
            money = 0;
        }

        if (!PlayerPrefs.HasKey("FuelLv"))
        {
            PlayerPrefs.SetInt("FuelLv", 0);
        }

        if (!PlayerPrefs.HasKey("ConsumpLv"))
        {
            PlayerPrefs.SetInt("ConsumpLv", 0);
        }

        if (!PlayerPrefs.HasKey("ProbabLv"))
        {
            PlayerPrefs.SetInt("ProbabLv", 0);
        }

        if (!PlayerPrefs.HasKey("BestRecord"))
        {
            PlayerPrefs.SetInt("BestRecord", 0);
        }

        timer = 0f;
        maxDistance = 50;
        movedDistance = 0;

        fuelUpgrade = new FuelUpgrade[16];

        for (int i = 0; i < 16; i++)
        {
            fuelUpgrade[i] = new FuelUpgrade
            {
                level = i,
                cost = Mathf.RoundToInt(Mathf.Pow(2, i) * 10),
                value = 100 + i * 10
            };
        }

        consumptionUpgrade = new ConsumptionUpgrade[16];

        for (int i = 0; i < 16; i++)
        {
            consumptionUpgrade[i] = new ConsumptionUpgrade
            {
                level = i,
                cost = Mathf.RoundToInt(Mathf.Pow(2, i) * 10),
                value = 4f - i * 0.2f
            };
        }

        probabilityUpgrade = new ProbabilityUpgrade[16];

        for (int i = 0; i < 16; i++)
        {
            probabilityUpgrade[i] = new ProbabilityUpgrade
            {
                level = i,
                cost = Mathf.RoundToInt(Mathf.Pow(2, i) * 10),
                value = 10f + i * 0.5f
            };
        }
    }

    public void ClearAllData()
    {
        PlayerPrefs.DeleteAll();
        money = 0;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RoundStart()
    {
        stopped = false;
        

        if (!stopped)
        {
            startDistance = timer;
        }
    }

    public void CalcDistance()
    {
        movedDistance = (int)math.round(timer - startDistance);
    }

    public void GameOver()
    {
        source.Play();
        Debug.Log("Game Over!");
        Debug.Log(movedDistance);

        if (movedDistance > PlayerPrefs.GetInt("BestRecord"))
        {
            PlayerPrefs.SetInt("BestRecord", movedDistance);
        }

        Explosion();
        Invoke(nameof(Explosion), 1f);
        Invoke(nameof(Explosion), 1f);

        Invoke(nameof(GameOverScene), 4f);
    }

    public void MoveSceneWithString(string name)
    {
        SceneManager.LoadScene(name);
    }

    private void GameOverScene()
    {
        stopped = true;
        inEnding = true;

        MoveSceneWithString("GameOver");

        if (SceneManager.GetActiveScene().Equals("GameOver"))
        {
            GameObject.Find("LostResource").GetComponent<Text>().text = "잃어버린 자원: " + movedDistance.ToString();
        }
    }

    private void Explosion()
    {
        if (PlayerController != null)
        {
            Instantiate(explosionPrefab, PlayerController.transform.position, Quaternion.identity);
        }
    }
}
