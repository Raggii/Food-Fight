using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject projectile;
    public GameObject firePos;
    public GameObject centerAxis;
    public float projectileVelocity;

    // Update is called once per frame
    void Update()
    {
        UpdateFirePosDirection();

        if (Input.GetKeyDown(KeyCode.Mouse0) ) {
            Fire();
        }
    }

    private void UpdateFirePosDirection()
    {
        Vector3 clickLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        centerAxis.transform.LookAt(clickLocation);
    }

    private void Fire()
    {
        
        GameObject newProj = Instantiate(projectile, firePos.transform.position, firePos.transform.rotation);
//        newProj.transform.position = firePos.transform.position;
//        newProj.transform.rotation = new Quaternion(firePos.transform.rotation.x, firePos.transform.rotation.y, 0, firePos.transform.rotation.w);
        newProj.SetActive(true);
        newProj.GetComponent<Rigidbody2D>().velocity = projectileVelocity * newProj.transform.up;
    }
}
