using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTeleport : MonoBehaviour {

	public List<Vector3> poses;
	public MazePlayerController hero;

	void OnTriggerEnter(Collider col) {
		if(col.gameObject.tag == "hero") {
			Vector3 pos;
			pos = poses[Random.Range (0, poses.Count)];
			col.gameObject.transform.position = pos;
			StartCoroutine(hero.Delay ());
		}
	}
}
