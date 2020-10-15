using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeSceen : MonoBehaviour
{

    public int index;
    public string lvlName;
    public Animator fader;

    public float transTime;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            loadNewLevel();

        }
    
    }

    public void loadNewLevel() 
    {
        StartCoroutine(LoadLevel(index));
        //SceneManager.LoadScene(lvlName);
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // play animation
        fader.SetTrigger("Start");

        yield return new WaitForSeconds(transTime);

        SceneManager.LoadScene(levelIndex);
        //wait for animation
    
    }
}
