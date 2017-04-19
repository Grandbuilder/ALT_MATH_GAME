using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private int numLevels;     //number of levels in world
    private GameObject[] enemies;   //enemies array
    private GameObject[] levels;   //levels array. Each level holds child enemies
    private int activeEnemyIndex;   //current index of enemy array (progress in level)
    private EquationGen[] equations;    //array of equations to be generated for level
    private string stringToEdit;
    private float camHeight;
    private float camWidth;
    private Rect inputBox;
    private bool playDeath;
    private Rect scoreBox;
    private int score = 0;

    private Rect healthBox;

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
    public CanvasGroup myCG;
    private bool flash = false;
    // Use this for initialization
    private bool moving = false;
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
        inputBox = new Rect(camWidth * .4f, camHeight - 150, 200, 20);

        healthBox = new Rect(0, camHeight - 50, 100, 100);

        scoreBox = new Rect(0, camHeight - 25, 100, 100);

        levels = GameObject.FindGameObjectsWithTag("Level").OrderBy(go => go.name).ToArray(); ;
        numLevels = levels.Length;
        activeEnemyIndex = 0;
        //set all enemies to inactive on first update except first enemy
        //equation generated for each enemy 
        for (int i = 0; i < numLevels; i++)
        {
            numEnemies += levels[i].transform.childCount;//each level prefab has 20 children
        }
        enemies = new GameObject[numEnemies];
        equations = new EquationGen[numEnemies];
        for (int i = 0; i < numLevels; i++)
        {
            for (int k = 0; k < 20; k++)
            {
                enemies[i * 20 + k] = levels[i].transform.GetChild(k).gameObject;
                equations[i * 20 + k] = new EquationGen(i + 1);//generates equation based off current level
                enemies[i * 20 + k].SetActive(false);
                Debug.Log("enemy initialized: " + (i * 20 + k));
            }

        }


        enemies[0].SetActive(true);
        //this the equation shown
        equationText.transform.GetComponent<Text>().text = "Equation: " + equations[activeEnemyIndex].equation;
        currentEnemy = enemies[0];
        myCG = GameObject.Find("Flash").GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        //updates damage of player, enemy alive or dead, activates next enemy if dead. checks victory
        updateActiveEnemy();
        //look towards target
        trackEnemy();
        if(playDeath)
        {
            currentEnemy.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            currentEnemy.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            currentEnemy.gameObject.transform.GetChild(2).gameObject.SetActive(false);
            currentEnemy.GetComponent<Enemy>().dead = true;
            playDeath = false;
        }
        if (flash)
        {

            myCG.alpha = myCG.alpha - Time.deltaTime;
            if (myCG.alpha <= 0)
            {
                myCG.alpha = 0;
                flash = false;
            }
        }
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
                flash = true;
                myCG.alpha = 1;
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
                Debug.Log("Active Enemy Index:" + activeEnemyIndex);
                if (activeEnemyIndex == 20)
                {
                    if (transform.position != levels[1].transform.position)
                    {
                        moving = true;
                        currentEnemy = enemies[activeEnemyIndex];
                        activeEnemyIndex--;//keep active index in check
                        Vector3 newVec = transform.position + transform.forward * 7f * Time.deltaTime;
                        newVec.y = transform.position.y;
                        transform.position = newVec;
                        if (Vector3.Distance(transform.position, levels[1].transform.position) <= 7)
                        {
                            //reset player health each level
                            health = 3;
                            transform.position = levels[1].transform.position;
                        }
                    }
                    else
                    {
                        moving = false;
                        enemies[activeEnemyIndex].SetActive(true);
                        currentEnemy = enemies[activeEnemyIndex];
                    }
                    //transform.position = levels[1].transform.position;
                }
                else
                {
                    enemies[activeEnemyIndex].SetActive(true);
                    currentEnemy = enemies[activeEnemyIndex];
                }
                //current enemy is next enemy in array of enemies, activate it
                
            }
            //if all enemies have been encountered.
            else
            {
                //victory
                endGame();
            }
            if(moving)
            {
                equationText.transform.GetComponent<Text>().text = "Entering Level: " + ((activeEnemyIndex+1)/20);
            }
            else
            {
                equationText.transform.GetComponent<Text>().text = "Equation: " + equations[activeEnemyIndex].equation;
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

    void endGame()
    {
        GameManager.nextScene();
    }
    /// <summary>
    /// Runs every frame, built in function.
    /// We use it to keep an input text box and to check input.
    /// </summary>
    void OnGUI()
    {

        var style = new GUIStyle();
        style.fontSize = 20;

        GUI.Label(scoreBox, "Score: " + score.ToString(), style);
        GUI.Label(healthBox, "Health: " + health.ToString(), style);


        GUI.SetNextControlName("mytextfield");
        stringToEdit = GUI.TextField(inputBox, stringToEdit, 25);
        GUI.FocusControl("mytextfield");

        /// <summary>
        /// Check if player input was submitted, and if it matches equation solution.
        /// Enemy dies and player loses health if failed. Check for death.
        /// Enemy dies if correct.
        /// AFTER PROTOTYPE: Record correct or incorrect answer.
        /// </summary>
        if (Event.current.isKey && (Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter) && !enterStillDown)
        {
            if(!currentEnemy.GetComponent<Enemy>().dead)
            {
                enterStillDown = true;
                //equation generator input comparison
                if (stringToEdit != equations[activeEnemyIndex].solution)
                {
                    currentEnemy.SetActive(false);
                    wrongAnswer = true;
                }
                else
                {
                    playDeath = true;
                    write.PlayOneShot(wrt);
                    score += 1000;
                }
                stringToEdit = "";
            }
            else
            {
                currentEnemy.SetActive(false);
            }
        }
        if (Event.current.keyCode != KeyCode.Return && Event.current.keyCode != KeyCode.KeypadEnter)
        {
            enterStillDown = false;
        }
    }
}