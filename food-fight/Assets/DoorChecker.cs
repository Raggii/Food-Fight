using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChecker : MonoBehaviour
{

    //Should only call this when camera moves
    public GameObject cameraInput;

    private Vector3 currentCameraPos;
    public bool isDoorOpen = true;
    public int countOfEnemies;
    private List<Collider2D> enemyGameObjects = new List<Collider2D>();


    private void Start()
    {
        currentCameraPos = new Vector3(cameraInput.transform.position.x, cameraInput.transform.position.y,0);
    }
    // Update is called once per frame
    void Update()
    {
        
        //Check movement in all directions then when its finished scan the room
        //The adds all objects to a list where the are kept
        if ((cameraInput.transform.position.x - currentCameraPos.x) >= 9)
        {
            enemyGameObjects = scanRoom();
            Debug.Log("Changed room");
            
        }
        else if ((cameraInput.transform.position.x - currentCameraPos.x) <= - 9)
        {
            enemyGameObjects = scanRoom();
            Debug.Log("Changed room");
        }
        else if ((cameraInput.transform.position.y - currentCameraPos.y) >= 4)
        {
            enemyGameObjects = scanRoom();
            Debug.Log("Changed room");

        }
        else if ((cameraInput.transform.position.y - currentCameraPos.y) <= - 4)
        {
            enemyGameObjects = scanRoom();
            Debug.Log("Changed room");

        }




        if (enemyGameObjects.Count == 0)
        {

            isDoorOpen = true;
        }
        else {

            isDoorOpen = false;

        }



        if (isDoorOpen)
        {
            List<Collider2D> detection = doorList();
            for (int i = 0; i < detection.Count; i++)
            {
                if (detection[i].tag == "Door")
                {
                    detection[i].isTrigger = true;
                }
            }
        }
        else {
            List<Collider2D> detection = doorList();
            for (int i = 0; i < detection.Count; i++)
            {
                if (detection[i].tag == "Door")
                {
                    detection[i].isTrigger = false;
                }
            }
        }
    }

    List<Collider2D> doorList()
    {
        List<Collider2D> returnList = new List<Collider2D>();
        //Right Side box
        Vector2 topLeftLeft = new Vector2(cameraInput.transform.position.x - 6, cameraInput.transform.position.y + .5f);
        Vector2 bottomRightLeft = new Vector2(cameraInput.transform.position.x + -5, cameraInput.transform.position.y - .5f);
        Collider2D[] detectionLeft = Physics2D.OverlapAreaAll(topLeftLeft, bottomRightLeft, 1, -Mathf.Infinity, Mathf.Infinity);

        //Left Side Box
        Vector2 topLeftRight = new Vector2(cameraInput.transform.position.x + 6, cameraInput.transform.position.y + .5f);
        Vector2 bottomRightRight = new Vector2(cameraInput.transform.position.x + 5, cameraInput.transform.position.y - .5f);
        Collider2D[] detectionRight = Physics2D.OverlapAreaAll(topLeftRight, bottomRightRight, 1, -Mathf.Infinity, Mathf.Infinity);

        //Top Box
        Vector2 topLeftTop = new Vector2(cameraInput.transform.position.x + 1, cameraInput.transform.position.y + 5);
        Vector2 bottomRightTop = new Vector2(cameraInput.transform.position.x - 1, cameraInput.transform.position.y + 2.5f);
        Collider2D[] detectionTop = Physics2D.OverlapAreaAll(topLeftTop, bottomRightTop, 1, -Mathf.Infinity, Mathf.Infinity);

        //Bottom Box
        Vector2 topLeftBottom = new Vector2(cameraInput.transform.position.x + 1, cameraInput.transform.position.y - 2.5f);
        Vector2 bottomRightBottom = new Vector2(cameraInput.transform.position.x - 1, cameraInput.transform.position.y - 5);
        Collider2D[] detectionBottom = Physics2D.OverlapAreaAll(topLeftBottom, bottomRightBottom, 1, -Mathf.Infinity, Mathf.Infinity);

        returnList.AddRange(detectionLeft);
        returnList.AddRange(detectionRight);
        returnList.AddRange(detectionTop);
        returnList.AddRange(detectionBottom);


        return returnList;

    }

    List<Collider2D> scanRoom()
    {
        List<Collider2D> objectsInRoom = new List<Collider2D>();
        Vector2 TopSide = new Vector2(cameraInput.transform.position.x - 6, cameraInput.transform.position.y + 3);
        Vector2 BottomSide = new Vector2(cameraInput.transform.position.x + 6, cameraInput.transform.position.y - 3);
        Collider2D[] detectionLeft = Physics2D.OverlapAreaAll(TopSide, BottomSide, 1, -Mathf.Infinity, Mathf.Infinity);
        objectsInRoom.AddRange(detectionLeft);
        currentCameraPos = new Vector3(cameraInput.transform.position.x, cameraInput.transform.position.y, 0);
        return objectsInRoom;
    }


    /* Collider2D[] returnList(int topLeft, int topRight, int bottomLeft, int bottomRight) {

         Collider2D[] detectionLeft = new ArrayList<Collider2D>();

         return detectionLeft;
     }*/


}
