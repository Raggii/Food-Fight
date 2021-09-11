using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openSettings : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject settingsMenu;

    public void openMenu()
    {
        settingsMenu.SetActive(true);
        Debug.Log("button Clicked");
    }
}