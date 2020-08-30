using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject projectile;
    public float projectileVelocity;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) ) {
            Fire();
        }
    }


    private void Fire()
    {
        Vector3 clickLocation = Input.mousePosition;
        Vector3 direction = transform.position - clickLocation.normalized;

        GameObject newProj = Instantiate(projectile);
        newProj.transform.position = transform.position + direction;
        newProj.GetComponent<Rigidbody2D>().velocity = projectileVelocity * direction;
        

    }
}
