using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMovement : MonoBehaviour
{
    public GameObject upperTitle, lowerTitle;

    void Start()
    {
        
    }

    void Update()
    {
        if (upperTitle.transform.position.x < -4f)
        {
            upperTitle.transform.Translate(new Vector3(0.5f * Time.deltaTime, 0f, 0f));
        }

        if (lowerTitle.transform.position.x > -3f)
        {
            lowerTitle.transform.Translate(new Vector3(-0.5f * Time.deltaTime, 0f, 0f));
        }
    }
}
