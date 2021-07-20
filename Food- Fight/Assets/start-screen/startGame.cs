using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{

    public int firstSceneNo; 


    public void OnButtonPress()
    {
        SceneManager.LoadScene(firstSceneNo);
    }
}
