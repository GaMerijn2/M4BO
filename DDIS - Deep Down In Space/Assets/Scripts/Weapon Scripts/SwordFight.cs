using UnityEngine;

public class SwordFight : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private GameObject swordHitBox;
    private bool swordAttack = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !swordAttack)
        {
            swordHitBox.SetActive(true);
            swordAttack = true;
            animator.SetBool("Attack", true);
             Debug.Log("Attack true");
        }
        else if (swordAttack == true) 
        {
            swordHitBox.SetActive(false);
            swordAttack = false;
            animator.SetBool("Attack", false);
             Debug.Log("Attack false");
        }
    }
}
