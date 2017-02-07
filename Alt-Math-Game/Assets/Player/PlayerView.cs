using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour {
    public int lookSpeed = 5;
    public GameObject enemy;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //look towards target
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(enemy.transform.position - transform.position), lookSpeed * Time.deltaTime);
    }
}
