using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerAnimUI : MonoBehaviour {

	public GameObject finger;

	public int changeScale = 0;

	public float speedScale;

	public float scale1, scale2;

	public float rot1, rot2;

	public float pos1, pos2;

	public float time, timeInter;

	// Update is called once per frame
	void Update () {
		if (time >= timeInter) {
			time = 0;
		} else {
			time += Time.deltaTime;
		}

		if (time >= timeInter) {
			if (changeScale == 0)
				changeScale = 1;
			else
				changeScale = 0;
		}

		if (changeScale == 0) {
			finger.transform.localScale = Vector3.MoveTowards (finger.transform.localScale, new Vector3 (finger.transform.localScale.x, scale1, finger.transform.localScale.z), Time.deltaTime * speedScale);
			finger.transform.Rotate (finger.transform.localRotation.x, finger.transform.localRotation.y, rot1);
			finger.transform.localPosition = Vector3.MoveTowards (finger.transform.localPosition, new Vector3 (finger.transform.localPosition.x + pos1, finger.transform.localPosition.y, finger.transform.localPosition.z), Time.deltaTime * speedScale);
		} else {
			finger.transform.localScale = Vector3.MoveTowards (finger.transform.localScale, new Vector3 (finger.transform.localScale.x, scale2, finger.transform.localScale.z), Time.deltaTime * speedScale);
			finger.transform.Rotate (finger.transform.localRotation.x, finger.transform.localRotation.y, rot2);
			finger.transform.localPosition = Vector3.MoveTowards (finger.transform.localPosition, new Vector3 (finger.transform.localPosition.x - pos2, finger.transform.localPosition.y, finger.transform.localPosition.z), Time.deltaTime * speedScale);
		}
	}
}
