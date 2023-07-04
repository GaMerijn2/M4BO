using UnityEngine;

public class SwordFight : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private GameObject swordHitBox;
    private bool swordAttack = false;
    public AudioSource audioSource;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !swordAttack)
        {
            swordHitBox.SetActive(true);
            audioSource.PlayOneShot(audioSource.clip);

            swordAttack = true;
            animator.SetBool("Attack", true);
             Debug.Log("Attack true");
            Invoke(nameof(ResetAttack), 0.5f);
        }

    }
    private void ResetAttack()
    {
        swordHitBox.SetActive(false);
        swordAttack = false;
        animator.SetBool("Attack", false);
        Debug.Log("Attack false");
    }
}
