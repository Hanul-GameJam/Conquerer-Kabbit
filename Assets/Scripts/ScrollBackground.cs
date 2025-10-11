using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public float scrollSpeed;
    private Renderer backgroundRenderer;

    void Start()
    {
        backgroundRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (!GameManager.Instance.stopped || GameManager.Instance.inTitle || GameManager.Instance.inEnding)
        {
            float offset = Time.time * scrollSpeed;
            backgroundRenderer.material.mainTextureOffset = new Vector2(offset, 0);
        }
    }
}
