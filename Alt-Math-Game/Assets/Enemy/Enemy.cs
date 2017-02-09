using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Vector3 playerLoc = Camera.main.transform.position;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newVec = this.transform.position;
        newVec[0] = this.transform.position.x;
        newVec[1] = this.transform.position.y;
        newVec[2] = this.transform.position.z;
        //move towards player
        if (newVec[0] > playerLoc.x)
        {
            newVec[0]--;
        }
        else
        {
            newVec[0]++;
        }
        if (newVec[2] > playerLoc.z)
        {
            newVec[2]--;
        }
        else
        {
            newVec[2]++;
        }
        this.transform.position = newVec;
    }
}