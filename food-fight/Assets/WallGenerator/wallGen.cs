using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallGen : MonoBehaviour
{
    //The 4 given walls to input for the generation
    public GameObject[] tops;
    public GameObject[] sides;
    public int maxCount = 3;
    int count = 0;
    bool genRooms = true;
    int roomCount = 0;
    int countChecker = 0;
    //public LayerMask room;

    // Coords for wall positions
    int[] wallTops = {3, -3, -3, 3};
    int[] wallSides = {0, 5, -5, -5};

    int[] moveRoomIndexesSide = {0, 10, 0, -10};
    int[] moveRoomIndexesUp = {6, 0, -6, 0};
    List<Vector2> roomLocations = new List<Vector2>(); // Queue format
    List<Vector2> pastWallLocations = new List<Vector2>(); // 

    private List<Vector3> os = new List<Vector3>();



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

        //diable everything at start
        // Get all objects
        // Disable render property
        

    }


    void Update()
    {
        //This disables all renderer objects
        // There is many objects that do not have renderers so we need to compensate for this
        if (countChecker == 0) {

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
            countChecker += 1;

        }


    }
}
