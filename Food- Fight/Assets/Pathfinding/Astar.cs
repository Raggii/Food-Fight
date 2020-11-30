using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AstarNode
{
    public AstarNode from;

    public int x; //x-coordinate in nodespace
    public int y; //y-coordinate in nodespace

    public int g; //Movement cost from start to here
    public int h; //Heuristic estimate at cost from here to target
    public int f; //g+h

    public AstarNode(AstarNode from, Tuple<int, int> pos, int distFrom, int distTrgt)
    {
        this.from = from;
        this.x = pos.Item1;
        this.y = pos.Item2;

        if (from == null)
        {
            g = 0;
        }
        else
        {
            g = from.g + distFrom;
        }
        h = distTrgt;
        f = g + h;
    }

    public void update()
    {
        f = g + h;
    }
}

public class Astar : MonoBehaviour
{

    private float MAXIMUM_REPULSION_DISTANCE = 1.0f; //The maximum distance in which repulsion can take effect

    private bool DEBUG = true;

    

    private struct sSpline
    {
        public List<Vector2> points;

        public sSpline(List<Vector2> points)
        {
            this.points = points;
        }

        public Vector2 GetSplinePoint(float t)
        {
            int p0, p1, p2, p3;

            p1 = (int)t + 1;
            p2 = p1 + 1;
            p3 = p2 + 1;
            p0 = p1 - 1;

            t = t - (int)t;

            float tt = t * t;
            float ttt = tt * t;

            float q1 = -ttt + 2.0f * tt - t;
            float q2 = 3.0f * ttt - 5.0f * tt + 2.0f;
            float q3 = -3.0f * ttt + 4.0f * tt + t;
            float q4 = ttt - tt;

            float tx = 0.5f * (points[p0].x * q1 + points[p1].x * q2 + points[p2].x * q3 + points[p3].x * q4);
            float ty = 0.5f * (points[p0].y * q1 + points[p1].y * q2 + points[p2].y * q3 + points[p3].y * q4);

            return new Vector2(tx, ty);
        }
    }

    public GameObject target;

    public GameObject[] obs;
    Collider2D[] colliders;

    List<AstarNode> open = new List<AstarNode>();
    List<AstarNode> closed = new List<AstarNode>();

