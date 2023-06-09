using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager { get; private set; }

    public UnitHealth playerHealth = new UnitHealth(200, 200);
    public UnitHealth enemyHealth = new UnitHealth(55, 55);

    // Start is called before the first frame update
    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        }   
        else
        {
            gameManager = this;
        }
    }
}
