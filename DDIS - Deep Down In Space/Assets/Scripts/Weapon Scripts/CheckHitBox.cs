using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHitBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        col.gameObject.CompareTag("Enemy");

        if (col.gameObject.name == "Enemy")
        {
            Debug.Log(col.gameObject.name);
            col.gameObject.GetComponent<EnemyAiTutorial>().EnemyTakeDmg(20);
            Debug.Log(col.gameObject.GetComponent<EnemyAiTutorial>().health);
        }
        if (col.gameObject.GetComponent<EnemyAiTutorial>().health <= 0)
        {
            col.gameObject.GetComponent<EnemyAiTutorial>().DestroyUnit();
        }
    }
}
