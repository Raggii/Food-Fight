using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerShoot : MonoBehaviour
{

    public float stepResolution;
    public Vector2 velocity = new Vector2(); 
    //public Vector2 direction = new Vector2();

    private float step = 0;
    private float stepSize = 0f;
    private bool hasHit = false;
    private Vector2 currPos = new Vector2();
    private Vector2 nextPos = new Vector2();


    void Start()
    {
        currPos = transform.position;
    }

    
    void Update()
    {
        stepSize = 1f / stepResolution;
        for (step=0; step<1f; step += stepSize)
        {
            nextPos = currPos + velocity * stepSize * Time.deltaTime;
            RaycastHit2D hit = Physics2D.Raycast(currPos, (currPos-nextPos).normalized, stepSize);
            
            if (hit.collider != null)
            {
                if (!hasHit)
                {
                    Debug.Log("Hit!");
                    Destroy(hit.collider.gameObject);
                    hasHit = true;
                }
            }
            currPos = nextPos;
            transform.position = currPos;
        }
    }
}
