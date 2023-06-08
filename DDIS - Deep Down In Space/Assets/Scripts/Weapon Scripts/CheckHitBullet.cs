using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckHitBullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (this.gameObject.tag == "EnemyBullet")
        {
            col.gameObject.CompareTag("Player");
            //player check dmg
            if (col.gameObject.tag == "Player")
            {
                Debug.Log("Player hit");
                col.gameObject.GetComponent<PlayerBehaviour>().PlayerTakeDmg(5);
            }
            if (GameManager.gameManager.playerHealth.Health <= 0)
            {
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
        if (this.gameObject.tag == "PlayerBullet")
        {
            if (col.gameObject.tag == "Enemy")
            {
                Debug.Log("Enemy hit");
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
}
