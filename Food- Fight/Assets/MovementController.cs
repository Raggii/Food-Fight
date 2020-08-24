using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed = 0.6f;

    private float horizontalDir = 0f;
    private float verticalDir = 0f;
    private Vector3 nextPos;
    private bool moved = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalDir = Input.GetAxisRaw("Horizontal");
        verticalDir = Input.GetAxisRaw("Vertical");
        nextPos = transform.position;

        if (horizontalDir != 0)
        {
            nextPos += transform.right * speed * Time.deltaTime * horizontalDir;
            moved = true;
        }

        if (verticalDir != 0)
        {
            nextPos += transform.up * speed * Time.deltaTime * verticalDir;
            moved = true;
        }

        if (moved)
        {
            transform.position = nextPos;
        }
    }
}
