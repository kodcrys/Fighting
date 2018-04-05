using UnityEngine;

public class Library : MonoBehaviour {

	[Header("Frame information character")]
	[SerializeField]
	GameObject frameInfo;
	[SerializeField]
	UnityEngine.UI.Text nameTxt;
	[SerializeField]
	UnityEngine.UI.Text HpTxt;
	[SerializeField]
	UnityEngine.UI.Text AtkTxt;
	[SerializeField]
	UnityEngine.UI.Text DefTxt;

	[Header("Button change library")]
	[SerializeField]
	UnityEngine.UI.Button[] btnsChangeLibrary;
	[SerializeField]
	Transform [] PosBtnsChangeLibrary;

	[Header("Library Type")]
	[SerializeField]
	GameObject libraryEquipment;
	[SerializeField]
	GameObject libraryCharacter;

	[Header("Animations UI")]
	[SerializeField]
	UIAnimations libraryMove;
	[SerializeField]
	UIAnimations topBarStartMenu;
	[SerializeField]
	UIAnimations botBarStartMenu;
	[SerializeField]
	UIAnimations midBarStartMenu;
	[SerializeField]
	GameObject btnBackLibrary;

	[Header("Data")]
	[SerializeField]
	CointainData[] dataChars;
	[SerializeField]
	CointainData[] dataItems;

	bool isMove = false;
	public bool isClick = false;

	public static Library Instance;

	void Awake () {
		if (Instance == null)
			Instance = this;
	}

	void Start()
	{
		isMove = false;
		isClick = false;
	}

	void Update() {
		if (isMove) {
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			pos.z = 0;
			frameInfo.transform.position = Vector3.MoveTowards(frameInfo.transform.position, pos, 10000 * Time.deltaTime);
		}
	}
		
	public void ReadInfoCharacter(DataCharacter dataChar) {

		isMove = true;

		nameTxt.text = dataChar.name;
		HpTxt.text = "HP: " + dataChar.HP.ToString ();
		AtkTxt.text = "ATK: " + dataChar.ATK.ToString ();
		DefTxt.text = "DEF: " + dataChar.DEF.ToString ();


		frameInfo.SetActive (true);
	}

	public void UnReadInfoCharacter() {
		isMove = false;
		frameInfo.SetActive (false);
	}

	public void ReadInfoEquipment(DataItems dataItem) {
		isMove = true;

		nameTxt.text = dataItem.name;
		HpTxt.text = "HP: " + dataItem.HP.ToString ();
		AtkTxt.text = "ATK: " + dataItem.ATK.ToString ();
		DefTxt.text = "DEF: " + dataItem.DEF.ToString ();

		frameInfo.SetActive (true);
	}

	bool isShowLibChar = true;
	public void ChangeLibrary() {
		isShowLibChar = !isShowLibChar;
		if (isShowLibChar) {
			btnsChangeLibrary [0].transform.SetParent (PosBtnsChangeLibrary [1].transform);
			btnsChangeLibrary [1].transform.SetParent (PosBtnsChangeLibrary [0].transform);
			libraryCharacter.SetActive (true);
			libraryEquipment.SetActive (false);
		} else {
			btnsChangeLibrary [1].transform.SetParent (PosBtnsChangeLibrary [1].transform);
			btnsChangeLibrary [0].transform.SetParent (PosBtnsChangeLibrary [0].transform);
			libraryCharacter.SetActive (false);
			libraryEquipment.SetActive (true);
		}
	}

	void EnDisableLibraryCell() {
		for (int i = 0; i < dataChars.Length; i++) {
			if (dataChars [i].dataChar.isOwned == false)
				//dataChars [i].GetComponent<UnityEngine.UI.Button> ().interactable = false;
				dataChars [i].transform.GetChild (1).gameObject.SetActive (true);
			else
				//dataChars [i].GetComponent<UnityEngine.UI.Button> ().interactable = true;
				dataChars [i].transform.GetChild (1).gameObject.SetActive (false);
		}

		for (int i = 0; i < dataItems.Length; i++) {
			if (dataItems [i].dataItem.isOwned == false)
				//dataItems [i].GetComponent<UnityEngine.UI.Button> ().interactable = false;
				dataItems [i].transform.GetChild (1).gameObject.SetActive (true);
			else
				dataItems [i].transform.GetChild (1).gameObject.SetActive (false);
		}
	}

	public void ShowLibrary() {
		if (isClick == false) {
			EnDisableLibraryCell ();
			libraryMove.isRunMoveAni = true;
			topBarStartMenu.isRunMoveAni = false;
			botBarStartMenu.isRunMoveAni = false;
			midBarStartMenu.isRunScaleAni = false;
			//btnBackLibrary.SetActive (true);
			isClick = true;
		}
	}

	public void HideLibrary() {
		if (isClick) {
			libraryMove.isRunMoveAni = false;
			topBarStartMenu.isRunMoveAni = true;
			botBarStartMenu.isRunMoveAni = true;
			midBarStartMenu.isRunScaleAni = true;
			btnBackLibrary.SetActive (false);
			//isClick = false;
		}
	}
}
