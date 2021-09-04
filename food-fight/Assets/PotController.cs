using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotController : MonoBehaviour
{

    public float range;
    public Animator anim;
    public EnemeyController con;
    public float attackSpeed;
    public float hostileStartupTime; 

    private bool startingAttack = false;
    private float hostileStartupCount = 0;
    private float targetHostileTime = 0;

    private bool attacking = false;
    private Vector3 lastKnownLocation;

    void Start()
    {
        // anim.SetBool("PlayerVisibleAndRange", true);
        // anim.SetBool("Attacking", true);
    }


    void Update()
    {
        startingAttack = con.DistanceFromPlayer() <= range && con.IsPlayerVisible();
        Debug.Log(startingAttack);

        // Begin agro
        HandleAgro();

        // Start attack
        StartCoroutine(HandleAttacking());
    }

    void HandleAgro()
    {
        if (startingAttack || attacking)
        {
            con.DisableMovement();
            anim.SetBool("PlayerVisibleAndRange", true);
        }
        else
        {
            con.EnableMovement();
            anim.SetBool("PlayerVisibleAndRange", false);
            targetHostileTime = Time.time + hostileStartupTime;
        }
        hostileStartupCount = Time.time;
    }


    IEnumerator HandleAttacking()
    {
        if (startingAttack && targetHostileTime <= hostileStartupCount)
        {
            anim.SetBool("Attacking", true);
            attacking = true;
            con.DisableSensors();
            yield return new WaitForSeconds(0.8f);
            con.MoveTowardsLastKnownLocation();
        }
    }


    
}
