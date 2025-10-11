using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;
    public int moveType;
    public bool moveUp;
    public bool Yseting;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.stopped)
        {
            return;
        }
        if (moveType == 0) // ���������� �̵�
        {
            transform.position = new Vector3(
                    transform.position.x - moveSpeed * Time.deltaTime,
                    transform.position.y,
                    transform.position.z);
        }
        if (moveType == 1) // ���Ʒ��� ƨ��鼭 �̵�
        {
            if (moveUp)
            {
                transform.eulerAngles = new Vector3(0, 0, -45);
                transform.position = new Vector3(
                    transform.position.x - moveSpeed * Time.deltaTime,
                    transform.position.y + moveSpeed * Time.deltaTime,
                    transform.position.z);
                if (transform.position.y > 3.7f)
                {
                    moveUp = false;
                }
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 45);
                transform.position = new Vector3(
                    transform.position.x - moveSpeed * Time.deltaTime,
                    transform.position.y - moveSpeed * Time.deltaTime,
                    transform.position.z);
                if (transform.position.y < -3.7f)
                {
                    moveUp = true;
                }
            }
        }
        if (moveType == 2) // ������ �Դٰ� ������ ����
        {
            transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
            if (transform.position.x < 6)
            {
                transform.Translate(new Vector3(-moveSpeed * 8 * Time.deltaTime, 0, 0));
            }
        }
        if (moveType == 3) // ������� �̵�
        {
            transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
            if (transform.position.x < 7 && transform.position.x > 5)
            {
                transform.Translate(new Vector3(0, moveSpeed * 1.2f * Time.deltaTime, 0));
            }
            if (transform.position.x < 0 && transform.position.x > -2)
            {
                transform.Translate(new Vector3(0, -moveSpeed * 1.2f * Time.deltaTime, 0));
            }
        }
        if (moveType == 4)
        {
            transform.position = new Vector3(
                    transform.position.x - moveSpeed * Time.deltaTime,
                    transform.position.y,
                    transform.position.z);

        }
        if (moveType == 5)
        {
            transform.Translate(new Vector3(-moveSpeed * Time.deltaTime,
                Random.Range(-7, 7) * Time.deltaTime, 0));
        }
        if (moveType == 6)
        {
            transform.position = new Vector3(
                    transform.position.x - moveSpeed * Time.deltaTime,
                    transform.position.y,
                    transform.position.z);
            if (transform.position.x < 10.5f)
            {
                moveSpeed -= 0.02f;
            }
        }
        if (moveType == 7)
        {
            if (transform.position.x < 0)
            {
                if (transform.position.y < 1)
                {
                    transform.eulerAngles = new Vector3(0, 0, -30);
                    transform.position = new Vector3(
                    transform.position.x - moveSpeed * Time.deltaTime,
                    transform.position.y + moveSpeed * 0.5f * Time.deltaTime,
                    transform.position.z);

                }
                if (transform.position.y > 1)
                {
                    transform.eulerAngles = new Vector3(0, 0, 30);
                    transform.position = new Vector3(
                    transform.position.x - moveSpeed * Time.deltaTime,
                    transform.position.y - moveSpeed * 0.5f * Time.deltaTime,
                    transform.position.z);

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
        }
        if (moveType == 8)
        {
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
        }
        if (moveType == 9)
        {
            transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
            if (transform.position.x < 6) moveType = 10;
        }
        if (moveType == 10)
        {
            transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
            if (transform.position.x > 12) moveType = 11;
        }
        if (moveType == 11)
        {
            moveSpeed = 50;
            if (Yseting)
            {
                transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
                if (transform.position.x < -12)
                {
                    moveType = 12; Yseting = false;
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x, Random.Range(-4, 4), transform.position.z);
                Yseting = true;
            }
            
        }
        if (moveType == 12)
        {
            if (Yseting)
            {
                transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
                if (transform.position.x > 12)
                {
                    moveType = 13;
                    Yseting = false;
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x, Random.Range(-4, 4), transform.position.z);
                Yseting = true;
            }
        }

        if (moveType == 13)
        {
            if (Yseting)
            {
                transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
                if (transform.position.x < -12)
                {
                    moveType = 14;
                    Yseting = false;
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x, Random.Range(-4, 4), transform.position.z);
                Yseting = true;
            }
        }
        if (moveType == 14)
        {
            if (Yseting)
            {
                transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
                if (transform.position.x > 12)
                {
                    
                    moveType = 15;
                    Yseting = false;
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x, Random.Range(-4, 4), transform.position.z);
                Yseting = true;
            }
        }
        if (moveType == 15)
        {
            if (Yseting)
            {
                transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
                if (transform.position.x < -12)
                {
                    moveType = 16;
                }
                    
            }
            else
            {
                transform.position = new Vector3(transform.position.x, Random.Range(-4, 4), transform.position.z);
                Yseting = true;
            }

        }
        if (moveType == 16)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        if (moveType < 9)
        {
            Destroy(gameObject);
        }
        
    } 
}
