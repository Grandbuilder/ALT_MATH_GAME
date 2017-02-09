using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    //Still need:
    //set all enemies to inactive on first update except first enemy
    //Player moves to each next enemy
    //Game over when player reaches 0 hp or end of enemies
    //equation generator
    //equation generator input comparison
    //if input is correct, enemy dies, else, enemy dies and player takes damage

    public int lookSpeed = 5;
    private GameObject currentEnemy;
    private int numEnemies;
    private GameObject[] enemies;
    private int activeEnemyIndex;
    public int health = 3;
	// Use this for initialization
	void Start ()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        numEnemies = enemies.Length;
        activeEnemyIndex = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //updates damage of player, enemy alive or dead, activates next enemy if dead. checks victory
        updateActiveEnemy();        
        //look towards target
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation(currentEnemy.transform.position - transform.position), 
            lookSpeed * Time.deltaTime);
    }

    void updateActiveEnemy()
    {
        //if current enemy is no longer active
        if(!currentEnemy.activeSelf)
        {
            //increment enemies index
            activeEnemyIndex++;
            //if we haven't beat all enemies
            if(activeEnemyIndex < numEnemies)
            {
                //current enemy is next enemy in array of enemies
                currentEnemy = enemies[activeEnemyIndex];
            }
            else
            {
                //victory
            }
        }
        else
        {
            //check if enemy is within 1 unit distance of player
            if((currentEnemy.transform.position.x < (transform.position.x+1) && currentEnemy.transform.position.x > (transform.position.x - 1))
                && (currentEnemy.transform.position.z < (transform.position.z + 1) && currentEnemy.transform.position.z > (transform.position.z - 1)))
            {
                currentEnemy.SetActive(false);
                health--;
                //monster dies and player takes 1 damage
            }
        }
    }
}
