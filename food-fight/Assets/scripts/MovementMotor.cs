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
    public float recoilDuration = 0.1f;

    [Header("Components")]
    public Animator animator;

    private Vector2 dir = new Vector2(0, 0);            // direction
    private Vector2 currentVelocity = new Vector2(0, 0);
    private Vector2 previousVelocity = new Vector2(0, 0);
    private Vector2 recoil = new Vector2(0, 0);

    private Vector2 currPos;
    private Vector2 nextPos;

    private float currentPause = 0f;
    private float stepSize = 0f;
    private float step = 0;
    private float recoilTimeLeft;

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

    public void Push(Vector2 pushVector)
    {
        recoil = pushVector;
    }

    public float GetNewVelocity(float direction, float currentVelocity, float deltaTime, float recoil_val, bool is_x_axis=false)
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

        
/*
 *      Recoil sends you into the void at Mach 18. Triggers float precision warning...
 *      Figure out a way to decrease the recoil overtime, as the max value hard cap of
 *      the speed slows you down immediatly and doesn't work...
 *      Good luck...
 * 
        if (Mathf.Abs(recoil_val) > 0)
        {
            newVelocity += recoil_val;
            float newRecoil = 0;
            if (is_x_axis)
            {
                if (Mathf.Abs(recoil.x) <= speedThreshold)
                {
                    newRecoil = 0f;
                } else
                {
                    newRecoil = recoil.x + decclaration * deltaTime * -Mathf.Sign(recoil.x);
                }
                recoil.x = newRecoil;
            } else {
                if (Mathf.Abs(recoil.y) <= speedThreshold)
                {
                    newRecoil = 0f;
                }
                else
                {
                    newRecoil = recoil.y + decclaration * deltaTime * -Mathf.Sign(recoil.x);
                }
                recoil.y = newRecoil;
            }
        }
*/

     return newVelocity;
    }


    void FixedUpdate()
    {
        currPos = gameObject.transform.position;
        stepSize = 1f / stepResolution;

        for (step = 0; step < 1f; step += stepSize)
        {
            currentVelocity.x = GetNewVelocity(dir.x, currentVelocity.x, Time.deltaTime, recoil.x, true);
            currentVelocity.y = GetNewVelocity(dir.y, currentVelocity.y, Time.deltaTime, recoil.y);

            currPos += (currentVelocity * stepSize * Time.deltaTime );

            transform.position = currPos;
        }        
    }

}