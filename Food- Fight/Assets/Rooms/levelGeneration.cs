//using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using System;


public class levelGeneration : MonoBehaviour
{

    public Transform[] startingPositions;
    public GameObject[] rooms;

    private int direction;
    private int previousDirection = 0;
    public float moveAmount;

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    public int maxSideways;
    public int maxDown;
    private int downCounter = 0;
    private int sidewaysCounter = 0;
    // Start is called before the first frame update
    void Start()
    {

        int randStartPos = UnityEngine.Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);


    }

    private void Move()
    {
        if (direction == 1 || direction == 2)
        { //Move Right
            if (previousDirection == 3 || previousDirection == 4)
            { // ILLEGAL ACTION
                direction = UnityEngine.Random.Range(1, 6);
            } else if(sidewaysCounter > maxSideways)
            { // Right == positive direction
                direction = UnityEngine.Random.Range(1, 6);
            }
            else
            {
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;
                previousDirection = direction;
                direction = UnityEngine.Random.Range(1, 6);
                sidewaysCounter += 1;
            }
        }
        else if (direction == 3 || direction == 4)
        { //Move Left
            if (previousDirection == 1 || previousDirection == 2)
            { // ILLEGAL ACTION
                direction = UnityEngine.Random.Range(1, 6);
            } else if (sidewaysCounter < -1 * maxSideways)
            { // Left == Negitive direction
                direction = UnityEngine.Random.Range(1, 6);
            }
            else
            {
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;
                previousDirection = direction;
                direction = UnityEngine.Random.Range(1, 6);
                sidewaysCounter -= 1;
            }
        }
        else if (direction == 5)
        { // Move Down
            Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
            transform.position = newPos;
            previousDirection = direction;
            direction = UnityEngine.Random.Range(1, 6);
            downCounter += 1;
        }

        Instantiate(rooms[0], transform.position, Quaternion.identity);
    }

    void FixedUpdate()
    {
        if (downCounter <= maxDown)
        {
            direction = UnityEngine.Random.Range(1, 6);
            Move();
        }

    }

}
