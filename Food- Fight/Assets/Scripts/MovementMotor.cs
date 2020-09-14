using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovementMotor : MonoBehaviour
{
    [Header("Constants")]
    //public float mass;
    //public float floorDrag;
    public float maxSpeed = 8f;
    public float acceleration = 30f;
    public float decclaration = 40f;
    public float speedThreshold = 0.3f;

    public Animator animator;

    [Header("Components")]
    public Rigidbody2D rb;

    private Vector2 dir = new Vector2(0, 0);
    //private Vector2 movment = new Vector2(0, 0);
    private Vector2 velocity = new Vector2(0, 0);


    public void Update()
    {

        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", Mathf.Abs(dir.x) + Mathf.Abs(dir.y));

        if (dir.x == 0) { 
        
        }
        else if(dir.x < 0) {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        } else {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }


    }


    public float getNewVelocity(float direction, float velocityVal, float deltaTime)
    {
        float newVelocity = velocityVal;
        if (velocityVal != direction * maxSpeed)
        {

            if (direction == 0) {
                newVelocity = velocityVal + decclaration * deltaTime * -Mathf.Sign(velocityVal);

                if (Mathf.Abs(newVelocity) <= speedThreshold)
                {
                    newVelocity = 0f;
                }

            } else {
                newVelocity = velocityVal + acceleration * deltaTime * Mathf.Sign(direction);

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
        Vector2 firstVelocity = rb.velocity;
        velocity.x = getNewVelocity(dir.x, rb.velocity.x, Time.fixedDeltaTime);
        velocity.y = getNewVelocity(dir.y, rb.velocity.y, Time.fixedDeltaTime);
        //rb.velocity = velocity;


        Vector2 forces = new Vector2((velocity.x - firstVelocity.x) / Time.fixedDeltaTime, (velocity.y - firstVelocity.y) / Time.fixedDeltaTime);
        
        rb.AddForce(forces);
        //Debug.Log("Velocity: " + rb.velocity + ", Forces: " + forces);
    }

    /*
    private float getNewVelocity(float direction, float velocityValue)
    {
        float desiredVel = direction * maxSpeed;
        if (velocityValue != desiredVel)
        {
            if (Mathf.Abs(velocityValue - desiredVel) < deltaVelX)
            {
                velocityValue = desiredVel;
            }
            else
            {
                if (desiredVel != 0)
                {
                    velocityValue += deltaVelX * Mathf.Sign(direction);
                }
                else
                {
                    velocityValue -= deltaVelX * Mathf.Sign(velocityValue);
                }
            }
        }
        return velocityValue;
    }
    main :::
         velocity.x = getNewVelocity(dir.x, velocity.x);
        velocity.y = getNewVelocity(dir.y, velocity.y);
        rb.velocity = velocity;
    */
}