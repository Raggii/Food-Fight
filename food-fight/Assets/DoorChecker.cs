using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChecker : MonoBehaviour
{

    //Should only call this when camera moves
    public GameObject cameraInput;

    private Transform currentCameraPos;
    public bool doorsEnabled = false;
    private int countOfEnemies;

    

    private void Start()
    {
        currentCameraPos = cameraInput.transform;

    }



    // Update is called once per frame
    void Update()
    {
        // Much more efficiant but still not useable without debug
        // Implement distance of player to camera such that when he goes into the room when he gets within a range of the camera 
        // It shuts all the doors until the emeines are dead.

        if (currentCameraPos != cameraInput.transform) 
        { 
            

        
        }



        if (doorsEnabled)
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



}
