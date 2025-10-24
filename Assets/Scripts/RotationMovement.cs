using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RotationMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float rotationSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(
            0f,
            0f,
            transform.rotation.eulerAngles.z + Random.Range
            (
                rotationSpeed / 20f,
                rotationSpeed
            ) * Time.deltaTime
        );
    }
}
