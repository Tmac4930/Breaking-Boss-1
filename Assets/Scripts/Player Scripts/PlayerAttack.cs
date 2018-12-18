using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {


    [SerializeField]
    private float startTimeBtwAttack;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private Transform attackPos;
    [SerializeField]
    private LayerMask whatIsEnemy;
    [SerializeField]
    private int Damage;

    private float timeBtwAttack;
   


    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Collider2D[] enemyToDamage = Physics2D.OverlapCircleAll(attackPos.position,attackRange, whatIsEnemy);
                for (int i = 0; i < enemyToDamage.Length; i++)
                {
                    //enemyToDamage[i].GetComponent<Enemy>().TakeDamage(Damage);
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
	}
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
//First things first, overall style guide was followed pretty well. There was only one variable that i had trouble figuring out
//and that was timeBtwAttack. So either changing that name or expanding the middle wrod would be good. Also clean up some of
//empty spaces near the top including the Start function.
