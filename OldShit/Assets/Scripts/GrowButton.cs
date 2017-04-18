using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowButton : MonoBehaviour {

	private bool grow;
	private float xvel, yvel;

	public void Start()
	{
		grow = false;
		xvel = yvel = 0.0f;
	}

	public void Update()
	{
		if(grow)
		{
			GetComponent<RectTransform>().localScale = Vector3.Lerp(GetComponent<Transform>().localScale, Vector3.one * 1.05f, 12.0f * Time.fixedDeltaTime);
		} else
		{
			GetComponent<RectTransform>().localScale = Vector3.Lerp(GetComponent<Transform>().localScale, Vector3.one, 12.0f * Time.fixedDeltaTime);
		}
	}


	public void OnMouseOver()
	{
		grow = true;
	}

	public void OnMouseExit()
	{
		grow = false;
	}
}
