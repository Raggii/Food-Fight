using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    public float effectValue;
    public bool isDamage;

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
