﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject[] projectile;
    public GameObject firePos;
    public GameObject centerAxis;
    public Slider moneyBar;
    public Joystick joystick;

    public AudioSource gunshot;
    public MovementMotor motor;
    public BankAccountManager account;


    public float recoilVelocity;
    public float projectileVelocity;
    public float rateOfFire = 10; // proj per second. Default ak47 rate of fire.

    public bool isSemi;
    public bool playShotSFX = false;

    private bool isActive = true;
    private float lastFireTime = 0f;
    private float waitTime = 0f;

    public void Awake()
    {
        waitTime = 1 / rateOfFire;
        if (account) {
            moneyBar.maxValue = account.getMaxBalance();
            }
        }

    // Update is called once per frame
    void Update()
    {

        if (!isActive)
        {
            return;
        }

        int activeFingerIndex = GetNewActiveFingerIndex();
        UpdateFirePosDirection(activeFingerIndex);


        if (0 <= activeFingerIndex)
        {
            if (isSemi) { 
                Fire();
            }
        } else
        {
            Fire();
        }

        if (account)
        {
            moneyBar.value = account.getBalance();
        }

    }

    private int GetNewActiveFingerIndex()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                return i;
            }
        }
        return -1;
    }

    private void UpdateFirePosDirection(int activeFingerIndex)
    {
        
        if (activeFingerIndex >= 0)
        {            

            Vector3 clickLocation = Camera.main.ScreenToWorldPoint(Input.GetTouch(activeFingerIndex).position);
            centerAxis.transform.LookAt(clickLocation);
        }
    }

    public bool CanFire()
    {
        return Time.time > waitTime + lastFireTime;
    }


    private void Recoil(Vector2 fireDir)
    {
        motor.Push(-fireDir * recoilVelocity);
    }


    public void DeactivatePlayerControls()
    {
        isActive = false;
        motor.enabled = false;
    }


    public void ActivatePlayerControls()
    {
        isActive = true;
        motor.enabled = true;
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

            int index = UnityEngine.Random.Range(0, projectile.Length);
            GameObject newProj = Instantiate(projectile[index], firePos.transform.position, firePos.transform.rotation);
            if (newProj != null)
            {
                ProjectileController PController = newProj.GetComponent<ProjectileController>();
                if (PController != null)
                {
                    PController.SetValues(firePos.transform.up * projectileVelocity);
                } else
                {
                    Destroy(newProj);
                }

                Recoil(firePos.transform.up);

                newProj.SetActive(true);
            }


            lastFireTime = Time.time;
        }
    }
}
