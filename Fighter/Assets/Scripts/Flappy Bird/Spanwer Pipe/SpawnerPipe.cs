using UnityEngine;
using System.Collections;

public class SpawnerPipe : MonoBehaviour {

	// Use this for initialization
	void Start () {
		timeCountdown = 0f;
	}

	float timeCountdown;

	void Update ()
	{
		if (BirdController.touchOnScreen)
			timeCountdown += Time.deltaTime;
		
		if (timeCountdown >= 1.7f) 
		{
			timeCountdown = 0f;
			Spawner ();
		}
	}
	/// <summary>
	/// Spawner the pipes.
	/// </summary>
	public void  Spawner()
	{
		// The number of pipe depend on the score. Every 10 score will unlock 1 new pipes
		int numberPipe = Mathf.RoundToInt(BirdController.instance.score / 10);

		if (numberPipe > 5)
			numberPipe = 5;

		// Random pipe on the number of pipe you got.
		int randomPipe = Random.Range (0, numberPipe);

		// Random the position for the pipe depend on the number of pipe.
		Vector3 temp = Vector3.zero;
		temp.y = Random.Range (-0.25f, PoolManager.Intance.listRandomPos[randomPipe]);

		// Create the pipe.
		PoolManager.Intance.lstPool [randomPipe].getindex ();
		PoolManager.Intance.lstPool [randomPipe].GetPoolObject ().transform.position = transform.position + temp;
		PoolManager.Intance.lstPool [randomPipe].GetPoolObject ().transform.rotation = transform.rotation;
		PoolManager.Intance.lstPool [randomPipe].GetPoolObject ().SetActive (true);

	}
}
