using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject player;

    void Start()
    {


    }

    void Update()
    {
        if ((player.transform.position.x - transform.position.x) >= 5)
        {
            transform.Translate(transform.right * 10);
        }
        else if ((player.transform.position.x - transform.position.x) <= -5)
        {
            transform.Translate(transform.right * -10);
        }
        else if ((player.transform.position.y - transform.position.y) >= 3) {
            transform.Translate(transform.up * 6);
        }
        else if ((player.transform.position.y - transform.position.y) <= -3)
        {
            transform.Translate(transform.up * -6);
        }
    }
}
