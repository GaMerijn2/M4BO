using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class GrabScript : MonoBehaviour
{
    public GameObject grabbedObject;
    public float grabRange= 2;
    public float speed = 5;
    public GameObject player;

    public bool inGrabRange;
    public LayerMask whatIsGrabObject;
    private void Update()
    {
        inGrabRange = Physics.CheckSphere(player.transform.position, grabRange, whatIsGrabObject);
        Vector3 distance = player.transform.position - grabbedObject.transform.position;
        Vector3 direction = distance.normalized;

       // grabbedObject.transform.position = player.transform.position + player.transform.right * 2;
        grabbedObject.transform.position += direction * speed * Time.deltaTime;
        /*
        if (distance.magnitude <= grabRange)
        {
            Debug.Log("grabbed");

        }
        else
        {
            //Debug.Log("Nect to player");
            grabbedObject.transform.position = player.transform.position +player.transform.right *2;
        }
        */
    }
}
