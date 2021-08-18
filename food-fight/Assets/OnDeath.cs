using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeath : MonoBehaviour
{

    public GameObject deathPannel; // Displays the image of this when u die
    public GameObject player; // to get the health manager of the player


    // Update is called once per frame
    void Update()
    {

        HealthManager management = player.GetComponent<HealthManager>();
        if (management.getAmountOfHealth())
        {
            Debug.Log("Dead?");
            deathPannel.SetActive(true);
        }


    }


}
