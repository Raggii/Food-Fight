using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Settings")]
    public float velocity = 5f;
    public float decay = 0f;
    public float damage = 1f;
    public CircleCollider2D myCollider;
    public SpriteRenderer spriteRenderer;

    [Header("Skins")]
    public Sprite[] sprites;

    [Header("Bullet Movement Properties")]
    public float stepResolution = 50;

    private float stepSize;
    private Vector3 currPos;
    private bool hasHit = false;

    public void Start()
    {
        currPos = transform.position;
        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }

    private void SimplyDie(Collider2D col)
    {

        if (col.CompareTag("Damagable") || col.CompareTag("Player") || col.CompareTag("Enemy")) {
            HealthManager hp = col.gameObject.GetComponent<HealthManager>();
            if (hp != null)
            {
                hp.Damage(damage);
            }
        }

        Destroy(this.gameObject);
    }

    public void FixedUpdate()
    {
        stepSize = 1f / stepResolution;
        for (float step = 0; step < 1f; step+= stepSize) {
            if (!hasHit)
            {
                Collider2D[] results = Physics2D.OverlapCircleAll(currPos, myCollider.radius);
                
                foreach (Collider2D col in results )
                {
                    if (!col.Equals(myCollider) && !hasHit) {
                        hasHit = true;
                        SimplyDie(col);
                    }
                }

                currPos = currPos + transform.up * velocity * stepSize * Time.deltaTime;
                transform.position = currPos;
            }
        }
    }

}
