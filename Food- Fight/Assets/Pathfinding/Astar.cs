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
        //debug_drawv2line(this.gameObject.transform.position, new Vector2(algotrgtpos.x * tilescale, algotrgtpos.y * tilescale));

        AstarNode node = algotrgtpos;
        backpropogate(node);

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

    private void backpropogate(AstarNode node)
    {
        AstarNode prevnode = node;
        debug_drawv2line(new Vector3(prevnode.x * tilescale, prevnode.y * tilescale), new Vector3(node.from.x * tilescale, node.from.y * tilescale));
        AstarNode newNode = node.from;
        while(newNode.from != null)
        {
            debug_drawv2line(new Vector3(newNode.x * tilescale, newNode.y * tilescale), new Vector3(newNode.from.x * tilescale, newNode.from.y * tilescale));

            if (newNode.g - newNode.from.g != prevnode.g - newNode.g)
            {
                // !!CORNER!!!! ! !
                Debug.DrawLine(this.gameObject.transform.position, new Vector3(newNode.x * tilescale, newNode.y * tilescale), Color.red);
            }

            prevnode = newNode;
            newNode = newNode.from;
        }
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
