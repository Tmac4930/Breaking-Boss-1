using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {
    [SerializeField]
    private float scrollSpeed;

    Vector3 startPOS;
	// Use this for initialization
	void Start ()
    {
        startPOS = transform.position;

	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate((new Vector3(-1, 0, 0)) * scrollSpeed * Time.deltaTime);

        if(transform.position.x <-864.5)
        {
            transform.position = startPOS;
        }
	}
}
