using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    [Header("\"Static\" Values")]
    public float maxHealth = 100f;
    public bool destroyOnDeath = false;
    public bool screenShakeOnHit = false;
    public float shakeMagnitude = 0.4f;
    public float shakeDuration = 0.15f;
    public GameObject deathDrop;

    public SpriteRenderer spriteRenderer;

    float currentHealth = 100f;

    [Header("Health Bar")]
    public int amountOfHealth;
    public int numOfHearts; // Get script from here

    public Image[] hearts;
    public Sprite fullHeart = null;
    public Sprite emptyHeart= null;

    private bool isHit = false;

    public bool getAmountOfHealth()
    {
        return amountOfHealth <= 0;


    }

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
            // This will stop all spawning
            // i added this because map gen askes for colliders and it doesnt like not having any
            deathDrop = null;
            if (deathDrop != null)
            {
                Instantiate(deathDrop, transform.position, Quaternion.identity);
            }

            Destroy(this.gameObject);
        }
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    public IEnumerator ShowHit()
    {
        spriteRenderer.color = new Color(1, .514151f, .514151f);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = new Color(1, 1, 1);
        yield return null;
    }

    public bool Damage(float damage)
    {
        StartCoroutine(ShowHit());

        if (screenShakeOnHit)
        {
            StartCoroutine(EffectsManager.ShakeMainCamera(shakeDuration, shakeMagnitude));
        }

        if (currentHealth - damage < 0)
        {
            currentHealth = 0;
        } else
        {
            currentHealth -= damage;
            amountOfHealth -= 1;
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

    //Health update stuff i guess;
    void Update()
    {

        if (amountOfHealth > numOfHearts)
        {

            amountOfHealth = numOfHearts;


        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < amountOfHealth)
            {

                hearts[i].sprite = fullHeart;
            }
            else
            {

                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {

                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;

            }
        }


    }

}
