using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UIElements;

public class PlayerBehaviour : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    //movement stuff
    public float speed = 6f;
    public float rotSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    //movement rotation
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //groundcheck stuff
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;

    //displays HP
    public TextMeshProUGUI healthDisplay;

    //camers for ADS, not working yet
    public GameObject maincamera;
    public GameObject cincamera;

    public HealthBar healthBar;

    public bool IsActive;

    public Animator animator;
    private Vector2 moveSpeed;

    // Update is called once per frame
    private void Start()
    {
        healthBar.SetMaxHealth(GameManager.gameManager.playerHealth.MaxHealth);

    }
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Hp();
        PlayerMovement();
        MouseMovement();
        CamMovement();

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            Cursor.lockState = CursorLockMode.None;

        }
    }
    private void Hp()
    {
        float currentHealth = GameManager.gameManager.playerHealth.Health;
        float currentMaxHealth = GameManager.gameManager.playerHealth.MaxHealth;

        if (healthDisplay != null)
        {
            healthDisplay.SetText(currentHealth + " / " + currentMaxHealth);
        }
        if (GameManager.gameManager.playerHealth.Health == null)
        {

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
    }
    public void PlayerTakeDmg(int dmg)
    {
        GameManager.gameManager.playerHealth.DmgUnit(dmg);

        healthBar.SetHealth(GameManager.gameManager.playerHealth.Health);
    }
    private void PlayerHeal(int healing)
    {
        GameManager.gameManager.playerHealth.HealUnit(healing);
        healthBar.SetHealth(GameManager.gameManager.playerHealth.Health);

    }
    public void DestroyUnit()
    {
        Destroy(gameObject);

    }
    private void PlayerMovement()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed * 2;
        } else if (Input.GetKeyUp(KeyCode.LeftShift)) 
        {
            speed = speed / 2;
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

            moveSpeed = new Vector2(this.velocity.x, this.velocity.z);
            Debug.Log(moveSpeed);
            if (moveSpeed.magnitude > 0.1f)
            {
                animator.SetTrigger("Walk");
            }
            else if (moveSpeed.magnitude == 0f)
            {
                animator.SetTrigger("Idle");
            }
            else if (moveSpeed.magnitude > 0.2f)
            {
                animator.SetTrigger("Run");

            }
        }
    }
    private void MouseMovement()
    {
        /*
        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCentre);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
        }

        
        if (Input.GetMouseButton(1))
        {
            Debug.Log("RMB");
            transform.rotation = cam.transform.rotation;
            maincamera.SetActive(false);
            aimcamera.SetActive(true);


            float mouseXValue = Input.GetAxis("Mouse X");
            float mouseYValue = Input.GetAxis("Mouse Y");
            transform.rotation *= Quaternion.Euler(mouseXValue * rotSpeed * Vector3.up);
        }
        */
        /*
        if (Input.GetMouseButton(2))
        {
            cincamera.SetActive(true);
            maincamera.SetActive(false);
        }
        else
        {
            maincamera.SetActive(true);
            cincamera.SetActive(false);
        }
        */



    }
    private void CamMovement()
    {
        
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            cincamera.SetActive(true);
            maincamera.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            cincamera.SetActive(false);
            maincamera.SetActive(true);
        }
    }
    

    public void LockCursor(bool IsActive)
    {
        if (IsActive)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (IsActive == false) 
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}