using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PickupSword : MonoBehaviour
{
    public GameObject swordOnPlayer;
    public GameObject Hitbox;

    public GameObject Pickuptext;
    public static float foundObjects;
    public ParticleSystem particle;

    public AudioSource PickupSound;

    // Start is called before the first frame update
    void Start()
    {
        Pickuptext.SetActive(false);
        swordOnPlayer.SetActive(false);

        foundObjects = 0;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Pickuptext.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                particle.Play();
                PickupSound.Play();
                this.gameObject.SetActive(false);

                swordOnPlayer.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Pickuptext.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.G)) 
        {
            foundObjects += 1;
           // Debug.Log(foundObjects);
        }
    }
}
