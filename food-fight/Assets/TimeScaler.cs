using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    bool slow = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            slow = !slow;
            HandleSlow();
        }
    }

    void HandleSlow()
    {
        if (slow)
        {
            Time.timeScale = 0.3f;
        } else
        {
            Time.timeScale = 1f;
        }
    }

}
