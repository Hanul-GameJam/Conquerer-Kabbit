using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed;
    private Vector2 moveInput;

    public float maxFuel;
    public float fuel;
    public float fuelConsumptionRate;

    public bool isbump = false;

    public Transform topLeftBoundary, bottomRightBoundary;

    private Animator animator;
    public Image[] fuelGauges;

    public Image fuelUI;
    public Sprite originalSprite, changeSprite;

    public AudioClip hit;            
    private AudioSource source;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();

        source.clip = hit;

        GameManager.Instance.RoundStart();
        animator = GetComponent<Animator>();
        fuel = maxFuel;

        fuelUI = GameObject.Find("Fuel").GetComponent<Image>();
    }

    void Update()
    {
        if (GameManager.Instance.stopped)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (fuel > 0)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            animator.SetFloat("Movement", moveInput.y);
            rb.velocity = moveInput * moveSpeed;

            transform.position = new Vector3(
                Mathf.Clamp(
                    transform.position.x,
                    topLeftBoundary.position.x,
                    bottomRightBoundary.position.x
                ),
                Mathf.Clamp(
                    transform.position.y,
                    bottomRightBoundary.position.y,
                    topLeftBoundary.position.y
                ),
                transform.position.z
            );

            consumptFuel();
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + 200f * Time.deltaTime);
            transform.position = new Vector3(transform.position.x - 3f * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }

    void consumptFuel()
    {
        fuel -= Time.deltaTime * fuelConsumptionRate;
        fuel = Mathf.Clamp(fuel, 0, maxFuel);

        int activeGaugeCount = Mathf.FloorToInt(fuel / maxFuel * fuelGauges.Length);

        for (int i = 0; i < fuelGauges.Length; i++)
        {
            if (i < activeGaugeCount)
            {
                fuelGauges[i].color = new Color(1f, 0f, 0f, 0.75f);
            }
            else
            {
                fuelGauges[i].color = new Color(0.75f, 0.75f, 0.75f, 0.75f);
            }
        }

        if (fuel <= 0f)
        {
            GameManager.Instance.GameOver();
        }
    }

    private void OnBecameInvisible()
    {
        if(fuel < 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("obstacle") && !isbump)
        {
            source.Play();
            fuelUI.sprite = changeSprite;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
            Destroy(other.gameObject);
            fuel -= 10;
            isbump = true;
            Invoke("setBumpFalse", 2f);
        }
    }

    public void setBumpFalse()
    {
        isbump = false;
        fuelUI.sprite = originalSprite;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }
}
