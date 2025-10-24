using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    public int money;

    public struct FuelUpgrade
    {
        public int level;
        public int cost;
        public int value;
    }
    public struct ConsumptionUpgrade
    {
        public int level;
        public int cost;
        public float value;
    }
    public struct SettleUpgrade
    {
        public int level;
        public int cost;
        public float value;
    }
    public FuelUpgrade[] fuelUpgrades;
    public ConsumptionUpgrade[] consumptionUpgrades;
    public SettleUpgrade[] settleUpgrades;

    public int maxFuelLevel, maxConsumptionLevel, maxSettleLevel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeUpgrades();
    }

    private void InitializeUpgrades()
    {
        money = PlayerPrefs.GetInt("Money", 0);

        if (money < 0)
        {
            money = 0;
            PlayerPrefs.SetInt("Money", 0);
        }

        if (!PlayerPrefs.HasKey("FuelLevel"))
        {
            PlayerPrefs.SetInt("FuelLevel", 0);
        }

        if (!PlayerPrefs.HasKey("ConsumptionLevel"))
        {
            PlayerPrefs.SetInt("ConsumptionLevel", 0);
        }

        if (!PlayerPrefs.HasKey("SettleLevel"))
        {
            PlayerPrefs.SetInt("SettleLevel", 0);
        }

        fuelUpgrades = new FuelUpgrade[maxFuelLevel + 1];

        for (int i = 0; i <= maxFuelLevel; i++)
        {
            fuelUpgrades[i] = new FuelUpgrade
            {
                level = i,
                cost = (int)Mathf.Pow(2, i) * 10,
                value = 100 + i * 10
            };
        }

        consumptionUpgrades = new ConsumptionUpgrade[maxConsumptionLevel + 1];

        for (int i = 0; i <= maxConsumptionLevel; i++)
        {
            consumptionUpgrades[i] = new ConsumptionUpgrade
            {
                level = i,
                cost = (int)Mathf.Pow(2, i) * 10,
                value = 4f - i * 0.2f
            };
        }

        settleUpgrades = new SettleUpgrade[maxSettleLevel + 1];

        for (int i = 0; i <= maxSettleLevel; i++)
        {
            settleUpgrades[i] = new SettleUpgrade
            {
                level = i,
                cost = (int)Mathf.Pow(2, i) * 10,
                value = 10f + i * 0.5f
            };
        }
    }

    public void SubtractMoney(int amount)
    {
        money -= amount;

        PlayerPrefs.SetInt("Money", money);
    }

    public FuelUpgrade GetCurrentFuelUpgrade()
    {
        return fuelUpgrades[PlayerPrefs.GetInt("FuelLevel")];
    }

    public void UpgradeFuel()
    {
        if (GetCurrentFuelUpgrade().level >= maxFuelLevel || money < GetCurrentFuelUpgrade().cost)
        {
            return;
        }

        SubtractMoney(GetCurrentFuelUpgrade().cost);

        PlayerPrefs.SetInt("FuelLevel", GetCurrentFuelUpgrade().level + 1);
    }

    public ConsumptionUpgrade GetCurrentConsumptionUpgrade()
    {
        return consumptionUpgrades[PlayerPrefs.GetInt("ConsumptionLevel")];
    }

    public void UpgradeConsumption()
    {
        if (GetCurrentConsumptionUpgrade().level >= maxConsumptionLevel || money < GetCurrentConsumptionUpgrade().cost)
        {
            return;
        }

        SubtractMoney(GetCurrentConsumptionUpgrade().cost);

        PlayerPrefs.SetInt("ConsumptionLevel", GetCurrentConsumptionUpgrade().level + 1);
    }

    public SettleUpgrade GetCurrentSettleUpgrade()
    {
        return settleUpgrades[PlayerPrefs.GetInt("SettleLevel")];
    }

    public void UpgradeSettle()
    {
        if (GetCurrentSettleUpgrade().level >= maxSettleLevel ||
            money < GetCurrentSettleUpgrade().cost)
        {
            return;
        }

        SubtractMoney(GetCurrentSettleUpgrade().cost);

        PlayerPrefs.SetInt("SettleLevel", GetCurrentSettleUpgrade().level + 1);
    }
}
