using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDown : MonoBehaviour
{
    public GameObject[] objects;
    // Start is called before the first frame update
    void Start()
    {
        if (objects.Length <= 0)
        {
            return;
        }
        int rand = Random.Range(0, objects.Length);
        Instantiate(objects[rand], transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 0f));
    }
}
