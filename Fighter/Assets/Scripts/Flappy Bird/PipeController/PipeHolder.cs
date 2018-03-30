using UnityEngine;
using System.Collections;

public class PipeHolder : MonoBehaviour {

	public float speed;
	
	// Update is called once per frame
	void Update () 
	{
		if (BirdController.instance != null) 
		{
			if (BirdController.instance.flag == 1) 
			{
				Destroy (GetComponent<PipeHolder> ());
			}
		}
		_PipeMovement ();
	}

	/// <summary>
	/// Pipes the movement.
	/// </summary>
	void _PipeMovement()
	{
		Vector3 temp = transform.position;
		temp.x -= speed * Time.deltaTime;
		temp.y += speed * Time.deltaTime * 0.3f;
		transform.position = temp;
	}

	/// <summary>
	/// When the pipe hit the destroybox, the pipe will invisible.
	/// </summary>
	/// <param name="target">Target.</param>
	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.tag == "Destroy") 
		{
			transform.gameObject.SetActive (false);
		}
	}
}
