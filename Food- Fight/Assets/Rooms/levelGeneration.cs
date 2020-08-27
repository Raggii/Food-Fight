//using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using System;
using System.Collections.Specialized;

public class levelGeneration : MonoBehaviour
{

    public Transform[] startingPositions;
    public GameObject[] rooms;

    private int currentDirection = 1;
    private int previousDirection = 0;
    private int nextDirection = 0;
    private float moveAmountSideways = 17;
    private float moveAmountDown = 9;

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    public int maxSideways;
    public int maxDown;
    private int downCounter = 0;
    private int sidewaysCounter = 0;

    private int roomNumber;

    // Start is called before the first frame update
    void Start()
    {

        int randStartPos = UnityEngine.Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartPos].position;
        Instantiate(rooms[1], transform.position, Quaternion.identity);
    }

    //Returns 1 - 4 depending on what room is needed
    // taking into account which room was previous and what came before
    // Also should not give any invalid remarks as they where taken out before
    private int roomSelection(int previousPosition, int nextPosition)
    {

        if (previousPosition == 1 || previousPosition == 2)
        { // Last went right so need left Door
            if (nextPosition == 1 || nextPosition == 2)
            { //went left again
                return 1;// Needs Left Right Room
            }
            else if (nextPosition == 3 || nextPosition == 4)
            { // Went back right? is wrong so wont happen
                return 1;

            }
            else
            {
                return 0;// is going to go down so needs left down
            }
        }
        else if (previousPosition == 3 || previousPosition == 4)
        { // went left so needs right door
            if (nextPosition == 1 || nextPosition == 2)
            { //went left so over laps wont happen
                return 1;
            }
            else if (nextPosition == 3 || nextPosition == 4)
            { // Went right so needs right Left
                return 4;
            }
            else
            { // went down so needs right down
                return 3;// is going to go down so needs left down
            }
        }
        else
        {
            if (nextPosition == 1 || nextPosition == 2)
            { //went left needs down left
                return 9;
            }
            else if (nextPosition == 3 || nextPosition == 4)
            { // Went right needs down right
                return 10;
            }
            else
            { // went down so needs down up
                return 11;// is going to go down so needs left down
            }

        }



    }


    private void Move()
    {
        nextDirection = UnityEngine.Random.Range(1, 6);
        roomNumber = roomSelection(previousDirection, nextDirection);
        if (currentDirection == 1 || currentDirection == 2)
        { //Move Right
            if (previousDirection == 3 || previousDirection == 4)
            { // ILLEGAL ACTION
                currentDirection = UnityEngine.Random.Range(1, 6);
            } else if(sidewaysCounter > maxSideways)
            { // Right == positive direction
                currentDirection = UnityEngine.Random.Range(1, 6);
            }
            else
            {
                Vector2 newPos = new Vector2(transform.position.x + moveAmountSideways, transform.position.y);
                transform.position = newPos;
                previousDirection = currentDirection;
                currentDirection = nextDirection;
                sidewaysCounter += 1;
            }
        }
        else if (currentDirection == 3 || currentDirection == 4)
        { //Move Left
            if (previousDirection == 1 || previousDirection == 2)
            { // ILLEGAL ACTION
                currentDirection = UnityEngine.Random.Range(1, 6);
            } else if (sidewaysCounter < -1 * maxSideways)
            { // Left == Negitive direction
                currentDirection = UnityEngine.Random.Range(1, 6);
            }
            else
            {
                Vector2 newPos = new Vector2(transform.position.x - moveAmountSideways, transform.position.y);
                transform.position = newPos;
                previousDirection = currentDirection;
                currentDirection = nextDirection;
                sidewaysCounter -= 1;
            }
        }
        else if (currentDirection == 5)
        { // Move Down
            Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmountDown);
            transform.position = newPos;
            previousDirection = currentDirection;
            currentDirection = nextDirection;
            downCounter += 1;
        }
        Instantiate(rooms[roomNumber], transform.position, Quaternion.identity);
    }

    void FixedUpdate()
    {

        if (downCounter <= maxDown)
        {
            //currentDirection = UnityEngine.Random.Range(1, 6);
            Move();
        }

    }

}
