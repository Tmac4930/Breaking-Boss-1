using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

private void OnTriggerEnter2D(Collider2D Collision)
    {
        if (Collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Entered Hazard");
            PlayerCharacter player = Collision.GetComponent<PlayerCharacter>();
            player.SetCurrentCheckPoint(this);
        }
    }
}
