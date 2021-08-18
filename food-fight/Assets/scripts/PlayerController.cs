using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject projectile;
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

    private bool readyToShoot = false;
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


    void Update()
    {

        if (isActive)
        {

            int activeFingerIndex = GetNewActiveFingerIndex();

            if (0 <= activeFingerIndex) {
                if (readyToShoot)
                {
                    UpdateFirePos(Input.GetTouch(activeFingerIndex).position);
                    Fire();
                    readyToShoot = false;
                }
            } else if (Input.GetKey(KeyCode.Mouse0)) {
                if (readyToShoot)
                {
                    UpdateFirePos(Input.mousePosition);
                    Fire();
                    readyToShoot = false;
                }
            } else {
                readyToShoot = true;
            }

            if (account)
            {
                moneyBar.value = account.getBalance();
            }

            Recoil();
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

    private void UpdateFirePos(Vector3 pos)
    {             
        Vector3 clickLocation = Camera.main.ScreenToWorldPoint(pos);
        centerAxis.transform.LookAt(clickLocation);
    }

    public bool CanFire()
    {
        return Time.time > waitTime + lastFireTime;
    }


    private void Recoil()
    {
        float currentTime = Math.Min(Math.Max(Time.time - lastFireTime / waitTime, 0), 1);

        if (currentTime > 0.5)
        {
            centerAxis.transform.Rotate(new Vector3(0, 0, Mathf.Lerp(10, 0, currentTime)));
        } else
        {
            centerAxis.transform.Rotate(new Vector3(0, 0, Mathf.Lerp(0, 10, currentTime)));
        }
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

            GameObject newProj = Instantiate(projectile, firePos.transform.position + firePos.transform.up.normalized * 0.15f, firePos.transform.rotation);
            newProj.SetActive(true);
            Recoil();
            lastFireTime = Time.time;
        }
    }
}
