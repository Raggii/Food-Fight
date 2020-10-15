using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovementMotor : MonoBehaviour
{
    [Header("Constants")]
    public float maxSpeed = 20f;
    public float acceleration = 30f;
    public float decclaration = 40f;
    public float speedThreshold = 0.3f;
    public int stepResolution = 20;


    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 dir = new Vector2(0, 0);            // direction
    private Vector2 currentVelocity = new Vector2(0, 0);
    private Vector2 previousVelocity = new Vector2(0, 0);
    private float currentPause = 0f;
    private float stepSize = 0f;
    private float step = 0;

    private Vector2 currPos;
    private Vector2 nextPos;

    public void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        AnimatorUpdate();
    }

    void AnimatorUpdate()
    {
        animator.SetFloat("Speed", Mathf.Abs(dir.x) + Mathf.Abs(dir.y));

        if (dir.x == 0)
        {

        }
        else if (dir.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void InstantStop()
    {
        currentPause = Time.time + 0.1f;
        currentVelocity = new Vector2(0, 0);
        dir = new Vector2(0, 0);
        rb.velocity = new Vector2(0, 0);
    }

    public float GetNewVelocity(float direction, float currentVelocity, float deltaTime)
    {
        float newVelocity = currentVelocity;
        if (currentVelocity != direction * maxSpeed)
        {

            if (direction == 0) {
                newVelocity = currentVelocity + decclaration * deltaTime * -Mathf.Sign(currentVelocity);

                if (Mathf.Abs(newVelocity) <= speedThreshold)
                {
                    newVelocity = 0f;
                }

            } else {
                newVelocity = currentVelocity + acceleration * deltaTime * Mathf.Sign(direction);

                if (Mathf.Abs(newVelocity) > maxSpeed)
                {
                    newVelocity = Mathf.Sign(direction) * maxSpeed;
                }

            }
        }

     return newVelocity;
    }
    

    void FixedUpdate()
    {
        currPos = gameObject.transform.position;
        stepSize = 1f / stepResolution;

        for (step = 0; step < 1f; step += stepSize)
        {
            currentVelocity.x = GetNewVelocity(dir.x, currentVelocity.x, Time.deltaTime);
            currentVelocity.y = GetNewVelocity(dir.y, currentVelocity.y, Time.deltaTime);

            currPos += (currentVelocity * stepSize *Time.deltaTime);
            transform.position = currPos;
        }        
    }

}