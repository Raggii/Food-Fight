using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject[] projectile;
    public GameObject firePos;
    public GameObject centerAxis;
    public AudioSource gunshot;
    public MovementMotor motor;
    public float recoilVelocity;
    public float projectileVelocity;
    public float rateOfFire = 10; // proj per second. Default ak47 rate of fire.
    public bool isSemi;
    public bool playShotSFX = false;

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

        if (isSemi)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && isSemi)
            {
                Fire();
            }
        } else {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Fire();
            }
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


    private void PlaySound()
    {
        AudioSource Shot = Instantiate(gunshot, transform.position, transform.rotation);
        Shot.gameObject.SetActive(true);
        Destroy(Shot, Shot.clip.length);
    }

    private void Fire()
    {
        if (CanFire())
        {
            if (playShotSFX)
            {
                PlaySound(); 
            }

            int project = UnityEngine.Random.Range(0, projectile.Length);
            GameObject newProj = Instantiate(projectile[project], firePos.transform.position, firePos.transform.rotation);
            newProj.SetActive(true);
            if (newProj != null)
            {
                ProjectileController PController = newProj.GetComponent<ProjectileController>();
                if (PController != null)
                {
                    PController.SetValues(firePos.transform.up * projectileVelocity);
                }                
            }
            

            lastFireTime = Time.time;
        }
    }
}
