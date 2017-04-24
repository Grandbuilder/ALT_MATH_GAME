using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// ////////////////////Need dead boolean that equals true based off of equation and input.
    /// </summary>
    public float moveSpeed = 0.1f;
    private Vector3 playerLoc;
    [HideInInspector]
    public bool reachedPlayer;
    public bool wrongAnswer;
    [HideInInspector]
    public bool dead;
    // Use this for initialization
    void Start()
    {
        playerLoc = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead)
        {
            
            playerLoc = Camera.main.transform.position;
            //looking at player
            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(playerLoc - transform.position),
            1.1f * Time.deltaTime);

            //moving to player

            Vector3 newVec = transform.position + transform.forward * moveSpeed * Time.deltaTime;
            newVec.y = transform.position.y;
            /*
            newVec[0] = this.transform.position.x;
            newVec[1] = this.transform.position.y;
            newVec[2] = this.transform.position.z;*/
            //move towards player
            //transform.position += transform.forward * moveSpeed * Time.deltaTime;
            transform.position = newVec;

            if ((transform.position.x + 2.3 > playerLoc.x && transform.position.x - 2.3 < playerLoc.x)
                && (transform.position.z + 2.3 > playerLoc.z && transform.position.z - 2.3 < playerLoc.z))
            {
                reachedPlayer = true;
                gameObject.SetActive(false);
            }
        }
        else if(!transform.GetChild(0).GetComponent<ParticleSystem>().IsAlive())
        {
            gameObject.SetActive(false);
        }
    }
}