using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyController : MonoBehaviour
{

    private GameObject player;
    public SpriteRenderer spriteRender;
    public Vector3 offset = new Vector3(0.0001f, 0.0001f, 0);
    public float trackingThresholdDistance = 0.00005f;
    public float playerHiddenSpeed = 2f;
    public float playerVisibileSpeed = 5f;

    private Vector3 currentDir = new Vector3(0, 0, 0);
    private RaycastHit2D playerHit;
    private float speed = 0;

    private Vector3 movementDirection = new Vector3(1, 0, 0);
    private Vector3 trackingStartLocation;

    private Vector3 lastKnownLocation;

    private void OnEnable()
    {
        trackingStartLocation = transform.position;
        lastKnownLocation = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TargetNearestPlayer();
        FacePlayer();
        UpdateSensors();
        UpdateMovement();
        UpdateSprite();
    }

    private void TargetNearestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        player = null;
        float smallestDistance = Mathf.Infinity;
        for (int i = 0; i < players.Length; i++)
        {
            float distance = Mathf.Abs(Vector3.Distance(players[i].transform.position, transform.position));
            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                player = players[i];
            }
        }
    }

    private void UpdateSprite()
    {

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

            if (playerHit && playerHit.collider.gameObject.Equals(player) ) {
                lastKnownLocation = playerHit.point;
                speed = playerVisibileSpeed;

            } else {
                trackingStartLocation = transform.position;
                speed = playerHiddenSpeed;
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
