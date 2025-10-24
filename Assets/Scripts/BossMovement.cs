using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BossMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed;
    public bool phase0, phase1, phase2, phase3, phase4, phase5, phase6;
    public float initialXThreshold, laterXThreshold, laterMovementSpeed, randomRange;
    public bool YSetting;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (phase0)
        {
            transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));

            if (transform.position.x < initialXThreshold)
            {
                phase0 = false;
                phase1 = true;
            }
        }
        else if (phase1)
        {
            transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));

            if (transform.position.x > laterXThreshold)
            {
                phase1 = false;
                phase2 = true;
            }
        }
        else if (phase2)
        {
            moveSpeed = laterMovementSpeed;

            if (YSetting)
            {
                transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));

                if (transform.position.x < -laterXThreshold)
                {
                    YSetting = false;

                    phase2 = false;
                    phase3 = true;
                }
            }
            else
            {
                transform.position = new Vector3
                (
                    transform.position.x,
                    Random.Range(-randomRange, randomRange),
                    transform.position.z
                );

                YSetting = true;
            }
        }
        else if (phase3)
        {
            if (YSetting)
            {
                transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));

                if (transform.position.x > laterXThreshold)
                {
                    YSetting = false;

                    phase3 = false;
                    phase4 = true;
                }
            }
            else
            {
                transform.position = new Vector3(
                    transform.position.x,
                    Random.Range(-randomRange, randomRange),
                    transform.position.z
                );

                YSetting = true;
            }
        }
        else if (phase4)
        {
            if (YSetting)
            {
                transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));

                if (transform.position.x < -laterXThreshold)
                {
                    YSetting = false;

                    phase4 = false;
                    phase5 = true;
                }
            }
            else
            {
                transform.position = new Vector3
                (
                    transform.position.x,
                    Random.Range(-randomRange, randomRange),
                    transform.position.z
                );

                YSetting = true;
            }
        }
        else if (phase5)
        {
            if (YSetting)
            {
                transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));

                if (transform.position.x > laterXThreshold)
                {
                    YSetting = false;

                    phase5 = false;
                    phase6 = true;
                }
            }
            else
            {
                transform.position = new Vector3
                (
                    transform.position.x,
                    Random.Range(-randomRange, randomRange),
                    transform.position.z
                );

                YSetting = true;
            }
        }
        else if (phase6)
        {
            if (YSetting)
            {
                transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));

                if (transform.position.x < -laterXThreshold)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                transform.position = new Vector3
                (
                    transform.position.x,
                    Random.Range(-randomRange, randomRange),
                    transform.position.z
                );

                YSetting = true;
            }
        }
    }
}
