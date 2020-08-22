using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{


    [Header("Attributes")]
    public float speed = 2.5f;
    public float maxSpeed = 10f;
    public Rigidbody2D rb;

    private float horizontalDirection = 0;
    private float currentSpeed = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");
        float addition = Time.deltaTime * speed * horizontalDirection;
        if (currentSpeed+addition < maxSpeed) {
            transform.position += transform.right * addition;
            currentSpeed += addition;
        }
        else
        {
            currentSpeed = maxSpeed;
        }
    }
}