    float tilescale = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        colliders = new Collider2D[obs.Length];
        for(int i = 0; i < obs.Length; i++)
        {
            colliders[i] = obs[i].GetComponent("Collider2D") as Collider2D;
        }
    }

    // Update is called once per frame
    void Update()
    {
        AstarNode algotrgtpos = algo(); 

        AstarNode node = algotrgtpos;
        List<Vector2> corners = backpropogate(node);
        RepelPointsFromObstacles(corners);
        //corners.Insert(0, target.gameObject.transform.position);
        corners.Insert(0, target.gameObject.transform.position);
        //corners.Insert(corners.Count - 1, this.gameObject.transform.position);
        corners.Insert(corners.Count, this.gameObject.transform.position);

        //Calculate spline points. Only if there are more than 3 points
        if (corners.Count > 3)
        {
            sSpline spline = new sSpline(corners);
            Vector2 prevPoint = spline.GetSplinePoint(0f);
            Vector2 newPoint;
            for (float t = 0; t < (float)spline.points.Count - 3; t += 0.1f)
            {
                newPoint = spline.GetSplinePoint(t);
                Debug.DrawLine(prevPoint, newPoint, Color.yellow);
                prevPoint = newPoint;
            }
        }

        open.Clear();
        closed.Clear();
    }

    private AstarNode algo()
    {
        Tuple<int, int> thispos = tileV2(this.gameObject.transform.position, tilescale);
        Tuple<int, int> trgtpos = tileV2(target.transform.position, tilescale);
        open.Add(new AstarNode(null, thispos, 0, taxicabDistance(thispos, trgtpos))); //start node
        AstarNode best = open[0];

        while (best.h > 10)
        {
            searchAround(best);
            best = open[0];
            foreach (AstarNode n in open)
            {
                if (n.f < best.f)
                {
                    best = n;
                }
            }
        }
        return best;
    }

    private void searchAround(AstarNode node)
    {
        open.Remove(node);
        closed.Add(node);

        var searchPos = new List<Tuple<int, int, int>>() {
            //deltax, deltay, approximate distance * 10
            Tuple.Create(node.x,     node.y + 1,    10),
            Tuple.Create(node.x + 1, node.y + 1,    14),
            Tuple.Create(node.x + 1, node.y,        10),
            Tuple.Create(node.x + 1, node.y - 1,    14),
            Tuple.Create(node.x,     node.y - 1,    10),
            Tuple.Create(node.x - 1, node.y - 1,    14),
            Tuple.Create(node.x - 1, node.y,        10),
            Tuple.Create(node.x - 1, node.y + 1,    14),
        };

        foreach (Tuple<int, int, int> pos in searchPos)
        {
            bool obstructflag = false;
            bool cflag = false;
            bool oflag = false;

            //Check if the node lies on an obstruction
            Vector2 vec = new Vector2(pos.Item1 * tilescale, pos.Item2 * tilescale);
            foreach(Collider2D c in colliders)
            {
                if (c.OverlapPoint(vec))
                {
                    //We cannot put a node here as it would be inside a solid object
                    obstructflag = true;
                }
            }

            if (!obstructflag)
            {
                foreach (AstarNode n in closed) // Search through 'closed' for already existing node
                {
                    if (n.x == pos.Item1 && n.y == pos.Item2)
                    {
                        // The node here is closed, so we can ignore it
                        cflag = true;
                        break;
                    }
                }
            }

            if (!cflag && !obstructflag)
            {
                foreach (AstarNode n in open) // Search through 'open' for already existing node
                {
                    if (n.x == pos.Item1 && n.y == pos.Item2)
                    {
                        // The node here already exists, so we can update it's g score if it is shorter than what it is currently
                        if (node.g + pos.Item3 < n.g)
                        {
                            n.g = node.g + pos.Item3;
                            n.from = node;
                            n.update();
                        }
                        oflag = true;
                        break;
                    }
                }
            }

            if (!oflag && !cflag && !obstructflag)
            {
                //There is no node in the current location. make a new one
                Tuple<int, int> nodepos = new Tuple<int, int>(pos.Item1, pos.Item2);
                AstarNode newNode = new AstarNode(node, nodepos, pos.Item3, taxicabDistance(nodepos, tileV2(target.transform.position, tilescale)));
                open.Add(newNode);

            }
        }
    }

    private int taxicabDistance(Tuple<int, int> nodepos, Tuple<int, int> trgtpos) //Determine the distance between node and target as if we were a cabbie fighting one's way 
                                                                         //through the lawless streets of manhattan
    {
        return (Mathf.Abs(nodepos.Item1 - trgtpos.Item1) + Mathf.Abs(nodepos.Item2 - trgtpos.Item2)) * 10;
    }

    private List<Vector2> backpropogate(AstarNode node)
    {
        List<Vector2> points = new List<Vector2>();
        AstarNode prevnode = node;
        if (DEBUG)
        {
            debug_drawv2line(new Vector3(prevnode.x * tilescale, prevnode.y * tilescale), new Vector3(node.from.x * tilescale, node.from.y * tilescale));
        }
        AstarNode newNode = node.from;
        while(newNode.from != null)
        {
            if (DEBUG)
            {
                debug_drawv2line(new Vector3(newNode.x * tilescale, newNode.y * tilescale), new Vector3(newNode.from.x * tilescale, newNode.from.y * tilescale));
            }

            if (newNode.g - newNode.from.g != prevnode.g - newNode.g)
            {
                // !!CORNER!!!! ! !
                points.Add(new Vector3(newNode.x * tilescale, newNode.y * tilescale));
            }

            prevnode = newNode;
            newNode = newNode.from;
        }

        return points;
    }

    private void RepelPointsFromObstacles(List<Vector2> points)
    {
        foreach (GameObject ob in obs)
        {
            for (int i = 0; i < points.Count(); i++)
            {

                Vector3 ob_centre = ob.transform.position;
                if (Vector2.Distance(ob_centre, points[i]) <= MAXIMUM_REPULSION_DISTANCE) //~~~BALLS~~~ there is a sqaure root here. how can it be removed?
                {
                    float scale_factor = MAXIMUM_REPULSION_DISTANCE * MAXIMUM_REPULSION_DISTANCE / (Mathf.Pow(points[i].x - ob_centre.x, 2) + Mathf.Pow(points[i].y - ob_centre.y, 2));
                    points[i] = new Vector2(scale_factor * (points[i].x - ob_centre.x) + ob_centre.x, scale_factor * (points[i].y - ob_centre.y) + ob_centre.y);
                }

                if (DEBUG)
                {
                    Debug.DrawLine(transform.position, points[i], Color.red);
                }
            }
        }
    }

    /// <summary>
    /// Checks if a point lies within an expanded envelope surrounding a axis-aligned box.
    /// </summary>
    /// <returns></returns>
    bool PointInExpandedQuad(Vector2 point, Vector2 quad0, Vector2 quad1, float expansion)
    {
        // The expanded envelope consists of a square in which each side is moved away from it's corresponding side by distance expansion
        if ((point.x > quad0.x - expansion) && (point.x < quad1.x + expansion) && (point.y < quad0.y + expansion) && (point.y > quad1.y - expansion))
        {
            return true;
        } else
        {
            return false;
        }
    }

    Vector2 RepelToBoundry()
    {
        return new Vector2(0,0); // Pieter I added this cause it complains!!!
    }

    Tuple<int, int> tileV2(Vector3 v, float s)
    {
        int newx = Mathf.FloorToInt(v.x / s);
        int newy = Mathf.FloorToInt(v.y / s);
        return new Tuple<int, int>(newx, newy);
    }

    void debug_drawv2line(Vector3 start, Vector3 end)
    {
        Debug.DrawLine(new Vector3(start.x, start.y, 0), new Vector3(end.x, end.y, 0), Color.green);
    }
}
