using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerAnim : BaseFinger {

	enum FingerState {none, Idel, First, Atk, Down, Death}
	[SerializeField]
	FingerState fingerAction = FingerState.none;


	void Start(){
		Time.timeScale = 1;
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		switch (fingerAction) 
		{
		case FingerState.none:
			fingerAction = FingerState.Idel;
			break;
		case FingerState.Idel:
			DoIdel ();
			break;
		case FingerState.First:
			DoFirstAtk ();
			break;
		}

		if (!doingSomething) 
		{
			fingerAction = FingerState.Idel;
		} 
		else 
		{
			if (!firstAtk)
				fingerAction = FingerState.First;
		}
	}

	public override void DoIdel()
	{
		ChangeStateAni (FingerState.Idel);

		finger.SetActive (true);
		fingerAtk.SetActive (false);

		firstAtk = false;

			if (time >= timeInter) 
			{
				time = 0;
			} 
			
			time += Time.deltaTime;

			if (time >= timeInter) 
			{
				if (changeScale == 0)
					changeScale = 1;
				else
					changeScale = 0;
			}

			if (changeScale == 0) 
			{
				finger.transform.localScale = Vector3.MoveTowards (finger.transform.localScale, new Vector3 (finger.transform.localScale.x, scale1, finger.transform.localScale.z), Time.deltaTime * speedScale);
				finger.transform.Rotate (finger.transform.localRotation.x, finger.transform.localRotation.y, rot1);
				finger.transform.localPosition = Vector3.MoveTowards (finger.transform.localPosition, new Vector3 (finger.transform.localPosition.x + pos1, finger.transform.localPosition.y, finger.transform.localPosition.z), Time.deltaTime * speedScale);
			} 
			else 
			{
				finger.transform.localScale = Vector3.MoveTowards (finger.transform.localScale, new Vector3 (finger.transform.localScale.x, scale2, finger.transform.localScale.z), Time.deltaTime * speedScale);
				finger.transform.Rotate (finger.transform.localRotation.x, finger.transform.localRotation.y, rot2);
				finger.transform.localPosition = Vector3.MoveTowards (finger.transform.localPosition, new Vector3 (finger.transform.localPosition.x - pos2, finger.transform.localPosition.y, finger.transform.localPosition.z), Time.deltaTime * speedScale);
			}

	}

	public override void DoFirstAtk()
	{
		ChangeStateAni (FingerState.Atk);

		firstAtk = true;
		finger.SetActive (false);
		fingerAtk.SetActive (true);
	}

	public void ClickAtk(){
		doingSomething = true;
	}

	public void UnClickAtk(){
		doingSomething = false;
	}

	public void ChangeItemsAI() {

		if (GamePlayController.hatAI != null) {
			// hat idle
			hat.sprite = GamePlayController.hatAI.avatar;
			hat.gameObject.SetActive (true);

/*			// hat AtkDown
			hatAtkDownSpr.sprite = GameplayBase.hatAI.avatar;
			hatAtkDownSpr.gameObject.SetActive (true);*/
		}

		if (GamePlayController.amorAI != null) {
			amor.sprite = GamePlayController.amorAI.avatar;
			amor.gameObject.SetActive (true);
		}

		if (GamePlayController.wpAI != null) {
			weapon.sprite = GamePlayController.wpAI.avatar;
			weapon.gameObject.SetActive (true);
		}
	}

	void ChangeStateAni (FingerState state) {
		switch (state) {
		case FingerState.Idel:
			skinIdle.SetActive (true);
			skinAtkDown.SetActive (false);
			break;
		case FingerState.Atk:
			skinIdle.SetActive (false);
			skinAtkDown.SetActive (true);
			break;
		}
	}
}
