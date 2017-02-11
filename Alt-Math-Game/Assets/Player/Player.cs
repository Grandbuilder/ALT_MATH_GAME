using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Still need:
    //Display current equation
    //Player moves to each next enemy
    //Polished victory and loss. Currently exits a built game immediately.

    public int lookSpeed = 5;   //rotation speed of player
    private GameObject currentEnemy;    //currently active enemy
    private int numEnemies;     //number of enemies on this level
    private GameObject[] enemies;   //enemies array
    private int activeEnemyIndex;   //current index of enemy array (progress in level)
    public int health = 3;          //player health. 1 damage taken per failed encounter
    private EquationGen[] equations;    //array of equations to be generated for level
    public string stringToEdit;
    private float camHeight;
    private float camWidth;
    private Rect inputBox;
    private bool wrongAnswer;
    // Use this for initialization
    void Start()
    {
        camHeight = Camera.main.pixelHeight;
        camWidth = Camera.main.pixelWidth;
        inputBox = new Rect(camWidth * .4f, camHeight * .9f, 200, 20);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        numEnemies = enemies.Length;
        activeEnemyIndex = 0;
        equations = new EquationGen[numEnemies];
        //set all enemies to inactive on first update except first enemy
        //equation generated for each enemy
        for (int i = 0; i < numEnemies; i++)
        {
            equations[i] = new EquationGen();
            enemies[i].SetActive(false);
        }
        enemies[0].SetActive(true);
        Debug.Log(numEnemies);
        currentEnemy = enemies[0];
    }

    // Update is called once per frame
    void Update()
    {
        //updates damage of player, enemy alive or dead, activates next enemy if dead. checks victory
        updateActiveEnemy();
        //look towards target
        trackEnemy();
    }
    /// <summary>
    /// Game over when player reaches 0 hp or end of enemies
    /// Otherwise, move to next enemy if current enemy died
    /// </summary>
    void updateActiveEnemy()
    {
        //if current enemy is no longer active
        if (!currentEnemy.activeSelf)
        {
            Enemy curEnemy = currentEnemy.GetComponent(typeof(Enemy)) as Enemy;
            //if enemy reached player or player gave wrong answer, lose 1 hp and check if dead.
            if (curEnemy.reachedPlayer || wrongAnswer)
            {
                wrongAnswer = false;
                health--;
                if (health <= 0)
                {
                    //gameover! retry level?
                    endGame();
                }
            }
            //increment enemies index
            activeEnemyIndex++;
            //if we haven't beat all enemies
            if (activeEnemyIndex < numEnemies)
            {
                //current enemy is next enemy in array of enemies, activate it
                enemies[activeEnemyIndex].SetActive(true);
                currentEnemy = enemies[activeEnemyIndex];
            }
            //if all enemies have been encountered.
            else
            {
                //victory
                endGame();
            }
        }
    }
    /// <summary>
    /// Look towards current enemy each frame.
    /// </summary>
    void trackEnemy()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,
    Quaternion.LookRotation(currentEnemy.transform.position - transform.position),
    lookSpeed * Time.deltaTime);
    }
    /// <summary>
    /// Check if player input was submitted, and if it matches equation solution.
    /// Enemy dies and player loses health if failed. Check for death.
    /// Enemy dies if correct.
    /// Record correct or incorrect answer.
    /// </summary>
    void checkInput()
    {
        Debug.Log(numEnemies);
        currentEnemy.SetActive(false);
        //equation generator input comparison
        if (stringToEdit == equations[activeEnemyIndex].solution)
        {
            //good job! move to next enemy
        }
        else
        {
            wrongAnswer = true;
        }
    }
    void endGame()
    {
        Application.Quit();
    }
    void OnGUI()
    {
        GUI.SetNextControlName("MyTextField");
        stringToEdit = GUI.TextField(inputBox, stringToEdit, 25);
        GUI.FocusControl("MyTextField");
        Event e = Event.current;
        if (e.keyCode == KeyCode.Return)
        {
            checkInput();
        }
    }
}
