using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkController : MonoBehaviour
{

    public GameObject player;
    public float minimumRotationAngle;
    public float minimumRange;
    public float rotationDelta;

    public bool shoot = false;
    public float waitTime = 0.5f;
    public GameObject projectile;

    private float lastFireTime = 0f;
    private float prevAngle = 0f;

    public void Start()
    {
        lastFireTime = Time.time;
    }

    public bool InRange()
    {

         return Mathf.Abs(Vector2.Distance(player.transform.position, transform.position)) <= minimumRange;

    }

    void FixedUpdate()
    {
        shoot = CanFire();

        if (player == null)
        {
            if (shoot)
            {
                Shoot();
            }
        } else if (InRange())
        {
            float angle = Mathf.Atan2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y) * Mathf.Rad2Deg;

            //Debug.Log(Mathf.Abs(angle - prevAngle));

            if (minimumRotationAngle <= Mathf.Abs(angle - prevAngle))
            {
                RotateObject(90 - angle);
                if (shoot)
                {
                    Shoot();
                }
            }
        }
    }

    public bool CanFire()
    {
        return Time.time > waitTime + lastFireTime;
    }

    public void Shoot()
    {
        GameObject newProj = Instantiate(projectile, transform.position + transform.right * 1, transform.rotation);
        ProjectileController projCon = newProj.GetComponent<ProjectileController>();
        if (player)
        {
            
        }
        newProj.SetActive(true);
        lastFireTime = Time.time;
    }

    public void RotateObject(float newAngle)
    {
        transform.Rotate(transform.forward, newAngle - prevAngle);
        prevAngle = newAngle;
    }
    /* 
    Problems with the lower right quarter not locking on correctly. Seems to be because of negative results from Mathf.Atan2(). No cllue how to proceed. 
    
    public float rotationDelta = 5f;
    public float addRotation = 2f;
    
    void FixedUpdate()
    {
        if (InRange())
        {
            float angle = Mathf.Atan2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y) * Mathf.Rad2Deg;
            angle = 90 - angle - prevAngle;
            //Debug.Log(Mathf.Abs(angle - prevAngle));
            Debug.Log("Angle : " + angle + ", PrevAngle : " + prevAngle);
            if (minimumRotationAngle <= Mathf.Abs(angle + transform.rotation.eulerAngles.z - prevAngle))
            {
                RotateObject(angle);
            }
        }
    }

    public void RotateObject(float newAngle)
    {
        float finalAngle = newAngle + prevAngle;
        float rotateBy = 0f;

        if ( Mathf.Abs(finalAngle - prevAngle) <= rotationDelta)
        {
            rotateBy = newAngle * Mathf.Sign(finalAngle);
        } else {
            rotateBy = addRotation * Mathf.Sign(newAngle);
        }

        prevAngle = transform.rotation.eulerAngles.z;
        transform.Rotate(transform.forward, rotateBy);
    }
    */
}
