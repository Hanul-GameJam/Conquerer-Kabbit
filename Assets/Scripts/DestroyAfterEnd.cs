using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterEnd : MonoBehaviour
{
    public float destroyTime;
    private float destroyCounter;

    void Start()
    {
        destroyCounter = destroyTime;
    }

    void Update()
    {
        destroyCounter -= Time.deltaTime;

        if (destroyCounter <= 0)
        {
            Destroy(gameObject);
        }
    }
}
