using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject projectile;
    public bool alreadyAttacked;
    public float timeBetweenAttacks;

    public Animator animator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && alreadyAttacked == false)
        {
            //animator.ResetTrigger("Idle");
            //animator.SetTrigger("Attack");
            animator.Play("Sword");

            Debug.Log("Shoot");
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = false;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        animator.ResetTrigger("Attack");
        animator.SetTrigger("Idle");
        alreadyAttacked = false;
    }
}
