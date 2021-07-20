using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPLevelController : MonoBehaviour
{

    public int currentXpLevel = 0;
    public int maxXpLevel = 20;

    
    public int GetCurrentXpLevel()
    {
        return currentXpLevel;
    }


    public void ResetXpLevel()
    {
        currentXpLevel = 0;
    }


    public void DecreaseXpLevel(int amount)
    {
        currentXpLevel = Mathf.Max(0, currentXpLevel-amount);
    }


    public void IncreaseXpLevel(int amount)
    {
        currentXpLevel = Mathf.Min(maxXpLevel, currentXpLevel + amount);
    }

}
