using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnim : MonoBehaviour {

	FingerLeftControl left;
	FingerRightControl right;

	void Start(){
		left = gameObject.GetComponentInParent<FingerLeftControl> ();
		right = gameObject.GetComponentInParent<FingerRightControl> ();
	}

	public void DestroyObj(){
		Destroy (gameObject);
	}

	public void OffAnim(){
		gameObject.GetComponent<Animator> ().enabled = false;
		if (right != null)
			right.fingerAminChanger = 3;
		else if (left != null)
			left.fingerAminChanger = 3;
		
	}
}
