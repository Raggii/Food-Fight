using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPatternGenerator : MonoBehaviour
{
    // public variables
    [Header("General")]
    public int numberOfProjectiles = 4;
    public float firingStarRadius = 5f;
    public float spawnAngleOffset = 0f;
    public float inBetweenShotsDelay;
    public float shootsTimeDelta;
    public int numberBulletsLimit;

    [Header("Switches")]
    public bool objectRotatesIndepedentlyAlongZ = false;
    public bool dynamicOffset = false;
    public bool reverseOrderFiring = false;
    public bool loopFiring = true;
    public bool limitBullets = false;

    [Header("Projectile Data")]
    public GameObject projectile;
    public float timeToLive = 1f;
    
    [Header("Forces and Velocities (temporary)")]
    public float upwardsVelocity;
    public float sideVelocity;
    public float pullVelcoity;

    // Private variables
    private float angleChange = 0f;
    private float nextShotTime = 0f;
    private float[] prevGeneralConstants = new float[3] {0, 0, 0};
    private int i = 0;
    private bool readyToFire = true;
    private List<Vector3> spawnPoints = new List<Vector3>();
    private List<Vector3> reversedSpawnPoints = new List<Vector3>();


    void FixedUpdate()
    {
        if (loopFiring)
        {
            Shoot();
        }
    }


    public void Shoot()
    {
        if (readyToFire && Time.time >= nextShotTime)
        {
            StartCoroutine(BuildAmmunitions());
        }
    }

    private Vector3 GetRotatedPointByAngle(Vector3 point, float angle)
    {
        float radAngle = angle * Mathf.Deg2Rad;

        return new Vector3()
        {
            x = point.x * Mathf.Cos(radAngle) - point.y * Mathf.Sin(radAngle),    
            y = point.x * Mathf.Sin(radAngle) + point.y * Mathf.Cos(radAngle),
            z = 0
        };
    }


    private void CalculateOriginSpawnPoints()
    {
        spawnPoints.Clear();
        angleChange = 360 / numberOfProjectiles;

        for (i=0; i<numberOfProjectiles; i++)
        {
            float tempAngle = angleChange * i + spawnAngleOffset;
            Vector3 startingPoint = transform.up * firingStarRadius;
            Vector3 tempPoint = GetRotatedPointByAngle(startingPoint, tempAngle);
            spawnPoints.Add(tempPoint);
        }



        prevGeneralConstants[0] = numberOfProjectiles;
        prevGeneralConstants[1] = spawnAngleOffset;
        prevGeneralConstants[2] = firingStarRadius;
    }


    private bool HasGeneralConstantsChanged()
    {
        return prevGeneralConstants[0] != numberOfProjectiles || prevGeneralConstants[1] != spawnAngleOffset || prevGeneralConstants[2] != firingStarRadius;
    }


    private void FireBullet(int i)
    {
        float bulletAngle = angleChange * i + spawnAngleOffset;
        Quaternion rot = Quaternion.Euler(0, 0, bulletAngle);

        Vector3 bulletPoint = transform.position;
        if (reverseOrderFiring)
        {
            bulletPoint += reversedSpawnPoints[i];
        }
        else
        {
            bulletPoint += spawnPoints[i];
        }

        GameObject newProj = Instantiate(projectile, bulletPoint, rot);
        //Rigidbody2D newProjRB = newProj.GetComponent<Rigidbody2D>();
        

        newProj.GetComponent<ProjectileController>().SetValues(
            upwardsVelocity, sideVelocity, pullVelcoity, this.transform);

        // Using this to visualise the fire rate... 
        Debug.DrawLine(newProj.transform.position, transform.position);
        newProj.SetActive(true);
        Destroy(newProj, timeToLive);
        
    }

    public int GetNumberOfBullets()
    {
        if (limitBullets)
        {
            if(numberBulletsLimit < 1 || numberOfProjectiles < 1)
            {
                return 1;
            } else
            {
                return Mathf.Min(numberOfProjectiles, numberBulletsLimit+1);
            }
        }
        else
        {
            return numberOfProjectiles;
        }
    }

    private IEnumerator BuildAmmunitions()
    {
        readyToFire = false;

        if (HasGeneralConstantsChanged() || objectRotatesIndepedentlyAlongZ)
        {
            CalculateOriginSpawnPoints();
            reversedSpawnPoints = new List<Vector3>(spawnPoints);
            reversedSpawnPoints.Reverse();
        }

        for (i=0; i<GetNumberOfBullets(); i++)
        {
            FireBullet(i);
            if (inBetweenShotsDelay>0) {
                yield return new WaitForSeconds(inBetweenShotsDelay);
            }
        }

        readyToFire = true;
        nextShotTime = Time.time + shootsTimeDelta;

        if (dynamicOffset)
        {
            spawnAngleOffset += spawnAngleOffset;
            spawnAngleOffset = spawnAngleOffset % 360;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        angleChange = 360 / numberOfProjectiles;
        Vector3 startingPoint = transform.up * firingStarRadius;

        Gizmos.DrawWireSphere(transform.position, firingStarRadius);

        for (int i=0; i < numberOfProjectiles; i++)
        {
            float angle = angleChange * i + spawnAngleOffset;
            Vector3 point = GetRotatedPointByAngle(startingPoint, angle);
            Gizmos.DrawSphere(transform.position + point, 0.1f);
        }


    }

}
