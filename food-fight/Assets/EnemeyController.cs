using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyController : MonoBehaviour
{

    private GameObject player;
    public SpriteRenderer spriteRender;
    public float offset = 1f;
    public float trackingThresholdDistance = 0.00005f;
    public float playerHiddenSpeed = 2f;
    public float playerVisibileSpeed = 5f;

    private Vector3 currentDir = new Vector3(0, 0, 0);
    private RaycastHit2D playerHit;
    private float speed = 0;

    private Vector3 movementDirection = new Vector3(1, 0, 0);
    private Vector3 trackingStartLocation;

    private Vector3 lastKnownLocation;

    private bool ableToMove = true;
    private bool updateSensors = true;

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

    public float DistanceFromPlayer()
    {
        if (lastKnownLocation != transform.position) {
            return Vector3.Distance(transform.position, lastKnownLocation);
        } else
        {
            return Mathf.Infinity;
        }
    }

    public bool IsPlayerVisible()
    {
        if (playerHit) return true;
        return false;
    }

    public void EnableMovement()
    {
        this.ableToMove = true;
    }

    public void DisableMovement()
    {
        this.ableToMove = false;
    }

    public void EnableSensors()
    {
        this.updateSensors = true;
    }

    public void DisableSensors()
    {
        this.updateSensors = false;
    }

    public bool MoveTowardsLastKnownLocation()
    {
        ableToMove = true;
        return transform.position.Equals(lastKnownLocation);
    }

    private void UpdateMovement()
    {
        if (ableToMove)
        {

            if ((lastKnownLocation - transform.position).magnitude > trackingThresholdDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, lastKnownLocation, speed * Time.fixedDeltaTime);
            }
            else
            {
                transform.position = lastKnownLocation;
            }
        }
    }

    private void FacePlayer()
    {
        if (updateSensors && player && playerHit) {
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
        if (updateSensors && player) {
            playerHit = Physics2D.Raycast(transform.position + (player.transform.position - transform.position).normalized * offset, (player.transform.position - transform.position).normalized);

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
