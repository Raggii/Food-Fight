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
        /*if (cameraInput.transform != currentCameraPos) { 
            // Then the camera moved 

        
        }*/
        if (doorsEnabled)
        {
            Vector2 topLeft = new Vector2(cameraInput.transform.position.x - 5, cameraInput.transform.position.y + 3);
            Vector2 bottomRight = new Vector2(cameraInput.transform.position.x + 5, cameraInput.transform.position.y - 3);
            Collider2D[] detection = Physics2D.OverlapAreaAll(topLeft, bottomRight, 1, -Mathf.Infinity, Mathf.Infinity);
            for (int i = 0; i < detection.Length; i++)
            {
                if (detection[i].tag == "Door")
                {
                    detection[i].enabled = false;
                }
            }
        }
        else {

            Vector2 topLeft = new Vector2(cameraInput.transform.position.x - 5, cameraInput.transform.position.y + 3);
            Vector2 bottomRight = new Vector2(cameraInput.transform.position.x + 5, cameraInput.transform.position.y - 3);
            Collider2D[] detection = Physics2D.OverlapAreaAll(topLeft, bottomRight, 1, -Mathf.Infinity, Mathf.Infinity);
            for (int i = 0; i < detection.Length; i++)
            {
                if (detection[i].tag == "Door")
                {
                    detection[i].enabled = true;
                }
            }


        }

    }
}
