//using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using System;
using System.Collections.Specialized;
using System.ComponentModel;

public class levelGeneration : MonoBehaviour
{

    public Transform[] startingPositions;
    public GameObject[] rooms;

    private int currentDirection = 1;
    private int previousDirection = 0;
    private float moveAmountSideways = 10;
    private float moveAmountDown = 6;

    public float startTimeBtwRoom = 0.25f;

    public int maxSideways;
    public int maxDown;
    private int downCounter = 0;
    private int sidewaysCounter = 0;

    private int roomNumber = 0;

    // Start is called before the first frame update
    void Start()
    {

        int randStartPos = UnityEngine.Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartPos].position;
        Instantiate(rooms[1], transform.position, Quaternion.identity);
    }

    private int betterMove()
    { // change current direction as well
        if (previousDirection == 1 || previousDirection == 2)
        { //Going Right so cannot go left
          //has to check for left right max movement
          //has to have left door
            if (sidewaysCounter >= maxSideways - 1)
            {
                roomNumber = 1; // Down if unable to move 
                                // 1 is room left down.
                currentDirection = 5;
            }
            else
            {
                // random direction of down or Right
                roomNumber = UnityEngine.Random.Range(0, 2);
                // all rooms that need left and are valid 
                // Room Left Right and Room Left Down
                if (roomNumber == 0)
                { // Right
                    currentDirection = 1;
                }
                else if (roomNumber == 1)
                { // down
                    currentDirection = 5;
                }
            }
            // Do stuff here
            Vector2 newPos = new Vector2(transform.position.x + moveAmountSideways, transform.position.y);
            transform.position = newPos;
            sidewaysCounter += 1;
            Instantiate(rooms[roomNumber], transform.position, Quaternion.identity);
            previousDirection = currentDirection;

        }
        else if (previousDirection == 3 || previousDirection == 4)
        { //Going left cannot go right
          //also check for max side to side movement
          // has to have right door
            if (sidewaysCounter <= -1 * maxSideways + 1)
            {
                roomNumber = 4; // Down if unable to move 
                                // 4 - room Right down
                currentDirection = 5;
            }
            else
            {
                // random direction of down or Left
                roomNumber = UnityEngine.Random.Range(0, 2);
                if (roomNumber == 1)
                {
                    roomNumber = 4;
                    // Down
                    currentDirection = 5;
                }
                else
                {
                    //Left
                    currentDirection = 3;
                }
                // 0 == room left Right
            }
            // Do stuff here
            Vector2 newPos = new Vector2(transform.position.x - moveAmountSideways, transform.position.y);
            transform.position = newPos;
            sidewaysCounter -= 1;
            Instantiate(rooms[roomNumber], transform.position, Quaternion.identity);
            previousDirection = currentDirection;

        }
        else
        { // Going down so needs an up door
            roomNumber = UnityEngine.Random.Range(0, 3);
            if (roomNumber == 0)
            {
                roomNumber = 2; // Left Up room
                currentDirection = 3;
            }
            else if (roomNumber == 1)
            {
                roomNumber = 3; // Right
                currentDirection = 1;
            }
            else
            {
                roomNumber = 5; // Down
                currentDirection = 5;
            }

            // Do stuff here
            Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmountDown);
            transform.position = newPos;
            downCounter += 1;
            Instantiate(rooms[roomNumber], transform.position, Quaternion.identity);
            previousDirection = currentDirection;
        }
        //currentDirection = UnityEngine.Random.Range(1, 6);

        return 0;
    }

    void FixedUpdate()
    {

        if (downCounter <= maxDown)
        {
            //currentDirection = UnityEngine.Random.Range(1, 6);
            betterMove();
        }

    }

}




