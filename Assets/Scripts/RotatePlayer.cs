using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotatePlayer : MonoBehaviour
{
    
    void Update()
    {
        transform.Rotate(0, 0, 5);
    }
}
