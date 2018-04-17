using UnityEngine;

public class InfomationChar : MonoBehaviour {

	[SerializeField]
	UnityEngine.UI.Text Lezel;
	[SerializeField]
	UnityEngine.UI.Text HP;
	[SerializeField]
	UnityEngine.UI.Text ATK;
	[SerializeField]
	UnityEngine.UI.Text DEF;

	[SerializeField]
	DataLevelStat data;

	// Use this for initialization
	void OnEnable () {
		Lezel.text = "Lv: " + (data.level + 1).ToString ();
		HP.text = "HP: " + data.hpBonus;
		ATK.text = "ATK: " + data.atkBonus;
		DEF.text = "DEF: " + data.defBonus;
	}
}
