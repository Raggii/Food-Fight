using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject player;
    public float transitionDelay = 3;

    private float startTime = 0;
    private Vector3 target;
    private bool transitioning = false;

    private Renderer colliderToDisable;
    bool firstTimeCount = false;

    private void Start()
    {
    }

    // for this we need to make it transition slowly as well as disable all objects outside it.
    void FixedUpdate()
    {
        //Loading sceen for first time
        if (!firstTimeCount) {
            firstTimeCount = true;
            target = transform.position;
        }

        UpdateCamPos();

        //Checking players current room position


        if (transitioning) return;

        if ((player.transform.position.x - transform.position.x) >= 5)
        {
            target = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);


        }
        else if ((player.transform.position.x - transform.position.x) <= -5)
        {
            target = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
        }
        else if ((player.transform.position.y - transform.position.y) >= 3)
        {

            target = new Vector3(transform.position.x, transform.position.y + 6, transform.position.z);
        }
        else if ((player.transform.position.y - transform.position.y) <= -3)
        {
            target = new Vector3(transform.position.x, transform.position.y - 6, transform.position.z);
        }
        
    }



    private void UpdateCamPos()
    {
        if ((transform.position - target).magnitude > 0.05f)
        {
            transitioning = true;
            transform.position = Vector3.Lerp(transform.position, target, Mathf.Min((Time.time - startTime), transitionDelay) / transitionDelay);

        } else {
            startTime = Time.time;
            transform.position = target;
            transitioning = false;
        }
    }

}




