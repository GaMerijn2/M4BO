using System.Collections;
using System.Collections.Generic;
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
            col.gameObject.GetComponent<EnemyAiTutorial>().EnemyTakeDmg(50);
            Debug.Log(col.gameObject.GetComponent<EnemyAiTutorial>().health);
        }
        if (col.gameObject.GetComponent<EnemyAiTutorial>().health <= 0)
        {
            col.gameObject.GetComponent<EnemyAiTutorial>().DestroyUnit();
            GameManager.gameManager.playerHealth.HealUnit(20);
        }


    }
}
