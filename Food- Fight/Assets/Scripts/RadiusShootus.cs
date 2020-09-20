using UnityEngine;

public class RadiusShootus : MonoBehaviour
{

    public float startRadius = 5f;
    public float increment = 1f;
    public int iterations = 4;
    public float angleChange = 20f;
    public bool seeCircles = true;

    private float cnt = 0;
    private float currRadius;
    private Vector3 t = new Vector3(0, 0);
    private Vector3 prevPoint = new Vector3(0, 0);


    private void OnDrawGizmosSelected()
    {        
        currRadius = startRadius;
        Gizmos.color = Color.red;
        t = transform.position + transform.up;
        prevPoint = transform.position;

        for (int i = 0; i < iterations; i++)
        {

            currRadius += increment;
            DrawCircle(currRadius);
            t = NextCoords(t , angleChange);
            //t.z = Mathf.Sin(cnt)* cnt;

            Gizmos.DrawIcon(t * currRadius, "Radius");
            Gizmos.DrawLine(prevPoint, t * currRadius);
            prevPoint = t * currRadius;
        }
        
    }

    
    private void FixedUpdate()
    {
        
        cnt += 0.1f;
        if(cnt >= 360)
        {
            cnt = 0;
        }
        angleChange = cnt;
    }

    private Vector3 NextCoords(Vector3 dir, float angle)
    {
        angle = angle * Mathf.Deg2Rad;
        Vector3 coords = new Vector3(0, 0, 0)
        {
            x = dir.x * Mathf.Cos(angle) - dir.y * Mathf.Sin(angle),
            y = dir.x * Mathf.Sin(angle) + dir.y * Mathf.Cos(angle)
        };

        return coords;
    }

    private void DrawCircle(float radius)
    {
        if (seeCircles)
        {
            Gizmos.DrawWireSphere(this.transform.position, radius);
        }
    }

}
