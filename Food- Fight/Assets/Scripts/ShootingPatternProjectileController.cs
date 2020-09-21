using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPatternProjectileController : MonoBehaviour
{


    public float effectValue;
    public float timeToLive = 2f;
    public bool isDamage;
    public bool destoryOnImpact;
    public Rigidbody2D rb;

    private float upwardsVelocity;
    private float sideVelocity;
    private float pullVelocity;
    private Transform parent ;
    private bool hasValues;
    private Vector3 unitVec = new Vector3(0, 0, 1);


    public void SetValues(float upwardsVelocity, float sideVelocity, float pullVelocity, Transform parent)
    {
        this.upwardsVelocity = upwardsVelocity;
        this.sideVelocity = sideVelocity;
        this.pullVelocity = pullVelocity;
        this.parent = parent;
        this.hasValues = true;
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Damagable"))
        {

            HealthManager healthMangCollider = collision.collider.GetComponent<HealthManager>();

            if (isDamage)
            {
                healthMangCollider.Damage(effectValue);
            }
            else
            {
                healthMangCollider.Heal(effectValue);
            }
        }

        if (destoryOnImpact)
        {
            Destroy(this.gameObject);
        }

    }


    public void FixedUpdate()
    {
        if (hasValues && parent != null)
        {
            rb.velocity = (transform.position - parent.position).normalized * upwardsVelocity + Vector3.Cross((parent.transform.position - transform.position), unitVec).normalized * sideVelocity
                                        + (transform.position - parent.position).normalized * -pullVelocity;
        }
    }


}
