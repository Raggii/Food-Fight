using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public ShootingPatternGenerator projGen;
    public List<AttackCombo> combos = new List<AttackCombo>();
    

    private int i = 0;
    private int j = 0;
    private bool isRunning = false;


    private void Start()
    {
       projGen.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRunning == false)
        {
            StartCoroutine(Attack());
            isRunning = true;
        }
    }


    private IEnumerator Attack()
    {
        projGen.enabled = true;
        if (projGen is null == false)
        {
            for (i = 0; i < combos.Count; i++)
            {
                if (projGen is null == false)
                {
                    Debug.Log(j);
                    for (j = 0; j < combos[i].attacks.Count; j++)
                    {
                        if (projGen is null == false)
                        {
                            projGen.SetAttack(combos[i].attacks[j]);
                            projGen.Shoot();
                            yield return new WaitForSeconds(combos[i].attacks[j].delay);
                        }
                    }
                    yield return new WaitForSeconds(combos[i].delay);
                }

            }
        }
        projGen.enabled = false;
        isRunning = false;

    }
}
