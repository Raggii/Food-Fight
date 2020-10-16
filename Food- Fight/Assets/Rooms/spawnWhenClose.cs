using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnWhenClose : MonoBehaviour
{

    public GameObject[] objects;
    public GameObject player;
    // Start is called before the first frame update

    void Start()
    {
        if (objects.Length <= 0)
        {
            return;
        }

        //float dist = Vector2.Distance(transform.position, player.transform.position);

        //if (dist >= 3) { 
        int rand = Random.Range(0, objects.Length);
        Instantiate(objects[rand], transform.position, Quaternion.identity);
        //}
    }

}