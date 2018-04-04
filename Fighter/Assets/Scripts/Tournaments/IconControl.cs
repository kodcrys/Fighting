using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconControl : MonoBehaviour {

	[Header("------Frame change icon character------")]
	[SerializeField]
	GameObject changebtn;

	[Header("------Animations UI------")]
	[SerializeField]
	UIAnimations libraryMove;
	[SerializeField]
	GameObject btnBackLibrary;

	[Header("------Data------")]
	[SerializeField]
	CointainData[] dataChars;

	bool isMove = false;
	public bool isClick = false;

	public static IconControl Instance;

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
			changebtn.transform.position = Vector3.MoveTowards(changebtn.transform.position, pos, 10000 * Time.deltaTime);
		}
	}

	public void ReadInfoCharacter(DataCharacter dataChar) {

		isMove = true;

		changebtn.SetActive (true);
	}

	public void UnReadInfoCharacter() {
		isMove = false;
		changebtn.SetActive (false);
	}

	public void ReadInfoEquipment(DataItems dataItem) {
		isMove = true;

		changebtn.SetActive (true);
	}

	bool isShowLibChar = true;

	void EnDisableLibraryCell() {
		for (int i = 0; i < dataChars.Length; i++) {
			if (dataChars [i].dataChar.isOwned == false)
				dataChars [i].GetComponent<UnityEngine.UI.Button> ().interactable = false;
			else
				dataChars [i].GetComponent<UnityEngine.UI.Button> ().interactable = true;
		}
	}

	public void ShowLibrary() {
		if (isClick == false) {
			EnDisableLibraryCell ();
			libraryMove.isRunMoveAni = true;
			isClick = true;
		}
	}

	public void HideLibrary() {
		if (isClick) {
			libraryMove.isRunMoveAni = false;
			btnBackLibrary.SetActive (false);
		}
	}
}
