
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAiTutorial : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //health
    //public int maxHealth = 100;
    public int health = 100;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public bool playerHit;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, playerIsAlive;

    //HealthBar
    public HealthBar healthBar;

    //attacks
    public float attackDamage = 10f;
    public float attackCooldown = 1f;

    public Animator anim;
    public Animator playerAnim;

    public AudioSource attackSound;
    //private PlayerBehaviour playerBehaviour;
     public PlayerBehaviour Name = new PlayerBehaviour();

    private void Start()
    {
        //maxHealth = 100;
        health = 100;
        playerHit = false;
    }
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        Name = player.GetComponent<PlayerBehaviour>();

    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            playerIsAlive = false;
        }

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange && !playerIsAlive)
        {
            ChasePlayer();
            transform.LookAt(player);
        }
        if (playerInAttackRange && playerInSightRange && !playerIsAlive)
        {
            AttackPlayer();
            transform.LookAt(player);
        }
        float movespeed = agent.velocity.magnitude;
        anim.SetFloat("Speed", movespeed);
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
            anim.SetBool("Attacks", true);
            attackSound.PlayOneShot(attackSound.clip);
            Debug.Log("set anim");

            foreach (Collider collider in hitColliders)
            {
                if (collider.CompareTag("Player"))
                {
                    //playerAnim.SetBool("Hit", true);
                    // Debug.Log("Hit Animation");
                    Name.PlayerHitAnim();
                    collider.gameObject.GetComponent<PlayerBehaviour>().PlayerTakeDmg(5);
                }
                if (GameManager.gameManager.playerHealth.Health <= 0)
                {
                    playerAnim.SetBool("Death", true);
                    SceneManager.LoadScene("DeathMenu");
                    Cursor.lockState = CursorLockMode.None;
                    Invoke(nameof(DeathScreen), 1);
                }
            }

            alreadyAttacked = true;

            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        Debug.Log("reset anim");
        anim.SetBool("Attacks", false);

        alreadyAttacked = false;
        //playerAnim.SetBool("Hit", false);

    }
    private void DeathScreen()
    {
        Debug.Log("Deadscreen");
        SceneManager.LoadScene("DeathMenu");
    }

    public void EnemyTakeDmg(int dmg)
    {
        //GameManager.gameManager.enemyHealth.DmgUnit(dmg);
        health -= dmg;
        healthBar.SetHealth(health);
    }
    private void EnemyHeal(int healing)
    {
        GameManager.gameManager.enemyHealth.HealUnit(healing);
        healthBar.SetHealth(health);

    }
    public void DestroyUnit()
    {
        Destroy(gameObject);

    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
