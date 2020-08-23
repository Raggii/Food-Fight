using System;
using System.Collections;
using System.Collections.Generic;
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
}

public class Astar : MonoBehaviour
{

    public GameObject target;

    List<AstarNode> open = new List<AstarNode>();
    List<AstarNode> closed = new List<AstarNode>();

    float tilescale = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 algotrgtpos = algo(); 
        debug_drawv2line(this.gameObject.transform.position, algotrgtpos);

        open.Clear();
        closed.Clear();
    }

    private Vector3 algo()
    {
        Tuple<int, int> thispos = tileV2(this.gameObject.transform.position, tilescale);
        open.Add(new AstarNode(null, thispos, 0, taxicabDistance(thispos, tileV2(target.transform.position, tilescale)))); //start node
        AstarNode best = open[0];
        searchAround(best);
        best = open[0];
        while (best.h > 2)
        {
            foreach(AstarNode n in open)
            {
                if (n.f <= best.f) 
                {
                    best = n;
                }
            }
            searchAround(best);
            best = open[0];
        }
        //Debug.Log("I git here");
        return new Vector3(best.x * tilescale, best.y * tilescale);
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
            bool cflag = false;
            bool oflag = false;
            foreach (AstarNode n in closed) // Search through 'closed' for already existing node
            {
                if (n.x == pos.Item1 && n.y == pos.Item2)
                {
                    // The node here is closed, so we can ignore it
                    cflag = true;
                    break;
                }
            }

            if (!cflag)
            {
                foreach (AstarNode n in open) // Search through 'open' for already existing node
                {
                    if (n.x == pos.Item1 && n.y == pos.Item2)
                    {
                        // The node here already exists, so we can update it's g score if it is shorter than what it is currently
                        if (node.g + pos.Item3 < n.g)
                        {
                            n.g = node.g + pos.Item3;
                        }
                        oflag = true;
                        break;
                    }
                }
            }

            if (!oflag && !cflag)
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
        return Mathf.Abs(nodepos.Item1 - trgtpos.Item1) + Mathf.Abs(nodepos.Item2 - trgtpos.Item2) * 10;
    }

    private AstarNode searchOpenFor(Tuple<int, int> pos) //Comb through 'open' looking for given node. 'from' parameter ignored
    {
        foreach (AstarNode n in open)
        {
            if (n.x == pos.Item1 && n.y == pos.Item2)
            {
                return n;
            }
        }
        return null;
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
