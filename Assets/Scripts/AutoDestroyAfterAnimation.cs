using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyAfterAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        float clipLength = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, clipLength * 1.1f);
    }
}
