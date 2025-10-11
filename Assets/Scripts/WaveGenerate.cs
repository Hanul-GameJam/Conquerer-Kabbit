using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveGenerate : MonoBehaviour
{
    public static WaveGenerate Instance;
    private int waveNumber;
    public float waveDelay;
    private float waveDelayCount;
    private int i;

    public int totalWaveCount, waveRate;
    public GameObject[] planets;
    public GameObject currentPlanet;

    public GameObject[] waves;

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

    void Start()
    {
        waveDelayCount = waveDelay;
    }

    void Update()
    {
        if (GameManager.Instance.stopped || GameManager.Instance.inTitle)
        {
            return;
        }

        waveDelayCount -= Time.deltaTime;

        if (waveDelayCount <= 0)
        {
            waveNumber = Random.Range(0, waves.Length);

            Instantiate(waves[waveNumber]);

            totalWaveCount++;

            if (totalWaveCount % waveRate == 0)
            {
                waveRate++;

                DiscoverNewPlanet();

                totalWaveCount = 0;
            }

            waveDelayCount = waveDelay;
        }
    }

    private void DiscoverNewPlanet()
    {
        int planetIndex = Random.Range(0, planets.Length);

        currentPlanet = Instantiate(planets[planetIndex]);

        GameManager.Instance.PlayerController.fuelConsumptionRate = 0f;
        GameManager.Instance.PlayerController.moveSpeed = 0f;
        GameManager.Instance.stopped = true;

        UIManager.instance.DiscoverNewPlanet();
    }
}
