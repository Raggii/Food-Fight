using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyController : MonoBehaviour
{

    public GameObject player;
    public Vector3 offset = new Vector3(0.0001f, 0.0001f, 0);

    private Vector3 currentDir = new Vector3(0, 0, 0);
    private readonly Vector2[] sensorsDirs = new Vector2[3] { new Vector2(-1, 1).normalized, new Vector2(0, 1).normalized, new Vector2(1, 1).normalized };
    private List<RaycastHit2D> hits = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hits.Clear();

        // Check player
        if (player)
        {
            RaycastHit2D playerHit = Physics2D.Raycast(transform.position + offset, (player.transform.position - transform.position).normalized);
            if (playerHit)
            {
                hits.Add(playerHit);
            }
        }

        // Check sensors
        for (int i = 0; i < sensorsDirs.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + offset, sensorsDirs[i].normalized);
            if (hit)
            {
                hits.Add(hit);
            }
        }

    }



    private void OnDrawGizmosSelected()
    {
        foreach (RaycastHit2D hitPoint in hits) {

            Gizmos.DrawLine(transform.position, hitPoint.point);
        }
    }
}
