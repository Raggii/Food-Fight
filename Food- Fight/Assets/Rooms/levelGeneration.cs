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

    private int currentDirection;
    private int previousDirection = 0;
    private int nextDirection = 0;
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

    //Returns 1 - 4 depending on what room is needed
    // taking into account which room was previous and what came before
    // Also should not give any invalid remarks as they where taken out before
    //private int roomSelection(int previousPosition, int nextPosition)
    //{
    //    if (previousPosition == 1 || previousPosition == 2)
    //    { // If last was Right needs a left door

    //        if (nextPosition == 1 || nextPosition == 2)
    //        { // Right Again
    //          //Rooms can be any of them
    //          return UnityEngine.Random.Range(0, 5);

    //        } else if(nextPosition == 3 || nextPosition == 4) 
    //        {
    //            // Error my guy
    //            //Fix by going down? -- CHECK LATER
    //            return UnityEngine.Random.Range(2, 5);

    //        } else
    //        { // Going down
    //            //Room has to have a down door
    //            return UnityEngine.Random.Range(2, 5);
    //        }

    //    }
    //    else if (previousPosition == 3 || previousPosition == 4)
    //    { // If last was left nneds right door
    //        if (nextPosition == 1 || nextPosition == 2)
    //        { 
    //          // Error my guy
    //          //Fix by going down? -- CHECK LATER
    //          return UnityEngine.Random.Range(2, 5);
    //        }
    //        else if (nextPosition == 3 || nextPosition == 4)
    //        {// Left Again
    //         //Rooms can be any of them
    //            return UnityEngine.Random.Range(0, 5);

    //        }
    //        else
    //        { // Going down
    //          //Room has to have a down door
    //            return UnityEngine.Random.Range(2, 5);
    //        }

    //    }
    //    else
    //    { // had to have moved down so needs a Up door
    //        if (nextPosition == 1 || nextPosition == 2)
    //        {
    //            // Error my guy
    //            //Fix by going down? -- CHECK LATER
    //            return UnityEngine.Random.Range(0, 5);
    //        }
    //        else if (nextPosition == 3 || nextPosition == 4)
    //        {// Left Again
    //         //Rooms can be any of them
    //            return UnityEngine.Random.Range(3, 5);

    //        }
    //        else
    //        { // Going down
    //          //Room has to have a down door and up door
    //            return UnityEngine.Random.Range(3, 5);
    //        }

    //    }



    //}



private void Move()
    {
        nextDirection = UnityEngine.Random.Range(1, 6);
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
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
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
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;
                previousDirection = currentDirection;
                currentDirection = nextDirection;
                sidewaysCounter -= 1;
            }
        }
        else if (currentDirection == 5)
        { // Move Down
            Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
            transform.position = newPos;
            previousDirection = currentDirection;
            currentDirection = nextDirection;
            downCounter += 1;
        }

        int randRoomNo = UnityEngine.Random.Range(0, 4);
        Instantiate(rooms[randRoomNo], transform.position, Quaternion.identity);
    }

    void FixedUpdate()
    {

        if (downCounter <= maxDown)
        {
            currentDirection = UnityEngine.Random.Range(1, 6);
            Move();
        }

    }

}
