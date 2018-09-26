using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {
    [SerializeField] // expose to the engine without making class/ veriable public
    private int lives = 3;

    [SerializeField]
    private string name = "Mario";

    [SerializeField]
    private float jumpHeight = 5, speed= 5;

    private bool haskey;

    private bool isOnGround;

    
	
    // Use this for initialization
	void Start ()
    {
        //  Debug.Log("Hello");
        // transform.Translate(0, -1, 5);// using physics, so dont use trainsform. translate.
       
	}
	
	// Update is called once per frame
	void Update ()
    {
	//Horizontal	
	}

    void Move()
    {

    }
}
