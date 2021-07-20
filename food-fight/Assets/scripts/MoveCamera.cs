using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject player;

    public float maxSpeed = 20f;
    public float acceleration = 30f;
    public float decclaration = 40f;
    public float speedThreshold = 0.3f;
    public int stepResolution = 20;
    public float recoilDuration = 0.1f;

    private Vector2 currPos;
    private Vector2 nextPos;
    private float stepSize = 0f;
    private float step = 0;

    private Vector2 dir = new Vector2(0, 0);
    private Vector2 currentVelocity = new Vector2(0, 0);

    void Update()
    {
        if ((player.transform.position.x - transform.position.x) >= 5)
        {

            transform.Translate(transform.right * 10);
            Vector2 checker = transform.position;
            while ((transform.position.x - checker.x) >= 10) {

                //transform.setPosition GetNewVelocityForCamera(dir.x, currentVelocity.x, Time.deltaTime, true);

            }

        }
        else if ((player.transform.position.x - transform.position.x) <= -5)
        {
            transform.Translate(transform.right * -10);
        }
        else if ((player.transform.position.y - transform.position.y) >= 3) {
            transform.Translate(transform.up * 6);
        }
        else if ((player.transform.position.y - transform.position.y) <= -3)
        {
            transform.Translate(transform.up * -6);
        }
    }

    void changeCameraPosition()
    {
        currPos = transform.position;
        stepSize = 1f / stepResolution;

        
        currentVelocity.y = GetNewVelocityForCamera(dir.y, currentVelocity.y, Time.deltaTime);

        currPos += (currentVelocity * stepSize * Time.deltaTime);

        transform.position = currPos;

    }

    public float GetNewVelocityForCamera(float direction, float currentVelocity, float deltaTime, bool is_x_axis = false)
    {
        float newVelocity = currentVelocity;
        if (currentVelocity != direction * maxSpeed)
        {

            if (direction == 0)
            {
                newVelocity = currentVelocity + decclaration * deltaTime * -Mathf.Sign(currentVelocity);

                if (Mathf.Abs(newVelocity) <= speedThreshold)
                {
                    newVelocity = 0f;
                }

            }
            else
            {
                newVelocity = currentVelocity + acceleration * deltaTime * Mathf.Sign(direction);

                if (Mathf.Abs(newVelocity) > maxSpeed)
                {
                    newVelocity = Mathf.Sign(direction) * maxSpeed;
                }

            }
        }

        return newVelocity;
    }


}




