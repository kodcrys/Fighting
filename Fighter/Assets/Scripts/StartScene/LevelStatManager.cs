
using UnityEngine;

public class LevelStatManager : MonoBehaviour {

	public static LevelStatManager intance;

	[SerializeField]
	Stat expBar;

	[SerializeField]
	DataLevelStat dataLevelStat;

	[SerializeField]
	UnityEngine.UI.Text levelDisplay;

	[Header("Dialog stat Lv up")]
	[SerializeField]
	public GameObject dialogStatLvUp;
	// old stat
	[SerializeField]
	public UnityEngine.UI.Text oldLv;
	[SerializeField]
	public UnityEngine.UI.Text oldHp;
	[SerializeField]
	public UnityEngine.UI.Text oldAtk;
	[SerializeField]
	public UnityEngine.UI.Text oldDef;
	// new stat
	[SerializeField]
	public UnityEngine.UI.Text newLv;
	[SerializeField]
	public UnityEngine.UI.Text newHp;
	[SerializeField]
	public UnityEngine.UI.Text newAtk;
	[SerializeField]
	public UnityEngine.UI.Text newDef;

	void Awake() {
		if (intance == null)
			intance = this;
	}

	public void Start() {
		expBar.Initialze();

		UpdateExp();
	}

	public void UpdateExp () {

		expBar.bar.isChange = true;

		expBar.MaxVal = dataLevelStat.expLevelUp[dataLevelStat.level];
		expBar.CurrentVal = SaveManager.instance.state.CurExp;

		levelDisplay.text = "Lv. " + (dataLevelStat.level + 1).ToString ();
	}

	public void IncreaseExp(int expIncrease) {

		bool isLevelUp = false;

		SaveManager.instance.state.CurExp += expIncrease;
		SaveManager.instance.Save ();

		int lvCur = dataLevelStat.level;
		int plusHP = 0, plusAtk = 0, plusDef = 0;

		while (SaveManager.instance.state.CurExp >= dataLevelStat.expLevelUp [lvCur]) {
			SaveManager.instance.state.CurExp -= dataLevelStat.expLevelUp [lvCur];
			SaveManager.instance.Save ();

			lvCur++;

			//Debug.Log (SaveManager.instance.state.CurExp + " " + dataLevelStat.expLevelUp [lvCur]);

			plusHP += Random.Range (1, 3);
			plusAtk += Random.Range (1, 3);
			plusDef += Random.Range (1, 3);

			//Debug.Log (plusHP + " " + plusAtk + " " + plusDef);

			isLevelUp = true;

			UpdateExp ();
		}

		if (isLevelUp) {
			oldLv.text = "LV: " + (dataLevelStat.level + 1) + " (+" + (lvCur - dataLevelStat.level) + ")";
			oldHp.text = "HP: " + dataLevelStat.hpBonus + " (+" + plusHP + ")";
			oldAtk.text = "ATK: " + dataLevelStat.atkBonus + " (+" + plusAtk + ")";
			oldDef.text = "DEF: " + dataLevelStat.defBonus + " (+" + plusDef + ")";

			// Save data
			dataLevelStat.level = lvCur;
			dataLevelStat.hpBonus += plusHP;
			dataLevelStat.atkBonus += plusAtk;
			dataLevelStat.defBonus += plusDef;

			newLv.text = "LV: " + (dataLevelStat.level + 1);
			newHp.text = "HP: " + dataLevelStat.hpBonus;
			newAtk.text = "ATK: " + dataLevelStat.atkBonus;
			newDef.text = "DEF: " + dataLevelStat.defBonus;

			dialogStatLvUp.SetActive (true);

			isLevelUp = false;
		}

		if (SaveManager.instance.state.CurExp < dataLevelStat.expLevelUp [lvCur])
			UpdateExp ();
	}

	public void CloseDialog() {
		dialogStatLvUp.SetActive(false);
	}
}
