using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTeleport : MonoBehaviour {

	public List<Vector3> poses;

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "hero") {
			Vector3 pos;
			pos = poses[Random.Range (0, poses.Count)];
			pos.z = -5;
			col.gameObject.transform.position = pos;
			StartCoroutine(col.gameObject.GetComponent<MazePlayerController> ().Delay ());
		}
	}
}
