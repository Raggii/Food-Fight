using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;

    private Vector2 dir;
    private Vector2 movment;


    public void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
    }


    void FixedUpdate()
    {

        movment.x = Time.fixedDeltaTime * dir.x * speed;
        movment.y = Time.fixedDeltaTime * dir.y * speed;

        rb.MovePosition(rb.position + movment);
    }
}