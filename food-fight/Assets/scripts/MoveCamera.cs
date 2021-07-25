using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject player;

    private Renderer colliderToDisable;
    bool firstTimeCount = false;

    // for this we need to make it transition slowly as well as disable all objects outside it.
    void Update()
    {
        //Loading sceen for first time
        if (!firstTimeCount) {

            detectionEnable();
            firstTimeCount = true;
        }

        //Checking players current room position
        if ((player.transform.position.x - transform.position.x) >= 5)
        {

            //Testing Area//
            float speed = 1;
            Vector3 endMarker = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
            
            cameraMovement(endMarker, speed);


            //transform.Translate(transform.right * 10);
            detectionEnable();
        }
        else if ((player.transform.position.x - transform.position.x) <= -5)
        {
            transform.Translate(transform.right * -10);
            detectionEnable();
        }
        else if ((player.transform.position.y - transform.position.y) >= 3) {
            transform.Translate(transform.up * 6);
            detectionEnable();
        }
        else if ((player.transform.position.y - transform.position.y) <= -3)
        {
            transform.Translate(transform.up * -6);
            detectionEnable();
        }
    }

    



    void detectionEnable()
    {
        Vector2 topLeft = new Vector2(transform.position.x - 5, transform.position.y + 3);
        Vector2 bottomRight = new Vector2(transform.position.x + 5, transform.position.y - 3);
        Collider2D[] detection = Physics2D.OverlapAreaAll(topLeft, bottomRight, 1, -Mathf.Infinity, Mathf.Infinity);
        for (int i = 0; i < detection.Length; i++)
        {
            colliderToDisable = detection[i].gameObject.GetComponent<Renderer>();
            colliderToDisable.enabled = true;
        }
    }



    void cameraMovement(Vector3 target, float speed) {


        while ((target - transform.position).magnitude > 0.005f)
        {
            float speeder = (target - transform.position).magnitude / speed * Time.deltaTime;
            
            transform.position = Vector3.MoveTowards(transform.position, target, speeder);

            yield return new WaitForSeconds(1);


        }
    }


}




