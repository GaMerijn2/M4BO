using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSword : MonoBehaviour
{
    public GameObject swordOnPlayer;
    public GameObject Pickuptext;

    // Start is called before the first frame update
    void Start()
    {
        Pickuptext.SetActive(false);
        swordOnPlayer.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Pickuptext.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                this.gameObject.SetActive(false);

                swordOnPlayer.SetActive(true);
            }
        }
        else
        {
            Pickuptext.SetActive(false);

        }

    }
}
