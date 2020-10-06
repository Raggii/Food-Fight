using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonRBProjectile : MonoBehaviour
{

    public float stepResolution;
    public Vector2 velocity = new Vector2();
    public new CircleCollider2D collider;
    public float radiusDelta;
    public float effectValue;
    public bool isDamage;
    public GameObject directionObj;

    private float step = 0;
    private float stepSize = 0f;
    private bool hasHit = false;

    private Vector2 currPos = new Vector2();
    private Vector2 nextPos = new Vector2();
    private Vector2 origin = new Vector2();

    private float minDistance = float.PositiveInfinity;
    private int minDistanceIndex = 0;
    private int i = 0;


    void Start()
    {
        currPos = transform.position;
    }


    void HitEvent(Collider2D hit)
    {
        if(hit.CompareTag("Damagable") || hit.CompareTag("Player"))
        {
            HealthManager healthMangCollider = hit.GetComponent<HealthManager>();
            if (isDamage)
            {
                healthMangCollider.Damage(effectValue);
            }
            else
            {
                healthMangCollider.Heal(effectValue);
            }
        }

        Destroy(this.gameObject);
    }


    void MinDistanceHit(Collider2D[] hits)
    {
        minDistance = float.PositiveInfinity;
        minDistanceIndex = 0;

        for (i=0; i< hits.Length; i++)
        {

            if (hits[i] != collider)
            {

                origin = new Vector2(transform.position.x, transform.position.y);
                if(Vector2.Distance(hits[i].ClosestPoint(transform.position), origin) < minDistance)
                {
                    minDistanceIndex = i;
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, directionObj.transform.position);
    }

    void Update()
    {
        stepSize = 1f / stepResolution;
        for (step=0; step<1f; step += stepSize)
        {
            if (!hasHit)
            {
                nextPos = currPos + velocity * stepSize * Time.deltaTime;
                Collider2D[] results = Physics2D.OverlapCircleAll(currPos, collider.radius);

                if (results.Length > 1)
                {
                    hasHit = true;
                    MinDistanceHit(results);
                    HitEvent(results[minDistanceIndex]);
                }
                /*   
                if (!hasHit)
                {      
                    RaycastHit2D hit = Physics2D.Raycast(currPos, (currPos-nextPos).normalized, stepSize);  
                    if (hit.collider != null)
                    {
                            hasHit = true;
                            HitEvent(hit);
                        
                    }
                }
               */
                currPos = nextPos;
                transform.position = currPos;
            }
        }
    }
}
