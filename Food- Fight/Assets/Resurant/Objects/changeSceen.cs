using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeSceen : MonoBehaviour
{

    public int index;
    public string lvlName;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {

            SceneManager.LoadScene(index);
            //SceneManager.LoadScene(lvlName);

        }
    
    }
}
