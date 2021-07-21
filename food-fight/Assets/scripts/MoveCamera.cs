using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject player;



    // for this we need to make it transition slowly as well as disable all objects outside it.
    void Update()
    {
        if ((player.transform.position.x - transform.position.x) >= 5)
        {
            transform.Translate(transform.right * 10);

            //Using current camera position we can always disable all objects outside of its bounds
            // camera size is 10x6 grid size



        }
        else if ((player.transform.position.x - transform.position.x) <= -5)
        {
            transform.Translate(transform.right * -10);
        }
        else if ((player.transform.position.y - transform.position.y) >= 3) {
            transform.Translate(transform.up * 6);
        }
        else if ((player.transform.position.y - transform.position.y) <= -3)
        {
            transform.Translate(transform.up * -6);
        }
    }

    /*    void detectionDisable() 
        {
            Vector2 topLeft = new Vector2(transform.position.x - 5, transform.position.y + 3);
            Vector2 bottomRight = new Vector2(transform.position.x + 5, transform.position.y - 3);
            Collider2D[] detection = OverlapAreaAll(topLeft, bottomRight,  layerMask, -Mathf.Infinity, Mathf.Infinity);

            for (int i = 0; i < detection.Count; i++) {

                //Need to enable all objects here
                // So somehow after making the wall we have to disable everything else

                //SetActive() -- useful tag   tr.gameObject.SetActive(true);
                //https://answers.unity.com/questions/532746/finding-gameobjects-within-a-radius.html
            }


        }*/


}




