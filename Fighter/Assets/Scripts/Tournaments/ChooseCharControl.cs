using System.Collections;
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
	UIAnimations moveChooseFrame;
	[SerializeField]
	UIAnimations showTop;
	[SerializeField]
	UIAnimations vsImage;
	[SerializeField]
	Transform showPosFrame;
	[SerializeField]
	Transform showPosReady;
	[SerializeField]
	Transform frameListChar;
	[SerializeField]
	Transform readybtn;
	[SerializeField]
	GameObject boardGamePanel;


	[Header("Lock Btn")]
	[SerializeField]
	Sprite lockSpr;
	[SerializeField]
	Sprite readySpr;

	[Header("Screen Map")]
	[SerializeField]
	SpriteRenderer map;

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

	bool isMoveListChar;

	void Awake() 
	{
		if (instance == null)
			instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		isMoveListChar = false;
		isTurnPlayer1 = true;
		chooseSymbol.SetActive (false);
		EnDisableLibraryCell ();
	}

	void OnEnable ()
	{
		ReadSave ();
	}

	// khi chọn vào char hoặc equipment thì sẽ có vòng tròn sáng màu vàng hiện ra ở chỗ chọn.
	void Update() 
	{
		if(objFollow != null)
			chooseSymbol.transform.position = objFollow.transform.position;

		if (!isMoveListChar) 
		{
			frameListChar.position = Vector3.MoveTowards (frameListChar.position, showPosFrame.position, 9 * Time.deltaTime);
			readybtn.position = Vector3.MoveTowards (readybtn.position, showPosReady.position, 12 * Time.deltaTime);
		}
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
			{
				dataChars [i].GetComponent<UnityEngine.UI.Button> ().interactable = false;
				dataChars [i].transform.GetChild (1).gameObject.SetActive (true);
			} 
			else 
			{
				dataChars [i].GetComponent<UnityEngine.UI.Button> ().interactable = true;
				dataChars [i].transform.GetChild (1).gameObject.SetActive (false);
			}
		}

		for (int i = 0; i < dataItems.Length; i++) 
		{
			if (dataItems [i].dataItem.isOwned == false) {
				dataItems [i].GetComponent<UnityEngine.UI.Button> ().interactable = false;
				dataItems [i].transform.GetChild (1).gameObject.SetActive (true);
			} 
			else 
			{
				dataItems [i].GetComponent<UnityEngine.UI.Button> ().interactable = true;
				dataItems [i].transform.GetChild (1).gameObject.SetActive (false);
			}
		}
	}

	// run ani change scene when click play
	public void AniChangeScene() 
	{
		// zs AI
		{
			isTurnPlayer1 = true;

			ready1.sprite = readySpr;
			EnDisableLibraryCell ();

			showTop.isRunMoveAni = true;
			moveChooseFrame.isRunMoveAni = true;

		}
	}
		
	// Khi bấm ready thì nút ready của player 1 sẽ tắt và hiện cái nút ready của player 2
	public void LockWhenFinishChoose() 
	{
		if (isTurnPlayer1) 
		{
			ready1.sprite = lockSpr;
			isTurnPlayer1 = false;

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
			
		transform.gameObject.SetActive (false);
		boardGamePanel.SetActive (true);
	}

	// choose character or equipment when click button in choose frame
	public void ChooseChar() 
	{
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

	public void PlayGame() 
	{
		FadeAni.isRunMapToChooseChar = false;
		FadeAni.isRunMapToHome = false ;
		FadeAni.isRunPlayGame = true;
		SaveManager.instance.state.winCountLeft = 0;
		SaveManager.instance.state.winCountRight = 0;
		SaveManager.instance.state.roundCount = 1;
		SaveManager.instance.state.whatMode = 1;
		SaveManager.instance.Save ();
	}
}
