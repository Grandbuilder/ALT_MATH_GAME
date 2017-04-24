using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeGrab : MonoBehaviour
{

    private Rect timeBox;
    private Rect scoreBox;
    void Start()
    {
        timeBox = new Rect(Camera.main.pixelWidth * 0.4f, Camera.main.pixelHeight * 0.6f, 200, 200);
        scoreBox = new Rect(Camera.main.pixelWidth * 0.4f, Camera.main.pixelHeight * 0.8f, 200, 200);
    }
    void OnGUI()
    {
        var style = new GUIStyle();
        float average = 0;

        style.fontSize = 20;
        style.normal.textColor = Color.white;
        float f = 0;
        if (Player.timeTakenPerEnemy.Count != 0)
        {
            foreach (float result in Player.timeTakenPerEnemy)
            {
                f += result;
            }
            average = f / Player.timeTakenPerEnemy.Count;

        }
        GUI.Label(timeBox, "Average Time Taken Per Question: \n" + average.ToString(), style);
        GUI.Label(scoreBox, "Final Score: \n" + Player.score.ToString(), style);
    }
}
