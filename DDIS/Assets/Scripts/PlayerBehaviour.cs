using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public TextMeshProUGUI healthDisplay;

    //public GameObject maincamera;
    //public GameObject aimcamera;



    // Update is called once per frame
    void Update()
    {
        float currentHealth = GameManager.gameManager.playerHealth.Health;
        float currentMaxHealth = GameManager.gameManager.playerHealth.MaxHealth;

        Cursor.lockState = CursorLockMode.Locked;


        if (Input.GetKey(KeyCode.LeftControl))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }


        /*
        if(Input.GetKey(KeyCode.K)) 
        {
            maincamera.SetActive(false);
            aimcamera.SetActive(true);
        }
        else
        {
            maincamera.SetActive(true);
            aimcamera.SetActive(false);
        }
        */





        if (healthDisplay != null)
        {
            healthDisplay.SetText(currentHealth + " / " + currentMaxHealth);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            PlayerTakeDmg(20);
            Debug.Log("PlayerHealth: " + GameManager.gameManager.playerHealth.Health.ToString());
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            PlayerHeal(25);
            Debug.Log("PlayerHealth: " + GameManager.gameManager.playerHealth.Health.ToString());
        }




        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);



            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
    private void PlayerTakeDmg(int dmg)
    {
        GameManager.gameManager.playerHealth.DmgUnit(dmg);
    }
    private void PlayerHeal(int healing)
    {
        GameManager.gameManager.playerHealth.HealUnit(healing);
    }
    public void DestroyUnit()
    {
        Destroy(gameObject);

    }

}
