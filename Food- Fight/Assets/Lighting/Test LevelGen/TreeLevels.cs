using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TreeLevels : MonoBehaviour
{
    [Header("Inputs")]
    public GameObject[] rooms;
    public Transform startPosition;
    public int maxHeight;
    public int maxBranchLength;
    public int amountOfUpRooms;


    [Header("InComp")]
    private int roomMovementUp = 6;
    private int roomMovementLeft = 10;
    private int counter = 0;
    private int currentRoom;
    private int nextLeft;
    private int nextRight;
    private int previousUp = 0;
    private int pastLeft = 0;

    // Start is called before the first frame update
    void Start()
    {

        // Inital 4x4 Room at given starting location
        // instantiate that
        Instantiate(rooms[10], transform.position, Quaternion.identity);
        nextLeft = UnityEngine.Random.Range(1, maxBranchLength);
        nextRight = UnityEngine.Random.Range(1, maxBranchLength);
    }

    List<int> upRoomSelect(int currentLeft, int currentRight) 
    {
        //THis gives the amount we have to start from the left side to start making up doors
        int maxUpLeft;
        //This gives amount we dont go to the right
        int maxUpRight;
        if (nextLeft >= currentLeft)
        {
            maxUpLeft = 0;
        } 
        else
        {
            maxUpLeft = currentLeft - nextLeft;
        }
        if (nextRight >= currentRight)
        {
            maxUpRight = currentRight;
        }
        else 
        {
            maxUpRight = nextRight;


        }
        List<int> upRooms = new List<int>(amountOfUpRooms + 1);
        for (int i = 0; i < amountOfUpRooms; i++) {

            int roomWithUp = UnityEngine.Random.Range(maxUpLeft, maxUpRight + 1 + maxUpLeft);

            upRooms.Add(roomWithUp);
        }
        return upRooms;

    }



    // This will handel all of the room selections depending on a whole bunch of inputs
    private int roomSelect(int currentPos, int size, int upRooms, int downDoor) 
    {

        if (upRooms == currentPos)
        {
            if (upRooms == downDoor)
            {

                //return val with up and down in same room
                // 5,7,9,11
                return 10;
            }
            else {
                //return room that doent have a down but must have an up
                // 1,2,8,10
                return 10;
            }
            
        }
        else if (currentPos == downDoor) {

            return 10;
        
        }
        else if (currentPos == 0)
        {
            return 1;
        }
        else if (currentPos == size)
        {
            return 2;
        }
        else
        {
            return 0;

        }


    }


    int downDoorChecker(int currentLeftAmout) 
    {

        return (currentLeftAmout - pastLeft) + previousUp;

    }

    //Should also return room numbers that have a chance at going upwards
    private void treeGeneration(bool upDirection) 
    {
        //Checks for direction
        int direction;
        if (upDirection) {
            direction = 1;
        } else {
            direction = -1;
        }
        //Move up by 1 Keep Original but could change
        Vector2 changePos = new Vector2(transform.position.x, (transform.position.y + roomMovementUp) * direction);
        transform.position = changePos;
        // Move Left By random up to max
        int currentLeftAmout = nextLeft;
        // Right By random by max
        int currentRightAmount = nextRight;
        //Change the next values for next time;
        nextLeft = UnityEngine.Random.Range(1, maxBranchLength);
        nextRight = UnityEngine.Random.Range(1, maxBranchLength);


        //Then change position by left by amount
        Vector2 sideWayMove = new Vector2(transform.position.x - roomMovementLeft * currentLeftAmout, transform.position.y);
        transform.position = sideWayMove;

        if (currentLeftAmout + currentRightAmount <= 2) 
        {
            currentRightAmount += 1;
        }
        List<int> upAmount = upRoomSelect(currentLeftAmout, currentRightAmount);
        int downDoor = downDoorChecker(currentLeftAmout);
        //Tuple<int, int> = boundarysForUp(currentLeftAmout, currentRightAmount)
        for (int i = 0; i < currentLeftAmout + currentRightAmount; i++) {

            currentRoom = roomSelect(i, currentLeftAmout + currentRightAmount - 1, upAmount[0], downDoor);
            previousUp = upAmount[0];

            Instantiate(rooms[currentRoom], transform.position, Quaternion.identity);

            Vector2 slightChange = new Vector2(transform.position.x + roomMovementLeft, transform.position.y);
            transform.position = slightChange;

        }
        transform.position = changePos;
        pastLeft = currentLeftAmout;
    }

    // Update is called once per frame
    void Update()
    {

        if (counter < maxHeight) 
        {
            treeGeneration(true);
            counter += 1;
        }
    }
}
