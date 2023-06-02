using UnityEngine;

public class SwordFight : MonoBehaviour
{
    public int damage = 10;
    public float attackRange = 1.5f;
    public LayerMask targetLayer;
    public Transform attackPoint;
    public Animator animator;
    [SerializeField] private GameObject swordHitBox;

    private bool isAttacking = false;
    private bool swordAttack = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            isAttacking = true;
            animator.ResetTrigger("Idle");
            animator.SetTrigger("Attack");
            Invoke("PerformAttack", 0.5f); // Adjust the delay based on your animation timing
        }
        else if (isAttacking == false)
        {
            animator.ResetTrigger("Attack");
            animator.SetTrigger("Idle");
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            swordHitBox.SetActive(true);
            swordAttack = true;
            animator.ResetTrigger("SwordIdle");
            animator.SetTrigger("SwordHit");
            // Debug.Log("enable hitbox");
        }
        else if (swordAttack == true) 
        {
            swordHitBox.SetActive(false);
            animator.ResetTrigger("SwordHit");
            animator.SetTrigger("SwordIdle");
            // Debug.Log("disable hitbox");
        }




    }

    void PerformAttack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, attackRange, targetLayer);

        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Enemy")) // Replace "Enemy" with your enemy's tag
            {
                // Apply damage to the enemy
                Debug.Log("Hit enemy");

            }
            else
            {

            }
        }

        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
