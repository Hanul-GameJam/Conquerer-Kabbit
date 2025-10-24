using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public int moveType;
    public bool moveUp;

    void Update()
    {
        switch (moveType)
        {
            case 0: // Straight movement
                transform.position = new Vector3
                (
                    transform.position.x - moveSpeed * Time.deltaTime,
                    transform.position.y,
                    transform.position.z
                );

                break;
            case 1: // Zigzag movement
                if (moveUp)
                {
                    transform.eulerAngles = new Vector3(0, 0, -45);
                    transform.position = new Vector3
                    (
                        transform.position.x - moveSpeed * Time.deltaTime,
                        transform.position.y + moveSpeed * Time.deltaTime,
                        transform.position.z
                    );

                    if (transform.position.y > 3.7f)
                    {
                        moveUp = false;
                    }
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 45);
                    transform.position = new Vector3
                    (
                        transform.position.x - moveSpeed * Time.deltaTime,
                        transform.position.y - moveSpeed * Time.deltaTime,
                        transform.position.z
                    );

                    if (transform.position.y < -3.7f)
                    {
                        moveUp = true;
                    }
                }

                break;
            case 2: // Sine wave movement
                transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));

                if (transform.position.x < 6)
                {
                    transform.Translate(new Vector3(-moveSpeed * 8 * Time.deltaTime, 0, 0));
                }

                break;
            case 3: // Wave movement
                transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));

                if (transform.position.x < 7 && transform.position.x > 5)
                {
                    transform.Translate(new Vector3(0, moveSpeed * 1.2f * Time.deltaTime, 0));
                }

                if (transform.position.x < 0 && transform.position.x > -2)
                {
                    transform.Translate(new Vector3(0, -moveSpeed * 1.2f * Time.deltaTime, 0));
                }

                break;
            case 4: // Straight movement with speed change
                transform.position = new Vector3(
                    transform.position.x - moveSpeed * Time.deltaTime,
                    transform.position.y,
                    transform.position.z
                );

                break;
            case 5: // Random vertical movement
                transform.Translate(new Vector3(-moveSpeed * Time.deltaTime,
                Random.Range(-7, 7) * Time.deltaTime, 0));

                break;
            case 6: // Gradual deceleration
                transform.position = new Vector3(
                    transform.position.x - moveSpeed * Time.deltaTime,
                    transform.position.y,
                    transform.position.z
                );

                if (transform.position.x < 10.5f)
                {
                    moveSpeed -= 0.02f;
                }

                break;
            case 7: // Diagonal movement with angle adjustment
                if (transform.position.x < 0)
                {
                    if (transform.position.y < 1)
                    {
                        transform.eulerAngles = new Vector3(0, 0, -30);
                        transform.position = new Vector3
                        (
                            transform.position.x - moveSpeed * Time.deltaTime,
                            transform.position.y + moveSpeed * 0.5f * Time.deltaTime,
                            transform.position.z
                        );

                    }

                    if (transform.position.y > 1)
                    {
                        transform.eulerAngles = new Vector3(0, 0, 30);
                        transform.position = new Vector3
                        (
                            transform.position.x - moveSpeed * Time.deltaTime,
                            transform.position.y - moveSpeed * 0.5f * Time.deltaTime,
                            transform.position.z
                        );

                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        transform.Translate(new Vector3(-moveSpeed * 1.2f * Time.deltaTime, 0, 0));
                    }
                }
                else
                {
                    transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
                }

                break;
            case 8: // Vertical boundary bounce
                if (transform.position.y < -3)
                {
                    transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
                }
                else if (transform.position.y > 3)
                {
                    transform.Translate(new Vector3(0, -moveSpeed * Time.deltaTime, 0));
                }
                else
                {
                    transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
                }

                break;
            default:
                Destroy(gameObject);

                break;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
