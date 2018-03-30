using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEffectOff : MonoBehaviour {

	float time = 0;
	public bool isRunFinish;

	[SerializeField]
	bool isSmokeLarge;
	[HideInInspector]
	public CointainData data;

	// Use this for initialization
	void OnEnable () {
		time = 0;
		isRunFinish = false;
		//if (isSmokeLarge) {
		//	RewardManager.instance.DisableBtnGoldDia ();
		//}
	}
	
	void Update() {
		time += Time.deltaTime;
		if (time >= 1.5f) {
			gameObject.SetActive (false);
			isRunFinish = true;
			time = 0;
			if (isSmokeLarge) {
				if (data.dataChar != null)
					if(CointainData.isRewardDailyQuest == false)
						RewardManager.instance.ShowBtnGold (true);
				else if(data.dataItem != null)
					RewardManager.instance.ShowBtnGold (false);
			}
		}
	}
}
