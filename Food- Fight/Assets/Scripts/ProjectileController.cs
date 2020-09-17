using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    public float effectValue;
    public bool isDamage;
    public float timeToLive = 2f;

    private float creationTime = 0f;

    public void Awake()
    {
        creationTime = Time.time;
    }

    private void FixedUpdate()
    {
        if (creationTime + timeToLive <= Time.time)
        {
            Debug.Log("TTL expired. For object with ID : " + this.gameObject.GetInstanceID());
            Destroy(this.gameObject);
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Damagable") ) {

            HealthManager healthMangCollider = collision.collider.GetComponent<HealthManager>();
            
            if (isDamage)
            {
                healthMangCollider.Damage(effectValue);
            } else {
                healthMangCollider.Heal(effectValue);
            }
        }
        

        Destroy(this.gameObject);

    }

}
