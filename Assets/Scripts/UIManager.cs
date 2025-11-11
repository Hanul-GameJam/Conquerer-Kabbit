using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static readonly WaitForSeconds _waitForSeconds0_5 = new(0.5f);
    private static readonly WaitForSeconds _waitForSeconds1 = new(1f);
    private static readonly WaitForSeconds _waitForSeconds3 = new(3f);

    public static UIManager Instance;

    public Text moneyText, scoreText;

    public static bool hasStarted = false;

    public AudioClip clickSound;
    private AudioSource audioSource;

    public GameObject upgradeMenu;
    public Text currentFuelValueText, fuelUpgradeCostText;
    public Text currentConsumptionValueText, consumptionUpgradeCostText;
    public Text currentSettleValueText, settleUpgradeCostText;

    public GameObject optionMenu;

    private int pressCount = 0;
    private float lastPressTime = 0f;
    public float interval = 0.5f;

    private bool isLoading = false;

    public Sprite normalSprite, hitSprite;
    public Image fuelUI;
    public Image[] fuelGauges;

    public GameObject choiceMenu;
    public GameObject settleButton, exploreButton;
    public Text settleChanceText;

    public GameObject settleResultMenu;
    public Text settleResultText, gainedMoneyText, bestScoreText;
    public GameObject exitButton;

    public bool isPaused;
    public GameObject pauseMenu;

    public GameObject gameOverMenu;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);

            return;
        }
    }

    void Start()
    {
        audioSource = GameObject.Find("SceneAudio").GetComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            UpdateUpgradeUI();
            ShowBestScore();

            if (Input.GetKeyDown(KeyCode.F5))
            {
                float currentTime = Time.time;

                if (currentTime - lastPressTime <= interval)
                {
                    pressCount++;
                }
                else
                {
                    pressCount = 1;
                }

                lastPressTime = currentTime;

                if (pressCount >= 3)
                {
                    Debug.Log("Resetting All Data");
                    ResetAllData();

                    pressCount = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (upgradeMenu.activeSelf)
                {
                    HideUpgradeMenu();
                }
                else if (optionMenu.activeSelf)
                {
                    HideUpgradeMenu();
                }
                else
                {
                    QuitGame();
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            float fuel = FindObjectOfType<PlayerController>().fuel;
            float maxFuel = FindObjectOfType<PlayerController>().maxFuel;

            int activeGaugeCount = Mathf.FloorToInt(fuel / maxFuel * fuelGauges.Length);

            for (int i = 0; i < fuelGauges.Length; i++)
            {
                if (i < activeGaugeCount)
                {
                    fuelGauges[i].color = new Color(1f, 0f, 0f, 0.75f);
                }
                else
                {
                    fuelGauges[i].color = new Color(0.75f, 0.75f, 0.75f, 0.75f);
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!choiceMenu.activeSelf && !settleResultMenu.activeSelf)
                {
                    PauseGame();
                }
            }
            
        }
        else if (SceneManager.GetActiveScene().name == "GameOverScene")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitToTitle();
            }
        }
    }

    // Title Scene UI Methods
    public void ShowBestScore()
    {
        scoreText.text = "최고 점수: " + PlayerPrefs.GetInt("BestScore").ToString();
    }

    public void PlayClickSound()
    {
        if (!hasStarted) return;

        audioSource.PlayOneShot(clickSound);
    }

    public void StartGame()
    {
        if (isLoading) return;
        
        isLoading = true;

        StartCoroutine(PlaySoundThenLoadAsync("PlayScene", 1.5f));
    }

    public void ShowUpgradeMenu()
    {
        upgradeMenu.SetActive(true);
    }

    public void HideUpgradeMenu()
    {
        upgradeMenu.SetActive(false);
    }

    public void UpdateUpgradeUI()
    {
        int money = PlayerPrefs.GetInt("Money");
        int fuelLevel = PlayerPrefs.GetInt("FuelLevel");
        int consumptionLevel = PlayerPrefs.GetInt("ConsumptionLevel");
        int settleLevel = PlayerPrefs.GetInt("SettleLevel");

        moneyText.text = money.ToString();

        currentFuelValueText.text = "현재: " + UpgradeManager.Instance.fuelUpgrades[fuelLevel].value.ToString() + "p";
        fuelUpgradeCostText.text = UpgradeManager.Instance.fuelUpgrades[fuelLevel].cost.ToString();

        currentConsumptionValueText.text = "현재: -" + UpgradeManager.Instance.consumptionUpgrades[consumptionLevel].value.ToString() + "/s";
        consumptionUpgradeCostText.text = UpgradeManager.Instance.consumptionUpgrades[consumptionLevel].cost.ToString();

        currentSettleValueText.text = "현재: +" + UpgradeManager.Instance.settleUpgrades[settleLevel].value.ToString() + "%p";
        settleUpgradeCostText.text = UpgradeManager.Instance.settleUpgrades[settleLevel].cost.ToString();
    }

    public void UpgradeFuelLevel()
    {
        UpgradeManager.Instance.UpgradeFuel();

        UpdateUpgradeUI();
    }

    public void UpgradeConsumptionLevel()
    {
        UpgradeManager.Instance.UpgradeConsumption();

        UpdateUpgradeUI();
    }

    public void UpgradeSettleLevel()
    {
        UpgradeManager.Instance.UpgradeSettle();

        UpdateUpgradeUI();
    }

    public void ShowOptionMenu()
    {
        optionMenu.SetActive(true);
    }

    public void HideOptionMenu()
    {
        optionMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void ResetAllData()
    {
        PlayerPrefs.SetInt("Money", 0);
        PlayerPrefs.SetInt("FuelLevel", 0);
        PlayerPrefs.SetInt("ConsumptionLevel", 0);
        PlayerPrefs.SetInt("SettleLevel", 0);

        UpdateUpgradeUI();
    }

    // Play Scene UI Methods
    public void HitFuelUI()
    {
        fuelUI.sprite = hitSprite;
    }
    
    public void RestoreFuelUI()
    {
        fuelUI.sprite = normalSprite;
    }

    public void UpdateScore(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

    public void ShowChoiceUI(float chance)
    {
        StartCoroutine(DelayedChoiceUI(chance));
    }

    public void ShowSettleChance(float chance)
    {
        settleChanceText.text = "성공 확률\n" + chance.ToString() + "%";
    }

    public void ChooseSettlement()
    {
        StartCoroutine(ChoseSettle());
    }

    public void ChooseExploration()
    {
        StartCoroutine(ChoseExplore());
    }

    public void ShowSettleResult(bool success, int gainedMoney)
    {
        StartCoroutine(DelayedShowResult(success, gainedMoney));
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            Time.timeScale = 0f;
            isPaused = true;
            pauseMenu.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    public void QuitToTitle()
    {
        if (isLoading) return;

        isLoading = true;
        Time.timeScale = 1f;

        StartCoroutine(PlaySoundThenLoadAsync("TitleScene", 1.5f));
    }

    // Game Over Scene UI Methods
    public void ShowGameOverUI()
    {
        StartCoroutine(DelayedShowGameOver());
    }

    public void ShowLostResource()
    {
        scoreText.text = "잃어버린 자원: " + PlayerPrefs.GetInt("LastScore", 0).ToString();
    }

    public void Delay(float delay, Action afterDelay)
    {
        StartCoroutine(DelayCoroutine(delay, afterDelay));
    }

    private IEnumerator DelayCoroutine(float delay, Action afterDelay)
    {
        yield return new WaitForSeconds(delay);
        afterDelay?.Invoke();
    }

    private IEnumerator ChoseSettle()
    {
        choiceMenu.SetActive(false);

        yield return null;

        PlanetController.Instance.SettlePlanet();
    }

    private IEnumerator ChoseExplore()
    {
        choiceMenu.SetActive(false);

        yield return null;

        PlanetController.Instance.LeavePlanet();
    }

    private IEnumerator DelayedChoiceUI(float chance)
    {
        choiceMenu.SetActive(true);
        ShowSettleChance(chance);

        yield return _waitForSeconds0_5;

        settleButton.SetActive(true);

        yield return _waitForSeconds0_5;

        exploreButton.SetActive(true);
    }

    private IEnumerator DelayedShowResult(bool success, int gainedMoney)
    {
        settleResultMenu.SetActive(true);

        if (success)
        {
            settleResultText.text = "!정착 성공!";
        }
        else
        {
            settleResultText.text = "!정착 실패!";
        }

        yield return new WaitForSeconds(1f);

        gainedMoneyText.gameObject.SetActive(true);
        gainedMoneyText.text = "얻은 자원: " + gainedMoney.ToString();
        UpgradeManager.Instance.AddMoney(gainedMoney);

        yield return new WaitForSeconds(1f);

        if (gainedMoney > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", gainedMoney);
            bestScoreText.gameObject.SetActive(true);
        }

        yield return _waitForSeconds3;

        exitButton.SetActive(true);
    }

    private IEnumerator DelayedShowGameOver()
    {
        gameOverMenu.SetActive(true);

        yield return new WaitForSeconds(1f);

        ShowLostResource();
        scoreText.gameObject.SetActive(true);

        yield return _waitForSeconds1;

        exitButton.SetActive(true);
    }

    private IEnumerator PlaySoundThenLoadAsync(string SceneName, float delay)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName);
        asyncLoad.allowSceneActivation = false;

        audioSource.PlayOneShot(clickSound);

        yield return new WaitForSeconds(delay);

        isLoading = false;
        asyncLoad.allowSceneActivation = true;
    }
}
