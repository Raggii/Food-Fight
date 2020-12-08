using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Update()
    {
        rb.velocity = transform.rotation.eulerAngles.normalized;
    }

}
