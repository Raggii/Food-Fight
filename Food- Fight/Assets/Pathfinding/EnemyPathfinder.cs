using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinder
{
    public class Obstacle
    {
        public GameObject obstacleObject;

        private List<Vector2> corners = new List<Vector2>(4);

        public bool getFrameIntersect(Vector2 source, Vector2 direction, out Vector2 intersection)
        {

        }

        public Vector2 getCornerPoint(Vector2 source, Vector2 direction)
        {
            // Get the two corners on the side of the line (parallel to the bisecting line)
            // running through the obstacle centre that is on the same side of the bisection line
            List<Vector2> sameSideCorners = new List<Vector2>();
            
            foreach (Vector2 corner in corners)
            {
                if (/*####*/)
                {
                    sameSideCorners.Add(corner)
                }

                if (sameSideCorners.Count >= 2)
                {
                    break;
                } 
            }

            // Get the corner of the two corners found before that is closest to the enemy's current
            // position. This is found by performing the dot product between the enemy-corner vector and the
            // enemy-obstacleCenter vector. Whichever has the smallest value is the closest corner. Return this

            Vector2 closest;
            float best;
            foreach(Vector2 corner in sameSideCorners)
            {
                dot = /*####*/;
                if (dot > best)
                {
                    best = dot;
                    closest = corner;
                }
            }

            return closest;
        }
    }

    public class EnemyPathfinder : MonoBehaviour
        {
            // Start is called before the first frame update
            void Start()
            {
                
            }

            // Update is called once per frame
            void Update()
            {

            }
        }
}


