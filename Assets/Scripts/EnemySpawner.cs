using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public List<GameObject> enemies;
	public GameObject blockade;
	public List<GameObject> thoughts;
	public bool first = true;


	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "hero" && first) {
			foreach (GameObject obj in enemies) {
				obj.SetActive (true);
			}
			blockade.SetActive (false);
			StartCoroutine(Think());
			first = false;
		}
	}

	public IEnumerator Think() {
		foreach (GameObject thought in thoughts) {
			thought.SetActive (true);
			yield return new WaitForSeconds (3.5f);
			thought.SetActive (false);
		}
	}
}
