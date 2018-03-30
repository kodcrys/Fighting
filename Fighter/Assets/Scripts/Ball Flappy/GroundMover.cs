using UnityEngine;
using System.Collections;

public class GroundMover : MonoBehaviour 
{
	// the ground will move to the right.
	void FixedUpdate() 
	{
		transform.position = transform.position - Vector3.right  * Time.deltaTime * 2f;
	}
}
