using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotState {
    idle = 0,
    agro,
    attacking,
    waitingForWall,
    stunned,
    idleDecision
};

public class PotController : MonoBehaviour
{
    [Header("Components")]
    public BoxCollider2D col;
    public Animator anim;
    public EnemeyController motor;

    [Header("Attack Stats")]
    public float range;
    public float attackSpeed;
    public float damage;

    [Header("Animation timing")]
    public float agroTime;
    public float attackingTime;
    public float stunnedTime;

    private PotState state = PotState.idle;
    private float transitionCount = 0;
    private float transitionTarget = 0;
    private bool playerAttackable = false;
    private int wallHitCount = 0;
    private Vector3 direction; 

    void Awake()
    {
        anim.SetBool("PlayerVisibleAndRange", false);
        anim.SetBool("Attacking", false);
    }


    void Update()
    {
        playerAttackable =
            motor.DistanceFromPlayer() <= range &&
            motor.IsPlayerVisible();

        switch (state)
        {
            
            case PotState.idle:
                if (playerAttackable)
                {
                    state = PotState.agro;
                }
                return;

            case PotState.agro:
                HandleAgro();
                return;

            case PotState.attacking:
                HandleAttacking();
                return;

            case PotState.waitingForWall:
                HandleMovement();
                return;

            case PotState.stunned:
                HandleStunned();
                return;

            case PotState.idleDecision:
                HandleDecision();
                return;

        }
    }

    void HandleAgro()
    {
        if (!anim.GetBool("PlayerVisibleAndRange") && playerAttackable)
        {
            transitionTarget = Time.time + agroTime;
            anim.SetBool("PlayerVisibleAndRange", true);
            wallHitCount = 0;
            motor.DisableMovement();
        }

        if (anim.GetBool("PlayerVisibleAndRange") && !playerAttackable)
        {
            anim.SetBool("PlayerVisibleAndRange", false);
            motor.EnableMovement();
            state = PotState.idle;
        }

        if (transitionTarget <= transitionCount)
        {
            state = PotState.attacking;
        }
        transitionCount = Time.time;
    }


    void HandleAttacking()
    {
        if (!anim.GetBool("Attacking"))
        {
            anim.SetBool("Attacking", true);
            motor.DisableMovement();
            motor.DisableSensors();
            direction = motor.getLastKnownLocation() - transform.position;
            transitionTarget = Time.time + attackingTime;
        }

        if (transitionTarget <= transitionCount)
        {
            motor.DisableMovement();
            motor.DisableSensors();
            state = PotState.waitingForWall;
        }
        transitionCount = Time.time;
    }

    void HandleMovement()
    {
        if (MoveUntilWall(direction, attackSpeed, 0.01f))
        {
            state = PotState.stunned;
        }
    }

    void HandleStunned()
    {
        if (anim.GetBool("Attacking"))
        {
            anim.SetBool("Attacking", false);
            anim.SetBool("ReadyForTransition", false);
            transitionTarget = Time.time + stunnedTime;
        }

        if (transitionTarget <= transitionCount)
        {
            state = PotState.idleDecision;
        }
        transitionCount = Time.time;
    }

    void HandleDecision()
    {
        if (anim.GetBool("PlayerVisibleAndRange"))
        {
            transitionTarget = Time.time + Random.Range(0.4f, 1.25f);
            anim.SetBool("PlayerVisibleAndRange", false);
        }

        if (transitionTarget <= transitionCount)
        {
            motor.EnableMovement();
            motor.EnableSensors();
            anim.SetBool("ReadyForTransition", true);
            state = PotState.idle;
        }
        transitionCount = Time.time;
    }

    public bool MoveUntilWall(Vector3 direction, float speed, float threshold)
    {

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.fixedDeltaTime);
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, col.size.x, direction);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit && hit.distance <= threshold) {
                Collider2D collider = hit.collider;
                if (collider.tag == "Untagged")
                {
                    return true;
                } else if (!collider.Equals(col) && (collider.tag == "Damagable" || collider.tag == "Player"))
                {
                    collider.GetComponent<HealthManager>().Damage(damage);
                }
            }
        }

        return false;
    }

}
