using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimWeapon : MonoBehaviour
{
    public float offset;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 diff = mouseWorldCoords - transform.position;
        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (player.transform.position.x - mouseWorldCoords.x <= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = false;
        } else
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = true;
        }

        /*if (transform.rotation.z == 0)
        {

        }
        else if (transform.rotation.z < -90)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(transform.rotation.z < 90)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }*/

    }
}
