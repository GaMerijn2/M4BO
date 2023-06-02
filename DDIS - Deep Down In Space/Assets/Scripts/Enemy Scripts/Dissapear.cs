using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissapear : MonoBehaviour {

 public int lifeTime = 3;
 public void Start()
 {
     StartCoroutine(WaitThenDie());
 }
 IEnumerator WaitThenDie()
 {
     yield return new WaitForSeconds(lifeTime);
     Destroy(gameObject);
 }
}
