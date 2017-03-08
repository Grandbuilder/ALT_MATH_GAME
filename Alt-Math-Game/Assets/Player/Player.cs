using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    //Still need:
    //Display current equation
    //display hp -after prototype
    //Player moves to each next enemy -after prototype
    //Polished victory and loss. Currently exits a built game immediately. -after prototype

    public int lookSpeed = 5;   //rotation speed of player
    public int health = 3;          //player health. 1 damage taken per failed encounter
    public GameObject equationText;
    private GameObject currentEnemy;    //currently active enemy
    private int numEnemies;     //number of enemies on this level
    private GameObject[] enemies;   //enemies array
    private int activeEnemyIndex;   //current index of enemy array (progress in level)
    private EquationGen[] equations;    //array of equations to be generated for level
    private string stringToEdit;
    private float camHeight;
    private float camWidth;
    private Rect inputBox;
    private bool wrongAnswer;
    private bool enterStillDown;        //for preventing duplicate input processing from one key press

    //main music for the game
    private AudioSource mainMusic;
    AudioClip apac;
    //sound for writing
    private AudioSource write;
    AudioClip wrt;
    //sound for attack
    private AudioSource monster;
    AudioClip mstr;

    // Use this for initialization
    void Start()
    {
        //initializing the game music
        mainMusic = gameObject.AddComponent<AudioSource>();
        apac = (AudioClip)Resources.Load("Audio/apacolypse");
        mainMusic.clip = apac;
        mainMusic.loop = true;
        mainMusic.Play();

        write = gameObject.AddComponent<AudioSource>();
        wrt = (AudioClip)Resources.Load("Audio/write");
        write.clip = wrt;

        monster = gameObject.AddComponent<AudioSource>();
        mstr = (AudioClip)Resources.Load("Audio/monster");
        monster.clip = mstr;

        stringToEdit = "";
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
        equationText.transform.GetComponent<Text>().text = "Equation:     " + equations[activeEnemyIndex].equation;
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
                monster.PlayOneShot(mstr);
                wrongAnswer = false;
                health--;
                if (health <= 0)
                {
                    GameManager.gameOverScene();
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
            equationText.transform.GetComponent<Text>().text = "Equation: " + equations[activeEnemyIndex].equation;
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

    void endGame()
    {
        Application.Quit();
    }
    /// <summary>
    /// Runs every frame, built in function.
    /// We use it to keep an input text box and to check input.
    /// </summary>
    void OnGUI()
    {
        GUI.SetNextControlName("mytextfield");
        stringToEdit = GUI.TextField(inputBox, stringToEdit, 25);
        GUI.FocusControl("mytextfield");

        /// <summary>
        /// Check if player input was submitted, and if it matches equation solution.
        /// Enemy dies and player loses health if failed. Check for death.
        /// Enemy dies if correct.
        /// AFTER PROTOTYPE: Record correct or incorrect answer.
        /// </summary>
        if (Event.current.isKey && Event.current.keyCode == KeyCode.Return && !enterStillDown)
        {
            enterStillDown = true;
            currentEnemy.SetActive(false);
            //equation generator input comparison
            if (stringToEdit != equations[activeEnemyIndex].solution)
            {
                wrongAnswer = true;
            }
            else
            {
                write.PlayOneShot(wrt);
            }
            stringToEdit = "";
        }
        if (Event.current.keyCode != KeyCode.Return)
        {
            enterStillDown = false;
        }
    }
}