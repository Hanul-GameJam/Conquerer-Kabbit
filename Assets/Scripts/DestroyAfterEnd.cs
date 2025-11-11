using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterEnd : MonoBehaviour
{
    public float destroyTime;
    private float destroyCounter;

    void Start()
    {
        GameManager.Instance.canSpawnNextWave = false;

        destroyCounter = destroyTime;
    }

    void Update()
    {
        destroyCounter -= Time.deltaTime;

        if (destroyCounter <= 0)
        {
            GameManager.Instance.canSpawnNextWave = true;
            
            Destroy(gameObject);
        }
    }
}
