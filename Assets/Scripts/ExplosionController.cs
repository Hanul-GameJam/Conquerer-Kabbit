using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    public void PlayExplosion()
    {
        if (!player.IsDestroyed())
        {
            gameObject.SetActive(true);
            //animator.Play("Explosion", -1, 0f);
        }
    }

    public void OnExplosionEnd()
    {
        gameObject.SetActive(false);
    }
}
