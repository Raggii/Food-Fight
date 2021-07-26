using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyController : MonoBehaviour
{

    public GameObject player;
    public Vector3 offset = new Vector3(0.0001f, 0.0001f, 0);
    public float trackingThresholdDistance = 0.00005f;
    public float speed = 10f;

    private Vector3 currentDir = new Vector3(0, 0, 0);
    private RaycastHit2D playerHit;

    private Vector3 movementDirection = new Vector3(1, 0, 0);
    private Vector3 trackingStartLocation;

    private Vector3 lastKnownLocation;

    private void OnEnable()
    {
        trackingStartLocation = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FacePlayer();
        UpdateSensors();
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if ((lastKnownLocation - transform.position).magnitude > trackingThresholdDistance) {
            transform.position = Vector3.MoveTowards(transform.position, lastKnownLocation, speed * Time.fixedDeltaTime);
        } else {
            transform.position = lastKnownLocation;
        }
    }

    private void FacePlayer()
    {
        if (player && playerHit) {
            if (playerHit.collider.gameObject.Equals(player)) {
                Vector3 playerDir = player.transform.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(-Vector3.forward, playerDir);
                transform.rotation = rotation * Quaternion.Euler(0, 0, 90);
            }
        }
    }

    private void UpdateSensors()
    {
        // Check player
        if (player) {
            playerHit = Physics2D.Raycast(transform.position + offset, (player.transform.position - transform.position).normalized);

            if (playerHit && playerHit.collider.gameObject.Equals(player)) {
                lastKnownLocation = playerHit.point;

            } else {
                trackingStartLocation = transform.position;

            }
        }
    }

    private void OnDrawGizmos()
    {

        if (playerHit)
        {
            Gizmos.DrawLine(transform.position, lastKnownLocation);
        }
    }

}
