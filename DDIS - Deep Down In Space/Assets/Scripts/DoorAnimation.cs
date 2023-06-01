using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    Animator m_Animator;
    public GameObject range;
    public GameObject player;
    Vector3 distance;

    [SerializeField] private string dooropen = "doorOpen";
    [SerializeField] private string doorclose = "doorClose";


    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        distance = this.transform.position - player.transform.position;
        if (distance.magnitude <= 2)
        {
            Debug.Log("In Trigger");
            m_Animator.Play(dooropen, 0, 0.0f);
        }
        else
        {
            Debug.Log("out Trigger");
            m_Animator.Play(doorclose, 0, 0.0f);
        }
    }
}
