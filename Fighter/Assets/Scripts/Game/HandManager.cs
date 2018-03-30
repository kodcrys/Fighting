using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour {

	enum AnimationState {none, twoHand, head}
	[SerializeField]
	AnimationState whoAnim;

	[SerializeField]
	GameObject Twohand;

	[SerializeField]
	float time, timeInter;

	[SerializeField]
	float speedScale, speedRot, speedPot;

	[SerializeField]
	int changeAnim;

	[SerializeField]
	bool changeRot, changePot;

	void Start(){
		changeRot = false;
		changePot = false;
	}

	void Update(){
		if (time >= timeInter) {
			time = 0;
		} else {
			time += Time.deltaTime;
		}

		switch (whoAnim) {
		case AnimationState.twoHand:
			HandMoveAnim ();
			break;
		case AnimationState.head:
			break;
		}
	}

	void HandMoveAnim(){
		if (time >= timeInter) {
			if (changeAnim == 0) {
				changeAnim = 1;
				if (!changePot)
					changePot = true;
				else
					changePot = false;
			} else {
				changeAnim = 0;
				if (!changeRot)
					changeRot = true;
				else
					changeRot = false;
			}
		}
		if (changeAnim == 0) {
			Twohand.transform.localScale = Vector3.MoveTowards (Twohand.transform.localScale, new Vector3 (1, 1, Twohand.transform.localScale.z), Time.deltaTime * speedScale);
			if (!changeRot)
				Twohand.transform.Rotate (Twohand.transform.localRotation.x, Twohand.transform.localRotation.y, -speedRot * Time.deltaTime);
			else
				Twohand.transform.Rotate (Twohand.transform.localRotation.x, Twohand.transform.localRotation.y, speedRot * Time.deltaTime);

			if (!changePot)
				Twohand.transform.localPosition = Vector3.MoveTowards (Twohand.transform.localPosition, new Vector3 (Twohand.transform.localPosition.x + 1.5f, Twohand.transform.localPosition.y, Twohand.transform.localPosition.z), Time.deltaTime * speedPot);
			else
				Twohand.transform.localPosition = Vector3.MoveTowards (Twohand.transform.localPosition, new Vector3 (Twohand.transform.localPosition.x - 1.5f, Twohand.transform.localPosition.y, Twohand.transform.localPosition.z), Time.deltaTime * speedPot);
		} else if (changeAnim == 1) {
			Twohand.transform.localScale = Vector3.MoveTowards (Twohand.transform.localScale, new Vector3 (1.08f, 1.08f, Twohand.transform.localScale.z), Time.deltaTime * speedScale);
			if (!changeRot)
				Twohand.transform.Rotate (Twohand.transform.localRotation.x, Twohand.transform.localRotation.y, speedRot * Time.deltaTime);
			else
				Twohand.transform.Rotate (Twohand.transform.localRotation.x, Twohand.transform.localRotation.y, -speedRot * Time.deltaTime);
			if (!changePot)
				Twohand.transform.localPosition = Vector3.MoveTowards (Twohand.transform.localPosition, new Vector3 (Twohand.transform.localPosition.x + 1.5f, Twohand.transform.localPosition.y, Twohand.transform.localPosition.z), Time.deltaTime * speedPot);
			else
				Twohand.transform.localPosition = Vector3.MoveTowards (Twohand.transform.localPosition, new Vector3 (Twohand.transform.localPosition.x - 1.5f, Twohand.transform.localPosition.y, Twohand.transform.localPosition.z), Time.deltaTime * speedPot);
		}
	}
}
