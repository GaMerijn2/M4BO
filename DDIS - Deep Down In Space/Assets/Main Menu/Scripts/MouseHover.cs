using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }

    private void OnMouseEnter()
    {
        Debug.Log("Mouse Entered");
        GetComponent<Renderer>().material.color = Color.white;
    }
    private void OnMouseExit()
    {
        Debug.Log("Mouse Exited");
        GetComponent<Renderer>().material.color = Color.black;
    }
}
