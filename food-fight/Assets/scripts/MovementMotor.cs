using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovementMotor : MonoBehaviour
{
    [Header("Constants")]
    public float speedThreshold = 0.3f;
    public int stepResolution = 20;
    public float recoilDuration = 0.1f;

    [Header("Movement")]
    public float maxSpeed = 20f;
    public float acceleration = 30f;
    public float decclaration = 40f;

    [Header("Controls")]
    public float joystickDeadzone = 0.2f;
    public float joystickOffset = 0.2f;

    [Header("Components")]
    public Animator animator;
    public Joystick joystick = null;

    private Vector2 dir = new Vector2(0, 0);            // direction
    private Vector2 currentVelocity = new Vector2(0, 0);
    private Vector2 previousVelocity = new Vector2(0, 0);
    private Vector2 recoil = new Vector2(0, 0);

    private Vector2 currPos;
    private Vector2 nextPos;

    private float stepSize = 0f;
    private float step = 0;
    private float recoilTimeLeft;
    private Vector2 prevPos;

    public void Update()
    {
        if (joystick)
        {
            // Getting direction vector and clamping the joystick constants
            Vector2 newDir = new Vector2(Input.GetAxisRaw("Horizontal") + joystick.Horizontal, Input.GetAxisRaw("Vertical") + joystick.Vertical);
            newDir.Normalize();
            joystickDeadzone = Mathf.Clamp01(joystickDeadzone);
            joystickOffset = Mathf.Clamp01(joystickOffset);
            float scale = 1 - Mathf.Max(joystickDeadzone, joystickOffset);

            // X axis controls
            if (Mathf.Abs(newDir.x) >= joystickDeadzone) {
                dir.x = newDir.x * scale;
            } else { dir.x = 0;  }

            // Y axis controls 
            if (Mathf.Abs(newDir.y) >= joystickDeadzone)
            {
                dir.y = newDir.y * scale;
            } else { dir.y = 0; }
        } else
        {
            // If we don't have a joystick we simply use the input raw axis values.
            dir.x = Input.GetAxisRaw("Horizontal");
            dir.y = Input.GetAxisRaw("Vertical");
        }        

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

    public float CalculateVelocity(float direction, float currentVelocity, float deltaTime, float recoil_val)
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

                if (Mathf.Abs(newVelocity) > direction * maxSpeed)
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
            currentVelocity.x = CalculateVelocity(dir.x, currentVelocity.x, Time.deltaTime, recoil.x);
            currentVelocity.y = CalculateVelocity(dir.y, currentVelocity.y, Time.deltaTime, recoil.y);
            
            
            currPos += (currentVelocity * stepSize * Time.deltaTime );
            //Debug.Log(currPos);
            //Debug.Log(transform.position);

            transform.position = currPos;
        }
    }


    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(transform.position, transform.position + new Vector3(dir.x, dir.y, transform.position.z));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(currPos, new Vector3(prevPos.x, prevPos.y, transform.position.z));
    }

}