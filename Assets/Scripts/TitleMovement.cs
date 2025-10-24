using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMovement : MonoBehaviour
{
    public GameObject upperTitle, lowerTitle;
    public float upperTitleMoveSpeed, lowerTitleMoveSpeed;
    public float upperTitleX, lowerTitleX;

    void Update()
    {
        if (upperTitle.transform.position.x < upperTitleX)
        {
            upperTitle.transform.position = new Vector3(
                upperTitle.transform.position.x + upperTitleMoveSpeed * Time.deltaTime,
                upperTitle.transform.position.y,
                upperTitle.transform.position.z
            );
        }

        if (lowerTitle.transform.position.x > lowerTitleX)
        {
            lowerTitle.transform.position = new Vector3(
                lowerTitle.transform.position.x - lowerTitleMoveSpeed * Time.deltaTime,
                lowerTitle.transform.position.y,
                lowerTitle.transform.position.z
            );
        }
    }
}
