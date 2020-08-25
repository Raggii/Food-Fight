//using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class levelGeneration : MonoBehaviour
{

    public Transform[] startingPositions;
    public GameObject[] rooms;

    private int direction;
    private int previousDirection = 0;
    public float moveAmount;

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;
    // Start is called before the first frame update
    void Start()
    {

        int randStartPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);


    }

    private void Move()
    {
        if (direction == 1 || direction == 2 )
        { //Move Right
            if (previousDirection == 3 || previousDirection == 4)
            { // ILLEGAL ACTION
                direction = Random.Range(1, 6);
            }
            else
            {
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;
                previousDirection = direction;
                direction = Random.Range(1, 6);
            }
        }
        else if (direction == 3 || direction == 4)
        { //Move Left
            if (previousDirection == 1 || previousDirection == 2)
            { // ILLEGAL ACTION
                direction = Random.Range(1, 6);
            }
            else
            {
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;
                previousDirection = direction;
                direction = Random.Range(1, 6);
            }
   
        }
        else if (direction == 5)
        { // Move Down
            Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
            transform.position = newPos;
            previousDirection = direction;
            direction = Random.Range(1, 6);
        }

        Instantiate(rooms[0], transform.position, Quaternion.identity);
    }

    void FixedUpdate()
    {
        direction = Random.Range(1, 6);
        if (timeBtwRoom <= 0)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }

    }

}
