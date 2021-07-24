using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyController : MonoBehaviour
{

    public GameObject player;
    public Vector3 offset = new Vector3(0.0001f, 0.0001f, 0);

    private Vector3 currentDir = new Vector3(0, 0, 0);
    private readonly Vector2[] sensorsDirs = new Vector2[3] { new Vector2(1, -1).normalized, new Vector2(1, 0).normalized, new Vector2(1, 1).normalized };
    private List<RaycastHit2D> sensorHits = new List<RaycastHit2D>();
    private RaycastHit2D playerHit;

    private Vector3 movementDirection = new Vector3(1, 0, 0);

    // Update is called once per frame
    void FixedUpdate()
    {

        FacePlayer();
        UpdateSensors();
        UpdateMovement();

    }

    private void UpdateMovement()
    {
        
    }

    private void FacePlayer()
    {
        Vector3 playerDir = player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(-Vector3.forward, playerDir);
        transform.rotation = rotation * Quaternion.Euler(0, 0, 90);

    }

    private void UpdateSensors()
    {
        sensorHits.Clear();

        // Check player
        if (player)
        {
            playerHit = Physics2D.Raycast(transform.position + offset, (player.transform.position - transform.position).normalized);
        }

        // Check sensors
        float angle = Vector3.SignedAngle(sensorsDirs[1], transform.right, transform.forward);
        for (int i = 0; i < sensorsDirs.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + offset, Quaternion.AngleAxis(angle, transform.forward) * sensorsDirs[i].normalized);
            if (hit)
            {
                sensorHits.Add(hit);
            }
        }
    }

    private void OnDrawGizmos()
    {
        foreach (RaycastHit2D hitPoint in sensorHits)
        {
            Gizmos.DrawLine(transform.position, hitPoint.point);
        }
        if (playerHit)
        Gizmos.DrawLine(transform.position, playerHit.transform.position);
    }

}
