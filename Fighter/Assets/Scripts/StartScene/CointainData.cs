using UnityEngine;
using System.Collections;

public class CointainData : MonoBehaviour {

	public DataCharacter dataChar;
	public DataItems dataItem;
	public Sprite dataMap;

	public DataQuests quest;

	[SerializeField]
	bool isX1Gatcha;
	[SerializeField]
	GameObject smokeEffect;
	[SerializeField]
	CharacterEquipmentManager charEquipManager;

	SmokeEffectOff smokeEffectOff;

	public static bool isRewardDailyQuest;

	bool isCheck;
	//changeChar
	void OnEnable() {
		isCheck = true;
	
		if (smokeEffect != null)
			smokeEffectOff = smokeEffect.GetComponent<SmokeEffectOff> ();
		if (isX1Gatcha)
			StartCoroutine (DetectShowEffect ());
	}

	IEnumerator DetectShowEffect() {
		while (true) {
			yield return new WaitForSeconds (0.6f);
			if (isCheck) {
				if (dataChar != null) {
					if (dataChar.isOwned) {
						smokeEffect.transform.position = transform.position;
						smokeEffect.SetActive (true);
						charEquipManager.ChangeRewardX1 ();
						smokeEffectOff.data = this;
					} else {
						if(isRewardDailyQuest == false)
							RewardManager.instance.ShowBtnGold (true);
						dataChar.isOwned = true;
					}
					isCheck = false;
				}
				if (dataItem != null) {
					if (dataItem.isOwned) {
						smokeEffect.transform.position = transform.position;
						smokeEffect.SetActive (true);
						charEquipManager.ChangeRewardX1Equipment ();
						smokeEffectOff.data = this;
					} else {
						RewardManager.instance.ShowBtnGold (false);
						dataItem.isOwned = true;
					}
					isCheck = false;
				}
			}
		}
	}
}
