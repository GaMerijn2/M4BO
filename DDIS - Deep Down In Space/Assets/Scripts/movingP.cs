using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class movingP : MonoBehaviour
{
    public Transform platform;
    public Transform startPoint;
    public Transform endPoint;
    public float speed;
    public GameObject player;

    int direction = 1;

    private void Update()
    {
        Vector2 target = currentMovementTarget();

        platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime);

        float distance = (target - (Vector2)platform.position).magnitude;

        if (distance <= 0.1f)
        {
            direction *=-1;
        }
        //Debug.Log(distance);

    }

    Vector3 currentMovementTarget()
    {
        if (direction == 1)
        {
            return startPoint.position;
        }
        else
        {
            return endPoint.position;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Touch platfrom");
        collision.collider.transform.SetParent(platform);
       // player.transform.position = platform.transform.position;
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("No Touch platfrom");
        //collision.collider.transform.SetParent(null);
    }
}