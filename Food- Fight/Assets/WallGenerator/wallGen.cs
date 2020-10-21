using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallGen : MonoBehaviour
{
    //The 4 given walls to input for the generation
    public GameObject[] tops;
    public GameObject[] sides;


    // need 2 coord systems
    /*
     * 1 for where the middle of the room is
     * another for where the walls go inside of the room
     * 
     */
    int wallUpAmount = 3;
    int wallSideAmount = 5;
    Queue roomLocations = new Queue();

    //goes around clockwise starting from the top spawning rooms
    void generateRoom()
    {
        Vector2 newPos = new Vector2(transform.position.x, transform.position.y + wallUpAmount);
        transform.position = newPos;
        int rand = Random.Range(0, tops.Length);
        Instantiate(tops[rand], transform.position, Quaternion.identity);

        Vector2 newPos1 = new Vector2(transform.position.x + wallSideAmount, transform.position.y - wallUpAmount);
        transform.position = newPos1;
        int rand1 = Random.Range(0, sides.Length);
        Instantiate(sides[rand1], transform.position, Quaternion.identity);

        Vector2 newPos2 = new Vector2(transform.position.x - wallSideAmount, transform.position.y - wallUpAmount);
        transform.position = newPos2;
        int rand2 = Random.Range(0, tops.Length);
        Instantiate(tops[rand2], transform.position, Quaternion.identity);

        Vector2 newPos3 = new Vector2(transform.position.x - wallSideAmount, transform.position.y + wallUpAmount);
        transform.position = newPos3;
        int rand3 = Random.Range(0, sides.Length);
        Instantiate(sides[rand3], transform.position, Quaternion.identity);

        Vector2 newPos4 = new Vector2(transform.position.x + wallSideAmount, transform.position.y);
        transform.position = newPos4;

        // then know which doors where made
        //Then enqueue a new position
        //Then change the position
        Vector2 tempHolder;// = new Vector2(transform.position.x + wallSideAmount, transform.position.y);
        if (rand == 0) {
            Debug.Log("Top");
            tempHolder = new Vector2(transform.position.x + wallSideAmount, transform.position.y);
            roomLocations.Enqueue(tempHolder);
        }
        if (rand1 == 0)
        {
            Debug.Log("Right");
        }
        if (rand2 == 0)
        {
            Debug.Log("Down");
        }
        if (rand3 == 0)
        {
            Debug.Log("Left");
        }

    }
    void changeToNewRoom() {

        //Vector2 tempHolder = roomLocations.Dequeue;

    }

    // Start is called before the first frame update
    void Start()
    {
        generateRoom();
        //changeToNewRoom();
        //generateRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
