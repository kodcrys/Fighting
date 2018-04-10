﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChooseCharControl : MonoBehaviour {
	public static ChooseCharControl instance;

	[Header("Button change type choose")]
	[SerializeField]
	UnityEngine.UI.Button[] btnsChangeType;
	[SerializeField]
	Transform [] PosBtnsChangeType;

	[SerializeField]
	GameObject typeChooseChar;
	[SerializeField]
	GameObject typeChooseEquipment;

	[Header("Data")]
	[SerializeField]
	CointainData[] dataChars;
	[SerializeField]
	CointainData[] dataItems;
	[SerializeField]
	CointainData[] dataHats;
	[SerializeField]
	CointainData[] dataAmors;
	[SerializeField]
	CointainData[] dataWeapons;

	[Header("Choose Action")]
	public GameObject chooseSymbol;

	[Header("Mask")]
	[SerializeField]
	Transform maskFrameChooseChar;
	[SerializeField]
	Transform maskFrameChooseEquip;

	[Header("PLayer1")]
	[SerializeField]
	UnityEngine.UI.Image hatSymbol2;
	[SerializeField]
	UnityEngine.UI.Image hatMainL;
	[SerializeField]
	UnityEngine.UI.Image amorMainL;
	[SerializeField]
	UnityEngine.UI.Image weaponMainL;

	[Header("UIAnimations")]
	[SerializeField]
	UIAnimations topBar;
	[SerializeField]
	UIAnimations midBar;
	[SerializeField]
	UIAnimations botBar;
	[SerializeField]
	UIAnimations moveChooseFrame;
	[SerializeField]
	UIAnimations showTop;
	[SerializeField]
	UIAnimations vsImage;
	[SerializeField]
	UIAnimations nextBtn;
	[SerializeField]
	UIAnimations preBtn;

	[Header("Lock Btn")]
	[SerializeField]
	Sprite lockSpr;
	[SerializeField]
	Sprite readySpr;

	[Header("Btn Play Game")]
	[SerializeField]
	UnityEngine.UI.Text contentBtn;
	string[] playMode = { "P1 VS P2", "P1 VS CPU", "TOURNAMENT", "MINI GAME" };
	[SerializeField]
	GameObject play2Btn;
	[SerializeField]
	GameObject aiBtn;

	[Header("Screen Map")]
	[SerializeField]
	SpriteRenderer map;

	[Header("Fade ani")]
	[SerializeField]
	FadeAni aniFade;

	[Header("Load Data")]
	[SerializeField]
	DataCharacter[] lstCharacters;
	[SerializeField]
	DataItems[] lstItems;

	bool isTurnPlayer1 = false;

	GameObject objFollow;

	[SerializeField]
	UnityEngine.UI.Image ready1;
	[HideInInspector]
	public bool isPlayAI;

	[Header("FadeAni")]
	[SerializeField]
	FadeAni fadeAni;

	void Awake() 
	{
		if (instance == null)
			instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		isTurnPlayer1 = true;
		chooseSymbol.SetActive (false);
	}

	// khi chọn vào char hoặc equipment thì sẽ có vòng tròn sáng màu vàng hiện ra ở chỗ chọn.
	void Update() 
	{
		if(objFollow != null)
			chooseSymbol.transform.position = objFollow.transform.position;
	}

	bool isShowTypeChar = true;

	// CHange type equipment choose or character choose
	public void ChangeTypeChar() 
	{
		isShowTypeChar = !isShowTypeChar;
		chooseSymbol.SetActive (false);
		if (isShowTypeChar) 
		{
			btnsChangeType [0].transform.SetParent (PosBtnsChangeType [1].transform);
			btnsChangeType [1].transform.SetParent (PosBtnsChangeType [0].transform);
			typeChooseChar.SetActive (true);
			typeChooseEquipment.SetActive (false);
		} 
		else 
		{
			btnsChangeType [1].transform.SetParent (PosBtnsChangeType [1].transform);
			btnsChangeType [0].transform.SetParent (PosBtnsChangeType [0].transform);
			typeChooseChar.SetActive (false);
			typeChooseEquipment.SetActive (true);
		}
	}

	// display image in each library cell
	void EnDisableLibraryCell() 
	{
		for (int i = 0; i < dataChars.Length; i++) 
		{
			if (dataChars [i].dataChar.isOwned == false)
				dataChars [i].GetComponent<UnityEngine.UI.Button> ().interactable = false;
			else
				dataChars [i].GetComponent<UnityEngine.UI.Button> ().interactable = true;
		}

		for (int i = 0; i < dataItems.Length; i++) 
		{
			if (dataItems [i].dataItem.isOwned == false)
				dataItems [i].GetComponent<UnityEngine.UI.Button> ().interactable = false;
			else
				dataItems [i].GetComponent<UnityEngine.UI.Button> ().interactable = true;
		}
	}

	// run ani change scene when click play
	public void AniChangeScene() 
	{
		// p1 zs p2
		if (contentBtn.text == playMode [0]) 
		{
			SaveManager.instance.state.player1AI = false;
			SaveManager.instance.state.player2AI = false;
			play2Btn.SetActive (true);
			aiBtn.SetActive (false);

			isTurnPlayer1 = true;

			ready1.sprite = readySpr;

			preBtn.GetComponent<UnityEngine.UI.Button> ().interactable = true;
			nextBtn.GetComponent<UnityEngine.UI.Button> ().interactable = false;

			EnDisableLibraryCell ();

			topBar.isRunMoveAni = false;
			midBar.isRunScaleAni = false;
			botBar.isRunMoveAni = false;

			showTop.isRunMoveAni = true;
			vsImage.isRunScaleAni = true;
			moveChooseFrame.isRunMoveAni = true;
			nextBtn.isRunMoveAni = true;
			preBtn.isRunMoveAni = true;

			ReadSave ();
		}

		// zs AI
		if (contentBtn.text == playMode [1]) {
			SaveManager.instance.state.player1AI = false;
			SaveManager.instance.state.player2AI = true;

			play2Btn.SetActive (false);
			aiBtn.SetActive (true);

			isTurnPlayer1 = true;

			ready1.sprite = readySpr;
			preBtn.GetComponent<UnityEngine.UI.Button> ().interactable = true;
			EnDisableLibraryCell ();

			topBar.isRunMoveAni = false;
			midBar.isRunScaleAni = false;
			botBar.isRunMoveAni = false;

			showTop.isRunMoveAni = true;
			vsImage.isRunScaleAni = true;
			moveChooseFrame.isRunMoveAni = true;
			preBtn.isRunMoveAni = true;

			aiBtn.GetComponent<UIAnimations> ().isRunMoveAni = true;
		}

		// tour
		if (contentBtn.text == playMode [2]) {

		}
	}
		
	// Khi bấm ready thì nút ready của player 1 sẽ tắt và hiện cái nút ready của player 2
	public void LockWhenFinishChoose() 
	{
		if (isTurnPlayer1) 
		{
			preBtn.GetComponent<UnityEngine.UI.Button> ().interactable = false;
			nextBtn.GetComponent<UnityEngine.UI.Button> ().interactable = true;
			ready1.sprite = lockSpr;
			isTurnPlayer1 = false;

			// AI
			if (contentBtn.text == playMode [1]) 
			{
				aniFade.stateFade = FadeAni.State.Show;
				aniFade.isChangeMap = true;
				// Change mode AI afer play
				PlayModeAI ();
			}

			if (SaveManager.instance.state.idChar1 != -1)
				GameplayBase.dataPlayer1 = lstCharacters [SaveManager.instance.state.idChar1];
			else
				GameplayBase.dataPlayer1 = null;

			if (SaveManager.instance.state.idHat1 != -1)
				GameplayBase.hatPlayer1 = lstItems [SaveManager.instance.state.idHat1];
			else
				GameplayBase.hatPlayer1 = null;

			if (SaveManager.instance.state.idAmor1 != -1)
				GameplayBase.amorPlayer1 = lstItems [SaveManager.instance.state.idAmor1];
			else
				GameplayBase.amorPlayer1 = null;

			if (SaveManager.instance.state.idWp1 != -1)
				GameplayBase.wpPlayer1 = lstItems [SaveManager.instance.state.idWp1];
			else
				GameplayBase.wpPlayer1 = null;

		} 
		else 
		{
			if (contentBtn.text == playMode [0]) 
			{
				nextBtn.GetComponent<UnityEngine.UI.Button> ().interactable = false;
				aniFade.stateFade = FadeAni.State.Show;
				aniFade.isChangeMap = true;

				if (SaveManager.instance.state.idChar2 != -1)
					GameplayBase.dataPlayer2 = lstCharacters [SaveManager.instance.state.idChar2];
				else
					GameplayBase.dataPlayer2 = null;

				if (SaveManager.instance.state.idHat2 != -1)
					GameplayBase.hatPlayer2 = lstItems [SaveManager.instance.state.idHat2];
				else
					GameplayBase.hatPlayer2 = null;

				if (SaveManager.instance.state.idAmor2 != -1)
					GameplayBase.amorPlayer2 = lstItems [SaveManager.instance.state.idAmor2];
				else
					GameplayBase.amorPlayer2 = null;

				if (SaveManager.instance.state.idWp2 != -1)
					GameplayBase.wpPlayer2 = lstItems [SaveManager.instance.state.idWp2];
				else
					GameplayBase.wpPlayer2 = null;
			}
			//UnityEngine.SceneManagement.SceneManager.LoadScene ("ChooseMap");
		}
	}

	// choose character or equipment when click button in choose frame
	public void ChooseChar() {
		GameObject gob = EventSystem.current.currentSelectedGameObject;

		objFollow = gob;

		if (isShowTypeChar)
			chooseSymbol.transform.SetParent (maskFrameChooseChar);
		else
			chooseSymbol.transform.SetParent (maskFrameChooseEquip);

		CointainData ctData = gob.GetComponent<CointainData> ();

		if (ctData.dataChar != null && ctData.dataChar.isOwned) 
		{
			if (isTurnPlayer1) 
			{
				hatSymbol2.gameObject.SetActive (false);
				hatMainL.gameObject.SetActive (true);
				hatMainL.sprite = ctData.dataChar.equipmentOfChar;
				amorMainL.gameObject.SetActive (false);
				weaponMainL.gameObject.SetActive (false);

				// Open data char
				GameplayBase.dataPlayer1 = ctData.dataChar;

				// Save data char
				SaveManager.instance.state.idChar1 = ctData.dataChar.id;
				SaveManager.instance.state.idHat1 = -1;
				SaveManager.instance.state.idAmor1 = -1;
				SaveManager.instance.state.idWp1 = -1;
				SaveManager.instance.Save ();

				// Close data items
				GameplayBase.hatPlayer1 = null;
				GameplayBase.amorPlayer1 = null;
				GameplayBase.wpPlayer1 = null;
			} 

			chooseSymbol.SetActive (true);
		} 
		else
			chooseSymbol.SetActive (false);


		if (ctData.dataItem != null && ctData.dataItem.isOwned) 
		{
			if (isTurnPlayer1) 
			{
				hatSymbol2.gameObject.SetActive (false);

				GameplayBase.dataPlayer1 = null;

				SaveManager.instance.state.idChar1 = -1;
				SaveManager.instance.Save ();

				if (ctData.dataItem.typeItem == TypeObject.hat) 
				{
					hatMainL.gameObject.SetActive (true);
					hatMainL.sprite = ctData.dataItem.avatar;

					SaveManager.instance.state.idHat1 = ctData.dataItem.id;
					SaveManager.instance.Save ();

					GameplayBase.hatPlayer1 = ctData.dataItem;
				}

				if (ctData.dataItem.typeItem == TypeObject.tshirt) 
				{
					amorMainL.gameObject.SetActive (true);
					amorMainL.sprite = ctData.dataItem.avatar;

					SaveManager.instance.state.idAmor1 = ctData.dataItem.id;
					SaveManager.instance.Save ();

					GameplayBase.amorPlayer1 = ctData.dataItem;
				}

				if (ctData.dataItem.typeItem == TypeObject.weapon) 
				{
					weaponMainL.gameObject.SetActive (true);
					weaponMainL.sprite = ctData.dataItem.avatar;

					SaveManager.instance.state.idWp1 = ctData.dataItem.id;
					SaveManager.instance.Save ();

					GameplayBase.wpPlayer1 = ctData.dataItem;
				}
			} 
			chooseSymbol.SetActive (true);
		} 
		else
			chooseSymbol.SetActive (true);

		if (ctData.dataMap != null) 
		{
			ctData.dataMap.SetActive (true);
			chooseSymbol.SetActive (true);
		}
	}

	public void RandomChar() 
	{
		GameObject gob = EventSystem.current.currentSelectedGameObject;
		List<CointainData> dtChars = new List<CointainData> ();

		chooseSymbol.SetActive (false);

		for (int i = 0; i < dataChars.Length; i++)
			if (dataChars [i].dataChar.isOwned)
				dtChars.Add (dataChars [i]);

		if (dtChars.Count > 0) 
		{
			int rand = Random.Range (0, dtChars.Count);
			while (dtChars [rand].dataChar.isOwned == false) 
			{
				rand = Random.Range (0, dtChars.Count);
			}
			CointainData ctData = dtChars [rand];

			if (isTurnPlayer1) 
			{
				hatSymbol2.gameObject.SetActive (false);
				hatMainL.gameObject.SetActive (true);
				hatMainL.sprite = ctData.dataChar.equipmentOfChar;
				amorMainL.gameObject.SetActive (false);
				weaponMainL.gameObject.SetActive (false);

				// Open data char
				GameplayBase.dataPlayer1 = ctData.dataChar;

				// Close data items
				GameplayBase.hatPlayer1 = null;
				GameplayBase.amorPlayer1 = null;
				GameplayBase.wpPlayer1 = null;

				SaveManager.instance.state.idChar1 = ctData.dataChar.id;

				SaveManager.instance.state.idHat1 = -1;
				SaveManager.instance.state.idAmor1 = -1;
				SaveManager.instance.state.idWp1 = -1;

				SaveManager.instance.Save ();
			} 
		}
	}

	public void RandomHat() 
	{
		GameObject gob = EventSystem.current.currentSelectedGameObject;
		List<CointainData> dtHats = new List<CointainData> ();

		chooseSymbol.SetActive (false);

		for (int i = 0; i < dataHats.Length; i++)
			if (dataHats [i].dataItem.isOwned)
				dtHats.Add (dataHats [i]);

		if (dtHats.Count > 0) 
		{
			int rand = Random.Range (0, dtHats.Count);
			while (dtHats [rand].dataItem.isOwned == false) 
			{
				rand = Random.Range (0, dtHats.Count);
			}
			CointainData ctData = dtHats [rand];

			if (isTurnPlayer1) 
			{
				hatSymbol2.gameObject.SetActive (false);
				hatMainL.gameObject.SetActive (true);
				hatMainL.sprite = ctData.dataItem.avatar;

				// close data char
				GameplayBase.dataPlayer1 = null;

				// open data items
				GameplayBase.hatPlayer1 = ctData.dataItem;

				SaveManager.instance.state.idHat1 = ctData.dataItem.id;

				SaveManager.instance.state.idChar1 = -1;

				SaveManager.instance.Save ();
			} 
		}
	}

	public void RandomAmor() 
	{
		GameObject gob = EventSystem.current.currentSelectedGameObject;
		List<CointainData> dtAmors = new List<CointainData> ();

		chooseSymbol.SetActive (false);

		for (int i = 0; i < dataAmors.Length; i++)
			if (dataAmors [i].dataItem.isOwned)
				dtAmors.Add (dataAmors [i]);

		if (dtAmors.Count > 0) {
			int rand = Random.Range (0, dtAmors.Count);
			while (dtAmors [rand].dataItem.isOwned == false) {
				rand = Random.Range (0, dtAmors.Count);
			}
			CointainData ctData = dtAmors [rand];

			if (isTurnPlayer1) 
			{
				hatSymbol2.gameObject.SetActive (false);
				amorMainL.gameObject.SetActive (true);
				amorMainL.sprite = ctData.dataItem.avatar;

				// close data char
				GameplayBase.dataPlayer1 = null;

				// open data items
				GameplayBase.amorPlayer1 = ctData.dataItem;

				SaveManager.instance.state.idAmor1 = ctData.dataItem.id;

				SaveManager.instance.state.idChar1 = -1;

				SaveManager.instance.Save ();
			} 
		}
	}

	public void RandomWeapon() 
	{
		GameObject gob = EventSystem.current.currentSelectedGameObject;
		List<CointainData> dtWps = new List<CointainData> ();

		chooseSymbol.SetActive (false);

		for (int i = 0; i < dataWeapons.Length; i++)
			if (dataWeapons [i].dataItem.isOwned)
				dtWps.Add (dataWeapons [i]);

		if (dtWps.Count > 0) {
			int rand = Random.Range (0, dtWps.Count);
			while (dtWps [rand].dataItem.isOwned == false) 
			{
				rand = Random.Range (0, dtWps.Count);
			}
			CointainData ctData = dtWps [rand];

			if (isTurnPlayer1) 
			{
				hatSymbol2.gameObject.SetActive (false);
				weaponMainL.gameObject.SetActive (true);
				weaponMainL.sprite = ctData.dataItem.avatar;

				// close data char
				GameplayBase.dataPlayer1 = null;

				// open data items
				GameplayBase.wpPlayer1 = ctData.dataItem;

				SaveManager.instance.state.idWp1 = ctData.dataItem.id;

				SaveManager.instance.state.idChar1 = -1;

				SaveManager.instance.Save ();
			} 
		}
	}

	public void ReadSave() 
	{
		if (SaveManager.instance.state.idChar1 != -1) 
		{
			Debug.Log(SaveManager.instance.state.idChar1);
			hatSymbol2.gameObject.SetActive (false);

			hatMainL.gameObject.SetActive (true);
			hatMainL.sprite = lstCharacters[SaveManager.instance.state.idChar1].equipmentOfChar;

			amorMainL.gameObject.SetActive (false);
			weaponMainL.gameObject.SetActive (false);
		} 
		else 
			if (SaveManager.instance.state.idChar1 == -1) 
			{
				hatSymbol2.gameObject.SetActive (true);
				hatMainL.gameObject.SetActive (false);
				amorMainL.gameObject.SetActive (false);
				weaponMainL.gameObject.SetActive (false);
			}

		if (SaveManager.instance.state.idHat1 != -1) 
		{
			hatSymbol2.gameObject.SetActive (false);
			hatMainL.gameObject.SetActive (true);
			hatMainL.sprite = lstItems[SaveManager.instance.state.idHat1].avatar;
		} 
		else 
			if (SaveManager.instance.state.idHat1 == -1)
			{
				hatMainL.gameObject.SetActive (false);

				if (SaveManager.instance.state.idChar1 != -1) 
				{
					hatMainL.gameObject.SetActive (true);
					hatSymbol2.gameObject.SetActive (false);
				}
				else 
				{
					hatMainL.gameObject.SetActive (false);
					hatSymbol2.gameObject.SetActive (true);
				}
			}
				
		if (SaveManager.instance.state.idAmor1 != -1) 
		{
			amorMainL.gameObject.SetActive (true);
			amorMainL.sprite = lstItems[SaveManager.instance.state.idAmor1].avatar;
		} 
		else 
			if (SaveManager.instance.state.idAmor1 == -1) 
			{
				amorMainL.gameObject.SetActive (false);
			}
	
		if (SaveManager.instance.state.idWp1 != -1) 
		{
			weaponMainL.gameObject.SetActive (true);
			weaponMainL.sprite = lstItems[SaveManager.instance.state.idWp1].avatar;
		} 
		else 
			if (SaveManager.instance.state.idWp1 == -1) 
			{
				weaponMainL.gameObject.SetActive (false);
			}
	}

	public void BackChooseChar() 
	{
		aniFade.stateFade = FadeAni.State.Show;
		aniFade.isChangeChooseChar = true;

		// bien danh dau tu map ze choose char
		FadeAni.isRunMapToChooseChar = true;
		FadeAni.isRunMapToHome = false;
		FadeAni.isRunPlayGame = false;
	}

	public void Home() 
	{
		aniFade.stateFade = FadeAni.State.Show;
		aniFade.isChangeChooseChar = true;
		FadeAni.isRunMapToChooseChar = true;
		FadeAni.isRunMapToHome = true;
		FadeAni.isRunPlayGame = false;
		//UnityEngine.SceneManagement.SceneManager.LoadScene ("StartScene");
	}

	public void PlayGame() 
	{
		aniFade.stateFade = FadeAni.State.Show;
		aniFade.isChangeChooseChar = true;
		FadeAni.isRunMapToChooseChar = false;
		FadeAni.isRunMapToHome = false ;
		FadeAni.isRunPlayGame = true;
		SaveManager.instance.state.winCountLeft = 0;
		SaveManager.instance.state.winCountRight = 0;
		SaveManager.instance.state.roundCount = 1;
		SaveManager.instance.state.whatMode = 1;
		SaveManager.instance.Save ();
	}

	void PlayModeAI() 
	{
		// neu file luu tru theo ID cua nhan zat hoac equipment dang != -1 (nghia la co luu tru thi thuc hien thay doi theo character player)
		// neu player chon character thi random character cho AI
		if (SaveManager.instance.state.idChar1 != -1) 
		{
			SaveManager.instance.state.idCharAI = Random.Range (0, lstCharacters.Length);
			SaveManager.instance.Save ();

			// Open data char
			GameplayBase.dataAI = lstCharacters[SaveManager.instance.state.idCharAI];

			// Close data items
			GameplayBase.hatAI = null;
			GameplayBase.amorAI = null;
			GameplayBase.hatAI = null;
		}

		// neu player chon equipment tuong ung thi AI se co equipment ung zs player
		if (SaveManager.instance.state.idHat1 != -1) 
		{
			SaveManager.instance.state.idHatAI = Random.Range (55, 116);
			SaveManager.instance.Save ();

			// Close data char
			GameplayBase.dataAI = null;

			// Open data items
			GameplayBase.hatAI = lstItems[SaveManager.instance.state.idHatAI];
		}

		if (SaveManager.instance.state.idAmor1 != -1) 
		{
			SaveManager.instance.state.idAmorAI = Random.Range (0, 55);
			SaveManager.instance.Save ();

			// Close data char
			GameplayBase.dataAI = null;

			// Open data items
			GameplayBase.amorAI = lstItems[SaveManager.instance.state.idAmorAI];
		}

		if (SaveManager.instance.state.idWp1 != -1) 
		{
			SaveManager.instance.state.idWpAI = Random.Range (116, lstItems.Length);
			SaveManager.instance.Save ();

			// Close data char
			GameplayBase.dataAI = null;

			// Open data items
			GameplayBase.wpAI = lstItems[SaveManager.instance.state.idWpAI];
		}
			
		SaveManager.instance.Save ();
	}
}