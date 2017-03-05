﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    static int numScenes = 3;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// Wrapper method for making startnextlevel none-static
    /// This is needed to use the method in unity button ui.
    /// </summary>
    public void nextSceneWrapper()
    {
        nextScene();
    }
    public static void nextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == numScenes-1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
    public static void gameOverScene()
    {
        SceneManager.LoadScene(numScenes-1);
    }
}
