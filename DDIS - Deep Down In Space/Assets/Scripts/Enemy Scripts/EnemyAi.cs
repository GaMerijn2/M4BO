using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;


    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    public LayerMask whatIsGround, whatIsPlayer, whatIsEnemy;

    public float health;

    public bool playerIsAlive;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, enemyInSightRange, runFromPlayer;

    [SerializeField] private float speed = 5f;
    public float BulletSpeed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hoi");
        playerIsAlive = true;

    }

    // Update is called once per frame
    void Update()
    {
        PlayerExists();
        if (playerIsAlive)
        {
            Vector3 distance = player.transform.position - this.transform.position;
            Vector3 direction = distance.normalized;
            if (playerInSightRange && !playerInAttackRange)
            {
                transform.position += direction * speed * Time.deltaTime;
                transform.LookAt(player.transform.position);
            }
            else if (distance.magnitude < 20)
            {
                transform.position -= direction * speed * Time.deltaTime;
                transform.LookAt(player.transform.position);
            }

/*
            Vector3 distanceEnemy = enemy.transform.position - this.transform.position;
            Vector3 directionEnemy = distanceEnemy.normalized;
            if (distanceEnemy.magnitude < 10)
            {
                transform.position -= direction * speed * Time.deltaTime;
            }
*/


            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            runFromPlayer = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            enemyInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsEnemy);


            if (playerInAttackRange && playerInSightRange)
            {
                AttackPlayer();
            }
            AttackPlayer();
        }

    }
    private void AttackPlayer()
    {

        if (!alreadyAttacked)
        {
            ///Attack code here
            transform.LookAt(player.transform.position + new Vector3(0, 0, 0));
            Rigidbody rb = Instantiate(projectile, transform.position, transform.rotation).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * BulletSpeed, ForceMode.Impulse);
            //rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code
              
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void PlayerExists()
    {
        if (player == null) 
        { 
            playerIsAlive = false;
        }

    }

}
