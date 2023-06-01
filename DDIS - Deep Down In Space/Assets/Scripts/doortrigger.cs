using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doortrigger : MonoBehaviour
{
   public Animator m_Animator;
  private string dooropen = "doorOpen";
    private string doorclose = "doorClose";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("In Trigger");
            m_Animator.Play(dooropen, 0, 0.0f);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("out Trigger");
            m_Animator.Play(doorclose, 0, 0.0f);
        }
    }
}
