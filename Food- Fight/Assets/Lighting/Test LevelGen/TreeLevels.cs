using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLevels : MonoBehaviour
{
    [Header("Inputs")]
    public GameObject[] rooms;
    public Transform startPosition;
    public int maxHeight;
    public int maxBranchLength;
    private int roomMovementUp = 6;
    private int roomMovementLeft = 10;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {

        // Inital 4x4 Room at given starting location
        // instantiate that
        Instantiate(rooms[0], transform.position, Quaternion.identity);

    }

    //Should also return room numbers that have a change at going upwards
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
        int currentLeftAmout = UnityEngine.Random.Range(1, maxBranchLength);
        // Right By random by max
        int currentRightAmount = UnityEngine.Random.Range(1, maxBranchLength);

        //Then change position by left by amount
        Vector2 sideWayMove = new Vector2(transform.position.x - roomMovementLeft * currentLeftAmout, transform.position.y);
        transform.position = sideWayMove;

        if (currentLeftAmout + currentRightAmount <= 2) 
        {
            currentRightAmount += 1;
        }

        for (int i = 0; i < currentLeftAmout + currentRightAmount; i++) {

            Instantiate(rooms[0], transform.position, Quaternion.identity);

            Vector2 slightChange = new Vector2(transform.position.x + roomMovementLeft, transform.position.y);
            transform.position = slightChange;

        }
        transform.position = changePos;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter < maxHeight) 
        {
            treeGeneration(true);
            counter += 1;
        }
        /*if (counter < 2* maxHeight && counter > maxHeight)
        {
            if (counter == maxHeight) 
            {
                Vector2 starting = new Vector2(startPosition.position.x, startPosition.position.y);
                transform.position = starting;
            }
            treeGeneration(false);
            counter += 1;
        }*/
    }
}
