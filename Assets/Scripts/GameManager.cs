using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform waveSpawnpoint, planetSpawnpoint;
    public GameObject[] waves;
    public int totalWaveCount, waveRate;
    public float waveInterval;
    private float countdown;
    public bool canSpawnNextWave;
    [SerializeField] int waveIndex;

    private PlayerController player;
    public GameObject planetPrefab;

    private float timer;
    public int currentScore;

    void Awake()
    {
        Instance = this;
    }

    IEnumerator Start()
    {
        player = FindObjectOfType<PlayerController>();
        
        countdown = waveInterval;

        currentScore = 0;
        timer = 0f;
        
        yield return new WaitUntil(() => UIManager.Instance != null);
        UIManager.Instance.UpdateScore(currentScore);
    }

    void Update()
    {
        timer += Time.deltaTime;
        CalculateDistance();

        UIManager.Instance.UpdateScore(currentScore);

        if (canSpawnNextWave)
        {
            countdown -= Time.deltaTime;

            if (countdown <= 0)
            {
                waveIndex = UnityEngine.Random.Range(0, waves.Length);

                Instantiate(waves[waveIndex], waveSpawnpoint.position, waveSpawnpoint.rotation);

                totalWaveCount++;

                if (totalWaveCount % waveRate == 0)
                {
                    ToggleWaveSpawning(false);

                    Delay(5f, () =>
                    {
                        PlanetController.Instance.Discovered();
                    });

                    StartCoroutine(WaitForPlanet());
                }

                countdown = waveInterval;
            }
        }
    }

    public void HurtPlayer(float dealtDamage)
    {
        player.TakeDamage(dealtDamage);

        player.TriggerExplosion();
    }

    public void CalculateDistance()
    {
        if (!PlayerController.canControl) return;

        currentScore = (int)Mathf.Round(timer);
    }
    
    public void ToggleWaveSpawning(bool status)
    {
        canSpawnNextWave = status;
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("LastScore", currentScore);

        SceneManager.LoadScene("GameOverScene");
    }

    private IEnumerator WaitForPlanet()
    {
        yield return new WaitUntil(() => PlanetController.Instance.canProgress);

        totalWaveCount = 0;
        countdown = waveInterval;
        ToggleWaveSpawning(true);
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
}
