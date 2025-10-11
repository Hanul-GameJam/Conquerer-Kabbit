using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDestoy : MonoBehaviour
{
    public float waveTimer = 10;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        waveTimer -= Time.deltaTime;

        if (waveTimer < 0 )
        {
            Destroy(gameObject);
        }
    }
}
