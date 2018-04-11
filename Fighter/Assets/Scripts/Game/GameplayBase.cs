using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayBase : MonoBehaviour {

	public static GameplayBase instance;

	public GameObject rightButton, leftButton;

	public bool zoomCamera;

	public bool gamePause;

	public Camera mainCamera;

	[SerializeField]
	GameObject[] maps;

	[SerializeField]
	GameObject shieldLeft, shieldRight;

	[SerializeField]
	UnityEngine.UI.Image roundNumImg;

	[SerializeField]
	List<Sprite> numRound = new List<Sprite>();

	[SerializeField]
	List<GameObject> winCheckRight = new List<GameObject>();

	[SerializeField]
	List<GameObject> winCheckLeft = new List<GameObject>();

	[SerializeField]
	GameObject player1, player2;

	[SerializeField]
	FadeAni aniFade;

	public GameObject pausePanel, gameoverP1Panel, gameoverP2Panel;

	public static DataCharacter dataPlayer1;
	public static DataCharacter dataPlayer2;
	public static DataCharacter dataAI;

	public static DataItems hatPlayer1, hatPlayer2, amorPlayer1, amorPlayer2, wpPlayer1, wpPlayer2;
	public static DataItems hatAI, amorAI, wpAI;

	public void Start() {
		instance = this;
		gamePause = false;
		CheckAI ();
		zoomCamera = false;
		SoundManager.Bangs.Play ();
		for (int i = 0; i < maps.Length; i++) {
			if (i == ChooseCharManager.indexMap)
				maps [i].SetActive (true);
			else
				maps [i].SetActive (false);
		}

		if (SaveManager.instance.state.roundCount == 1)
			roundNumImg.sprite = numRound [0];
		else if(SaveManager.instance.state.roundCount == 2)
			roundNumImg.sprite = numRound [1];

		if (!SaveManager.instance.state.player2AI) {
			FingerRightControl.instance.ChangeCharPlayer ();
			FingerRightControl.instance.ChangeItemsPlayer ();
		} else if (SaveManager.instance.state.player2AI) {
			FingerRightControl.instance.ChangeCharAI ();
			FingerRightControl.instance.ChangeItemsAI ();
		}

		FingerLeftControl.instance.ChangeCharPlayer ();
		FingerLeftControl.instance.ChangeItemsPlayer ();

		if (player1.GetComponent<FingerLeftControl>().defend > 0) {
			SaveManager.instance.state.isShieldLeft = true;
			SaveManager.instance.Save ();
		}

		if (player2.GetComponent<FingerRightControl>().defend > 0) {
			SaveManager.instance.state.isShieldRight = true;
			SaveManager.instance.Save ();
		}

		if (!SaveManager.instance.state.isShieldLeft) {
			shieldLeft.SetActive (false);
		} else {
			shieldLeft.SetActive (true);
		}

		if (!SaveManager.instance.state.isShieldRight) {
			shieldRight.SetActive (false);
		} else {
			shieldRight.SetActive (true);
		}
	}

	public void Update(){
		if (AnimationText.canPlay) {
			if (!SaveManager.instance.state.player2AI)
				rightButton.SetActive (true);

			if (!SaveManager.instance.state.player1AI)
				leftButton.SetActive (true);

//			if(player1.GetComponent<FingerLeftControl>().doingSomething)
		} else {
			rightButton.SetActive (false);
			leftButton.SetActive (false);
		}
		if (zoomCamera) {
			if (mainCamera.orthographicSize >= SaveManager.instance.state.cameraSize)
				mainCamera.orthographicSize = SaveManager.instance.state.cameraSize;
			else
				mainCamera.orthographicSize += 2 * Time.deltaTime;
		}

		CheckWin ();
	}

	void CheckAI(){
		if (!SaveManager.instance.state.player1AI) {
			player1.GetComponent<AIManager> ().enabled = false;
		} else if (SaveManager.instance.state.player1AI) {
			player1.GetComponent<AIManager> ().enabled = true;
		}

		if (!SaveManager.instance.state.player2AI) {
			player2.GetComponent<AIManager> ().enabled = false;
		} else if (SaveManager.instance.state.player2AI) {
			
			player2.GetComponent<AIManager> ().enabled = true;
		}
	}

	void CheckWin(){
		if (SaveManager.instance.state.winCountRight > 0) {
			for (int i = 1; i <= SaveManager.instance.state.winCountRight; i++) {
				winCheckRight [i - 1].SetActive (true);
			}
		}

		if (SaveManager.instance.state.winCountLeft > 0) {
			for (int i = 1; i <= SaveManager.instance.state.winCountLeft; i++) {
				winCheckLeft [i - 1].SetActive (true);
			}
		}
	}

	public void PauseClick(){
		gamePause = true;
		pausePanel.SetActive (true);
	}

	public void CountinueClick(){
		gamePause = false;
		pausePanel.SetActive (false);
	}

	public void Homecoming(){
		aniFade.stateFade = FadeAni.State.Show;
		aniFade.isChangeChooseChar = true;
		FadeAni.isRunMapToChooseChar = true;
		FadeAni.isRunMapToHome = true;
		FadeAni.isRunPlayGame = false;
	}

	public void ReMatch(){
		SaveManager.instance.state.winCountLeft = 0;
		SaveManager.instance.state.winCountRight = 0;
		SaveManager.instance.state.roundCount = 1;
		SaveManager.instance.Save ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("MainGameScene");
	}
}
