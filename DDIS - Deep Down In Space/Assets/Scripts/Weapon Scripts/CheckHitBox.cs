using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckHitBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        col.gameObject.CompareTag("Enemy");
        //Debug.Log(col.gameObject.name);
        //enemy check dmg
        if (col.gameObject.tag == "Enemy")
        {
            //Debug.Log("Enemy hit");
            col.gameObject.GetComponent<EnemyAiTutorial>().EnemyTakeDmg(20);
            Debug.Log(col.gameObject.GetComponent<EnemyAiTutorial>().health);
        }
        if (col.gameObject.tag == "Enemy" && col.gameObject.GetComponent<EnemyAiTutorial>().health <= 0)
        {
            GameManager.gameManager.playerHealth.HealUnit(20);
            col.gameObject.GetComponent<EnemyAiTutorial>().DestroyUnit();
            
        }


    }
}
