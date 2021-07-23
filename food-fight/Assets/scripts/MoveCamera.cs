using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject player;


    // for this we need to make it transition slowly as well as disable all objects outside it.
    void Update()
    {
        //detectionEnable();
        if ((player.transform.position.x - transform.position.x) >= 5)
        {
            transform.Translate(transform.right * 10);

            //Using current camera position we can always disable all objects outside of its bounds
            // camera size is 10x6 grid size
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
        //Vector2 topLeft = new Vector2(transform.position.x - 5, transform.position.y + 3);
        //Vector2 bottomRight = new Vector2(transform.position.x + 5, transform.position.y - 3);
        //var detection = Physics2D.OverlapAreaAll(topLeft, bottomRight, 1, -Mathf.Infinity, Mathf.Infinity);
        var gameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        Plane[] getCameraOutline = GeometryUtility.CalculateFrustumPlanes(camera); // gives veiw thrustrum so 6 thingos
        //Doesnt like this line for some reason

        for (int i = 0; i < 10; i++)
        {

            if (GeometryUtility.TestPlanesAABB(getCameraOutline, gameObjects[i].Collider2D.bounds)) {

                Debug.Log("Somethin");

            }
            //detection[i].gameObject.SetActive(true);

        }




        //Debug.Log(getCameraOutline[0]);


    }


}




