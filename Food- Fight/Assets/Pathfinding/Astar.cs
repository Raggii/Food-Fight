using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarNode
{
    public AstarNode from;
    public float x;
    public float y;

    public int g; //Movement cost from start to here
    public int h; //Heuristic estimate at cost from here to target
    public int f; //g+h

    private void get_h()
    {

    }

    public void update(int distFrom)
    {
        g = from.g + distFrom;
    }
}

public class Astar : MonoBehaviour
{

    public GameObject target;

    private ArrayList<>

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = new Vector2((Input.mousePosition.x / Screen.width)*3, (Input.mousePosition.y / Screen.height))*3;
        debug_drawv2line(new Vector2(0, 0), mousePos);
        debug_drawv2line(new Vector2(0, 0), tileV2(mousePos, 0.3f));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="v">Input Vector</param>
    /// <param name="s">tile size in unity units or whatever</param>
    /// <returns></returns>
    Vector2 tileV2(Vector2 v, float s)
    {
        float newx = Mathf.Floor(v.x / s) * s;
        float newy = Mathf.Floor(v.y / s) * s;
        return new Vector2(newx, newy);
    }

    void debug_drawv2line(Vector2 start, Vector2 end)
    {
        Debug.DrawLine(new Vector3(start.x, start.y, 0), new Vector3(end.x, end.y, 0), Color.green);
    }
}
