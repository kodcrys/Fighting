using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBird : MonoBehaviour {
	
	private GameObject Player;

	[SerializeField]
	float maxTime, minTime, speed;

	float randomTime;
	bool checkIdle;

	// Use this for initialization
	void OnEnable () {
		Player = GameObject.FindGameObjectWithTag ("Player");
		randomTime = Random.Range (maxTime, minTime);
		checkIdle = false;
	}
		
	// Update is called once per frame
	void Update () 
	{
		PipeFollowBird ();
	}

	/// <summary>
	/// If the position of the pipe == position of the bird, the pipe will move follow the bird in ranndom time.
	/// </summary>
	void PipeFollowBird ()
	{
		// Check the position of the pipe and the bird.
		if (transform.position.x <= Player.transform.position.x) 
		{
			checkIdle = true;
		}

		// Countdown the time the pipe follow the bird.
		if (checkIdle)
			randomTime -= Time.deltaTime;

		if (randomTime >= 0 && checkIdle) 
		{
			Vector3 temp = transform.position;
			temp.x += speed * Time.deltaTime;
			temp.y -= speed * Time.deltaTime * 0.3f;
			transform.position = temp;
		} 
		else
			checkIdle = false;
	}
}
