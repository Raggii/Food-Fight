using UnityEngine;

public class HealthManager : MonoBehaviour
{

    [Header("\"Static\" Values")]
    public float maxHealth = 100f;
    public bool destroyOnDeath = false;
    public bool screenShakeOnHit = false;
    public float shakeMagnitude = 0.4f;
    public float shakeDuration = 0.15f;

    public CamShake camShake;

    private float currentHealth = 100f;

    public void Awake()
    {
        currentHealth = maxHealth; // This needs to be set like this unless we set maxHealth to static.
    }

    public void SetCurrentHealth(float health)
    {
        if (health < 0)
        {
            currentHealth = 0;
        } else
        {
            this.currentHealth = maxHealth;
        }
    }
     

    public void FixedUpdate()
    {
        if (IsDead() && destroyOnDeath)
        {
            Destroy(this.gameObject);
        }
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    public bool Damage(float damage)
    {

        if(screenShakeOnHit)
        {
            StartCoroutine(camShake.Shake(shakeDuration, shakeMagnitude));
        }

        if (currentHealth - damage < 0)
        {
            currentHealth = 0;
        } else
        {
            currentHealth -= damage;
        }

        return IsDead();
    }
    
    public void Heal(float heal)
    {
        if (currentHealth + heal > maxHealth)
        {
            currentHealth = maxHealth;
        } else {
            currentHealth += heal;
        }
    }
    
}
