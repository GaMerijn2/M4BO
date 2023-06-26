using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    private PickupSword pickupSword;
    private MainMenu mainMenu;
    public GameObject Pickuptext;

    void Start()
    {
        Pickuptext.SetActive(false);
    }
    private void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Pickuptext.SetActive(true);
            //Debug.Log(PickupSword.foundObjects);


            //Debug.Log("Touch Hitbox");
            if (Input.GetKey(KeyCode.E))
            {
                //Debug.Log(PickupSword.foundObjects);
                if (PickupSword.foundObjects >= 4)
                {
                    Debug.Log("End Scene");

                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    Cursor.lockState = CursorLockMode.Confined;

                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Pickuptext.SetActive(false);

    }
}
