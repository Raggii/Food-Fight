using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallGen : MonoBehaviour
{
    //The 4 given walls to input for the generation
    public GameObject[] tops;
    public GameObject[] sides;
    public GameObject trapDoor;
    public int maxCount = 3;
    int count = 0;
    bool genRooms = true;
    int roomCount = 0;
    bool isCountChecker = true;
    //public LayerMask room;

    // Coords for wall positions
    int[] wallTops = {3, -3, -3, 3};
    int[] wallSides = {0, 5, -5, -5};

    int[] moveRoomIndexesSide = {0, 10, 0, -10};
    int[] moveRoomIndexesUp = {6, 0, -6, 0};
    List<Vector2> roomLocations = new List<Vector2>(); // Queue format
    List<Vector2> pastWallLocations = new List<Vector2>(); // 

    private List<Vector3> os = new List<Vector3>();

    private Vector2 maxRoom = new Vector2(0,0); // This moves the end position to the furthest away room so that the end room can be made there


    //goes around clockwise starting from the top spawning rooms
    void generateRoom(bool generateRooms)
    {
        Vector2 newPos;
        int rand;
        Vector2 startingPosition = transform.position;
        for (int i = 0; i < 4; i++) {

            newPos = new Vector2(transform.position.x + wallSides[i], transform.position.y + wallTops[i]);
            transform.position = newPos;
            // also needs to have a collision checker here and if collides does not add it to the room and doesnt spawn anything
            //Stopping it from spawing if there is a door works nicely just need the logic
            if (pastWallLocations.IndexOf(newPos) == -1) // returns -1 if not found so room needs to go there
            {
                pastWallLocations.Add(newPos);
                if (generateRooms)
                {
                    rand = Random.Range(0, 2);
                }
                else
                {
                    rand = 1;
                }
                if (i % 2 == 0)
                {
                    Instantiate(tops[rand], transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(sides[rand], transform.position, Quaternion.identity);
                }
                if (rand == 0)
                {
                    //add door to queue
                    //as both the doors are set to the first index.
                    // need to get information from i, 0 = top, 1 = right ect
                    // These are put into a set list
                    Vector2 roomLocationNew = new Vector2(startingPosition.x + moveRoomIndexesSide[i], startingPosition.y + moveRoomIndexesUp[i]);
                    if (maxRoom.magnitude < roomLocationNew.magnitude)
                    {
                        maxRoom = roomLocationNew;
                    }
                    roomLocations.Insert(0, roomLocationNew); //
                    
                }
            }
            else {
                //Debug.Log("Collision Detected");
            
            }
        }
        roomCount++;
        // reset back position
        newPos = new Vector2(transform.position.x + wallSides[1], transform.position.y);
        transform.position = newPos;
        

    }
    void changeToNewRoom() {

        Vector2 tempHolder = roomLocations[0];// as Vector2;
        roomLocations.RemoveAt(0);
        transform.position = tempHolder;

    }

    //This creates the last room and removes any other doors into it
    //Needs to be called before update removes everything
    void endRoomGenerator()
    {
        //Checks all sides of room and finds the walls;
        //Position of transform is in center of the room
        /*for (int i = 0; i < 4; i++) {

            Vector2 positionOfCheck = new Vector2(transform.position.x + wallTops[i], transform.position.y + wallSides[i]);
            Collider2D wallFinder = Physics2D.OverlapCircle(positionOfCheck, 1,1, -Mathf.Infinity,Mathf.Infinity); // Then we have to do this twice?
            if (wallFinder != null && wallFinder.tag == "Door")
            {
                Debug.Log("Found one");
            
            }
        }*/

        Instantiate(trapDoor, transform.position, Quaternion.identity);

    
    }



    void Start()
    {
        // first room needs to garentee at least 2 rooms in it somehow
        generateRoom(true);
        while (roomLocations.Count != 0)
        {//roomLocations.Count != 0) {



            if (count == maxCount)
            {
                genRooms = false;
            }
            changeToNewRoom();
            generateRoom(genRooms);
            count++;


        }
        //at this point all the rooms are finished loading.
        Debug.Log(transform.position);
        transform.position = maxRoom;
        //Call a function that checks for a wall that doesnt have a room behind it
        endRoomGenerator();
        //count = 0;

    }




    void Update()
    {
        //This disables all renderer objects
        // There is many objects that do not have renderers so we need to compensate for this
        if (isCountChecker) {

            GameObject[] allColliders = FindObjectsOfType<GameObject>();
            for (int i = 0; i < allColliders.Length; i++)
            {
                if (allColliders[i].GetComponent<Renderer>() != null)
                {
                    if (allColliders[i].tag != "Player")
                    {
                        Renderer renderObject = allColliders[i].GetComponent<Renderer>();
                        //Debug.Log(renderObject);
                        renderObject.enabled = false;
                    }
                }
            }
            isCountChecker = false;

        }


    }
}
