using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentSong : MonoBehaviour {
	public string sceneDelete;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	void Update() {
		string s = SceneManager.GetActiveScene ().name;
		if (s == sceneDelete) {
			Destroy (gameObject);
		}
	}
}
