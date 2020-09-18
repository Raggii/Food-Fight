using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimWeapon : MonoBehaviour
{
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
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
