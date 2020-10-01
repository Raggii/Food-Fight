using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaddyAttackManager : MonoBehaviour
{

    public ShootingPatternGenerator gen;

    private Attack1Class attack1;
    private bool ready = true;

    public void Start()
    {
        attack1 = new Attack1Class(gen);
        attack1.SetInitialParams();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        if (ready)
        {
            float waitTime = 1.5f;
            ready = false;
            attack1.Attack1();
            yield return new WaitForSeconds(waitTime);
            attack1.Attack2();
            yield return new WaitForSeconds(waitTime);
            ready = true;
        }
    }
}
