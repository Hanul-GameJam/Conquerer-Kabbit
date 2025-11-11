using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlanetController : MonoBehaviour
{
    public static PlanetController Instance;
    private PlayerController player;

    private Animator animator;
    [SerializeField] bool isReversed;

    [SerializeField] bool choiceShown;
    public int varieties, variety;

    public float chance, baseSettleChance, additionalSettleChance;
    public float exploreFuelBonus, exploreSettleBonus;
    public int exploreScoreBonus, exploreWaveRateIncrease;
    public float settleSuccessBonusMultiplier, settleFailBonusMultiplier;

    public Transform planetSpawnpoint;
    [SerializeField] bool canMove;
    public float moveSpeed;
    public float moveTargetX;
    public float animationTargetX;
    [SerializeField] bool varietyChosen;
    public float scaleTarget, scaleSpeed;
    public bool canProgress;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;

        player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.OnLanded += OnPlayerLanded;

        canMove = false;
        choiceShown = false;
        canProgress = false;

        baseSettleChance = UpgradeManager.Instance.GetCurrentSettleUpgrade().value;
        additionalSettleChance = 0;
    }

    void Update()
    {
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

        if (isReversed && currentState.normalizedTime <= 0f)
        {
            animator.speed = 0f;
            isReversed = false;
            canMove = false;
        }

        if (!isReversed && currentState.normalizedTime >= 1f)
        {
            animator.speed = 0f;
        }

        if (!varietyChosen)
        {
            variety = UnityEngine.Random.Range(0, varieties);
            animator.SetInteger("Variety", variety);

            varietyChosen = true;
        }

        if (canMove)
        {
            transform.position = new Vector3(
                transform.position.x - moveSpeed * Time.deltaTime,
                transform.position.y,
                transform.position.z
            );

            if (transform.position.x <= animationTargetX)
            {
                animator.enabled = true;
                PlayAnimation(true);
            }

            if (transform.position.x <= moveTargetX + 0.05 &&
                transform.position.x >= moveTargetX - 0.05)
            {
                if (!choiceShown)
                {
                    canMove = false;

                    chance = baseSettleChance + additionalSettleChance;

                    UIManager.Instance.ShowChoiceUI(chance);
                }
            }
        }
    }

    private void OnBecameInvisible()
    {
        transform.position = planetSpawnpoint.position;
        varietyChosen = false;
        animator.enabled = false;
        canMove = false;
        canProgress = false;
    }

    public void Discovered()
    {
        player.ToggleControl(false);

        animator.enabled = false;

        canMove = true;
    }

    private void PlayAnimation(bool isApproaching)
    {
        if (isApproaching)
        {
            animator.speed = 0.75f;
            animator.Play("Planet_" + variety.ToString(), -1, 0f);
        }
        /*else
        {
            animator.speed = -0.75f;
            animator.Play("Planet_" + variety.ToString(), 0, 1f);
            isReversed = true;
        }*/
    }

    public void SettlePlanet()
    {
        /*StartCoroutine(ScaleUpAndStop());*/

        player.EnableSettleMode();
    }
    
    private void OnPlayerLanded()
    {
        int score = GameManager.Instance.currentScore;
        float roll = UnityEngine.Random.Range(0, 100);
        int totalGainedMoney = 0;
        int totalMoney = PlayerPrefs.GetInt("Money");

        Delay(1f, () =>
        {
            if (roll < chance)
            {
                Debug.Log("Settle Success!");

                Delay(2f, () =>
                {
                    totalGainedMoney = (int)Mathf.Round(
                        score * settleSuccessBonusMultiplier
                    );

                    UIManager.Instance.ShowSettleResult(true, totalGainedMoney);
                });
            }
            else
            {
                Debug.Log("Settle Failed!");

                player.TriggerExplosion();

                Delay(1f, () =>
                {
                    totalGainedMoney = (int)Mathf.Round(
                    score * settleFailBonusMultiplier
                );

                    UIManager.Instance.ShowSettleResult(false, totalGainedMoney);
                });
            }

            PlayerPrefs.SetInt("Money", totalMoney + totalGainedMoney);
        });
    }

    public void LeavePlanet()
    {
        player.ToggleControl(true);
        canMove = true;
        canProgress = true;

        player.RefillFuel(exploreFuelBonus);
        additionalSettleChance += exploreSettleBonus;
        GameManager.Instance.currentScore += exploreScoreBonus;
        GameManager.Instance.waveRate += exploreWaveRateIncrease;

        PlayAnimation(false);
    }

    private IEnumerator ScaleUpAndStop()
    {
        canMove = false;

        Vector3 startScale = transform.localScale;
        Vector3 targetScale = startScale * scaleTarget;
        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime * scaleSpeed;
            transform.localScale = Vector3.Lerp(startScale, targetScale, progress);
            yield return null;
        }

        transform.localScale = targetScale;
    }


    public void Delay(float delay, Action afterDelay)
    {
        StartCoroutine(DelayCoroutine(delay, afterDelay));
    }

    private IEnumerator DelayCoroutine(float delay, Action afterDelay)
    {
        yield return new WaitForSeconds(delay);
        afterDelay?.Invoke();
    }
}
