using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// ////////////////////Need dead boolean that equals true based off of equation and input.
    /// </summary>
    public float moveSpeed = 0.01f;
    private Vector3 playerLoc;
    [HideInInspector]
    public bool reachedPlayer;
    public bool wrongAnswer;
    // Use this for initialization
    void Start()
    {
        playerLoc = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerLoc = Camera.main.transform.position;
        //looking at player
        transform.rotation = Quaternion.Slerp(transform.rotation,
    Quaternion.LookRotation(playerLoc - transform.position),
    .2f * Time.deltaTime);

        //moving to player
        Vector3 newVec = this.transform.position;
        newVec[0] = this.transform.position.x;
        newVec[1] = this.transform.position.y;
        newVec[2] = this.transform.position.z;
        //move towards player
        if (newVec[0] > playerLoc.x)
        {
            newVec[0] -= moveSpeed;
        }
        else if (newVec[0] < playerLoc.x)
        {
            newVec[0] += moveSpeed;
        }
        if (newVec[2] > playerLoc.z)
        {
            newVec[2] -= moveSpeed;
        }
        else if (newVec[2] < playerLoc.z)
        {
            newVec[2] += moveSpeed;
        }
        this.transform.position = newVec;

        if ((transform.position.x + 2.3 > playerLoc.x && transform.position.x - 2.3 < playerLoc.x)
            && (transform.position.z + 2.3 > playerLoc.z && transform.position.z - 2.3 < playerLoc.z))
        {
            reachedPlayer = true;
            gameObject.SetActive(false);
        }
    }
}