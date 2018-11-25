using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private static int coinCount;

   private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

   private void OnTriggerEnter2D(Collider2D collision)
    {
    if (collision.gameObject.CompareTag("Player"))
        {
            debug.log("coinCount:" + coinCount);
            coinCount++;
            Destroy(gameObject, audioSource.clip.length);
            spriteRenderer.enabled = false;
            boxCollider2D.enabled = false;
            audioSource.Play();
        }

    }
}
 