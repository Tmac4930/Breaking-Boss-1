﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private int health;
   

    
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
      if(health <=0)
        {
            Destroy(gameObject);
        }  
	}
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("damage taken !");
        
    }
}
//it is not specifyed whose health is being talked about here, so that can be improved upon. Also cleaning up empty spaces to
//to condense space would look better.
