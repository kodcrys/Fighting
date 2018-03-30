using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {

	public int indexPipe;
	bool movePos2;
	public Transform pos1, pos2, pos;

	
	// Update is called once per frame
	void Update () {
		if (indexPipe == 0) {
			movePos2 = false; 
			transform.position = pos.position;
		}
		if (indexPipe == 1) {
						
			if (transform.position == pos1.position) 
			{
				movePos2 = true; 
			}

			if (transform.position == pos2.position) 
			{
				movePos2 = false;
			}
								
			if (movePos2) 
				transform.position = Vector3.MoveTowards (transform.position, pos2.position, Time.deltaTime);
			else
				transform.position = Vector3.MoveTowards (transform.position, pos1.position, Time.deltaTime);
		}
	}
}
