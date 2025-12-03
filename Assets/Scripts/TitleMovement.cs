using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMovement : MonoBehaviour
{
    public GameObject upperTitle, lowerTitle;
    public Transform upperTitleTarget, lowerTitleTarget;
    public float upperTitleMoveSpeed, lowerTitleMoveSpeed;

    void Update()
    {
        if (upperTitle.transform.position.x < upperTitleTarget.position.x)
        {
            upperTitle.transform.position = new Vector3(
                upperTitle.transform.position.x + upperTitleMoveSpeed * Time.deltaTime,
                upperTitle.transform.position.y,
                upperTitle.transform.position.z
            );
        }

        if (lowerTitle.transform.position.x > lowerTitleTarget.position.x)
        {
            lowerTitle.transform.position = new Vector3(
                lowerTitle.transform.position.x - lowerTitleMoveSpeed * Time.deltaTime,
                lowerTitle.transform.position.y,
                lowerTitle.transform.position.z
            );
        }
    }
}
