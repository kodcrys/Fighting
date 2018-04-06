using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconControl : MonoBehaviour {

	[Header("------Animations UI------")]
	[SerializeField]
	Transform posIconLibrary;
	[SerializeField]
	Transform posHideLibrary;
	[SerializeField]
	Transform posShowLibrary;
	[SerializeField]
	GameObject btnBackLibrary;

	[Header("------Data------")]
	[SerializeField]
	CointainData[] dataChars;
	[SerializeField]
	private List<Sprite> listSpriteMask;

	[Header("------Icon------")]
	[SerializeField]
	UnityEngine.UI.Image maskIcon;
	[SerializeField]
	GameObject changebtn;

	bool isMove = false, isMoveLibraryUp, isMoveLibraryDown;
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
		isMoveLibraryUp = false;
		isMoveLibraryDown = false;

		maskIcon.sprite = listSpriteMask [SaveManager.instance.state.iconChar];
		
	}

	void Update() 
	{
		if (isMoveLibraryUp) 
		{
			posIconLibrary.position = Vector3.MoveTowards (posIconLibrary.position, posShowLibrary.position, 40 * Time.deltaTime);
			if (posIconLibrary.position == posShowLibrary.position)
				isMoveLibraryUp = false;
		}

		if (isMoveLibraryDown) 
		{
			posIconLibrary.position = Vector3.MoveTowards (posIconLibrary.position, posHideLibrary.position, 40 * Time.deltaTime);
			if (posIconLibrary.position == posHideLibrary.position)
				isMoveLibraryDown = false;
		}
	}
		
	public void OnChangeIcon(int idChar) 
	{
		maskIcon.sprite = listSpriteMask[idChar];
		SaveManager.instance.state.iconChar = idChar;
		SaveManager.instance.Save ();
		changebtn.SetActive (false);
		HideLibrary ();
	}

	void EnDisableLibraryCell() 
	{
		for (int i = 0; i < dataChars.Length; i++) 
		{
			if (dataChars [i].dataChar.isOwned == false)
				dataChars [i].GetComponent<UnityEngine.UI.Button> ().interactable = false;
			else
				dataChars [i].GetComponent<UnityEngine.UI.Button> ().interactable = true;
		}
	}

	public void ShowLibrary() 
	{
		if (isClick == false) 
		{
			isMoveLibraryUp = true;
			EnDisableLibraryCell ();
			btnBackLibrary.SetActive (true);
			isClick = true;
		}
	}

	public void HideLibrary() 
	{
		if (isClick) 
		{
			isMoveLibraryDown = true;
			btnBackLibrary.SetActive (false);
			isClick = false;
		}
	}
		
}
