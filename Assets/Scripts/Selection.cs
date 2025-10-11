using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.stopped = true;
    }

    private void OnDisable()
    {
        GameManager.Instance.stopped = false;
    }
}
