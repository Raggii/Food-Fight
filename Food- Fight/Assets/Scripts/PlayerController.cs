using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject[] projectile;
    public GameObject firePos;
    public GameObject centerAxis;
    public Rigidbody2D playerRB;
    public float recoilForce;
    public float projectileVelocity;
    public float rateOfFire = 10; // proj per second. Default ak47 rate of fire.
    public bool isSemi;
    
    private float lastFireTime = 0f;
    private float waitTime = 0f;

    public void Awake()
    {
        waitTime = 1 / rateOfFire;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFirePosDirection();

        if (Input.GetKeyDown(KeyCode.Mouse0) && isSemi) {
            Fire();
        }

        if (Input.GetKey(KeyCode.Mouse0) && !isSemi)
        {
            Fire();
        }
    }

    private void UpdateFirePosDirection()
    {
        Vector3 clickLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        centerAxis.transform.LookAt(clickLocation);
    }

    public bool CanFire()
    {
        return Time.time > waitTime + lastFireTime;
    }

    private void Recoil()
    {
        playerRB.velocity = new Vector2(0, 0);
        playerRB.AddForce(firePos.transform.up * -recoilForce);
    }

    private void Fire()
    {
        if (CanFire())
        {
            int project = UnityEngine.Random.Range(0, projectile.Length);
            GameObject newProj = Instantiate(projectile[project], firePos.transform.position, firePos.transform.rotation);
            newProj.SetActive(true);
            newProj.GetComponent<Rigidbody2D>().velocity = projectileVelocity * newProj.transform.up;
            Recoil();
            lastFireTime = Time.time;
        }
    }
}
