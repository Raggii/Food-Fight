using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimWeapon : MonoBehaviour
{
    public float offset;
    public GameObject player;
    public SpriteRenderer gunRenderer;


    void Update()
    {
        Vector3 mouseWorldCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldCoords.z = transform.position.z;
        transform.LookAt(mouseWorldCoords);
    }
}
