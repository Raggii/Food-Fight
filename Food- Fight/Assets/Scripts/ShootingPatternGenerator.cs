
using System.Collections.Generic;
using UnityEngine;

public class ShootingPatternGenerator : MonoBehaviour
{
    // public variables
    [Header("General")]
    public int numberOfProjectiles = 4;
    public float firingStarRadius = 5f;
    public float spawnAngleOffset = 0f;
    public float shootsTimeDelta;
    public bool continiousUpdating = false;

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
    private List<Vector3> spawnPoints = new List<Vector3>();


    void FixedUpdate()
    {
        if (Time.time >= nextShotTime)
        {
            BuildAmmunitions();
            nextShotTime = Time.time + shootsTimeDelta;
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


    private void BuildAmmunitions()
    {        
        if (HasGeneralConstantsChanged() || continiousUpdating)
        {
            CalculateOriginSpawnPoints();
        }


        for(i=0; i<spawnPoints.Count; i++)
        {
            float bulletAngle = angleChange * i + spawnAngleOffset;
            Vector3 bulletPoint = transform.position + spawnPoints[i];
            Quaternion rot = Quaternion.Euler(0, 0, bulletAngle);

            GameObject newProj = Instantiate(projectile, bulletPoint, rot);
            Rigidbody2D newProjRB = newProj.GetComponent<Rigidbody2D>();

            if (newProjRB == null)
            {
                Destroy(newProj);
            }
            else
            {
                newProj.GetComponent<ShootingPatternProjectileController>().SetValues(
                    upwardsVelocity, sideVelocity, pullVelcoity, this.transform);

                // Using this to visualise the fire rate... 
                Debug.DrawLine(newProj.transform.position, transform.position);
                newProj.SetActive(true);
                Destroy(newProj, timeToLive);
            }
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
