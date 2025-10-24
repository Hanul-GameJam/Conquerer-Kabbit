using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer rend;
    private Animator animator;

    public float moveSpeed;
    [SerializeField] Vector2 moveInput;

    public Transform topLeftBoundary, bottomRightBoundary;

    public GameObject explosionGameObject;
    public bool atStart, atSettle, canBeHurt;
    public static bool canControl = false;
    public float scaleSpeed, scaleMultiplier;
    public event Action OnLanded;

    private AudioSource source;
    public AudioClip hitSound, explosionSound;

    public float fuel, maxFuel, fuelConsumptionRate;

    public float rotationInterval, explosionInterval;
    private float rotationCountdown, explosionCountdown;
  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();

        if (SceneManager.GetActiveScene().name == "PlayScene")
        {
            atStart = true;
            atSettle = false;
            canBeHurt = false;
            ToggleControl(false);

            ApplyUpgrades();

            fuel = maxFuel;
        }
        else if (SceneManager.GetActiveScene().name == "GameOverScene")
        {
            canControl = true;
            fuel = 0;
        }
        
        rotationCountdown = rotationInterval;
        explosionCountdown = explosionInterval;
    }

    void Update()
    {
        if (atStart)
        {
            transform.position = new Vector3(
                transform.position.x + moveSpeed * Time.deltaTime,
                transform.position.y,
                transform.position.z
            );

            if (transform.position.x >= -6.5f)
            {
                atStart = false;
                canControl = true;
                canBeHurt = true;
                canControl = true;
            }
        }
        else if (atSettle)
        {
            StartCoroutine(ScaleDownAndReset());

            atSettle = false;
        }
        else
        {
            if (canControl)
            {
                if (fuel > 0)
                {
                    moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                    rb.velocity = moveInput * moveSpeed;

                    transform.position = new Vector3
                    (
                        Mathf.Clamp
                        (
                            transform.position.x,
                            topLeftBoundary.position.x,
                            bottomRightBoundary.position.x
                        ),
                        Mathf.Clamp
                        (
                            transform.position.y,
                            bottomRightBoundary.position.y,
                            topLeftBoundary.position.y
                        ),
                        transform.position.z
                    );

                    animator.SetFloat("Movement", moveInput.y);

                    FuelConsumption();
                }
                else
                {
                    transform.SetPositionAndRotation(
                        new Vector3(
                            SceneManager.GetActiveScene().name == "PlayScene" ? transform.position.x - 3f * Time.deltaTime : transform.position.x,
                            transform.position.y,
                            transform.position.z
                        ),
                        Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + 200f * Time.deltaTime)
                    );

                    rotationCountdown -= Time.deltaTime;

                    if (rotationCountdown <= 0f)
                    {
                        float status = animator.GetFloat("Movement");

                        switch (status)
                        {
                            case > 0:
                                animator.SetFloat("Movement", -0.05f);
                                break;
                            case < 0:
                                animator.SetFloat("Movement", 0f);
                                break;
                            case 0:
                                animator.SetFloat("Movement", 0.5f);
                                break;
                        }

                        rotationCountdown = rotationInterval;
                    }

                    explosionCountdown -= Time.deltaTime;

                    if (explosionCountdown <= 0f)
                    {
                        TriggerExplosion();

                        explosionCountdown = explosionInterval;
                    }
                }
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void OnBecameInvisible()
    {
        if (SceneManager.GetActiveScene().name == "PlayScene" && UIManager.Instance.pauseMenu.activeSelf == false && atSettle == false)
        {
            GameManager.Instance.GameOver();
        }
    }

    private void ApplyUpgrades()
    {
        maxFuel = UpgradeManager.Instance.GetCurrentFuelUpgrade().value;
        fuelConsumptionRate = UpgradeManager.Instance.GetCurrentConsumptionUpgrade().value;
    }

    public void ToggleControl(bool status)
    {
        canControl = status;
    }

    private void FuelConsumption()
    {
        fuel -= fuelConsumptionRate * Time.deltaTime;
        fuel = Mathf.Clamp(fuel, 0, maxFuel);
    }

    public void TakeDamage(float damageAmount)
    {
        if (!canControl || !canBeHurt)
        {
            return;
        }
        
        canBeHurt = false;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        UIManager.Instance.HitFuelUI();

        source.PlayOneShot(hitSound);

        fuel -= damageAmount;

        Invoke(nameof(ResetHurt), 2f);
    }
    
    private void ResetHurt()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        UIManager.Instance.RestoreFuelUI();

        canBeHurt = true;
    }

    public void TriggerExplosion()
    {
        ExplosionController explosion = explosionGameObject.GetComponent<ExplosionController>();
        explosion.PlayExplosion();

        source.PlayOneShot(explosionSound);
    }

    public void RefillFuel(float amount)
    {
        fuel += amount;
        fuel = Mathf.Clamp(fuel, 0, maxFuel);
    }

    public void EnableSettleMode()
    {
        atSettle = true;
    }

    private IEnumerator ScaleDownAndReset()
    {
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = Vector3.zero;
        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime * scaleSpeed;
            /*transform.localScale = Vector3.Lerp(startScale, targetScale, progress);*/
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(0, 0, 30),
                moveSpeed * Time.deltaTime
            );
            yield return null;
            if (transform.position.z >= 0) break;
        }

        /*rend.enabled = false;*/
        /*transform.localScale = startScale * scaleMultiplier;*/

        OnLanded?.Invoke();
    }
}
