using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour {
	float time;

	// The effect only appear in 0.5s
	void Update () 
	{
		time += Time.deltaTime;
		if (time > 0.5f) 
		{
			gameObject.SetActive (false);
			time = 0;
		}
	}
}
