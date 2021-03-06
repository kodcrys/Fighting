﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayBase : MonoBehaviour {

	public static GameplayBase instance;

	public GameObject rightButton, leftButton;

	public bool zoomCamera;

	public bool gamePause;

	public Camera mainCamera;

	bool isTalk;

	float time, timeInter;

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
	private UIAnimations soundUI;
	[SerializeField]
	private UIAnimations musicUI;
	[SerializeField]
	private UIAnimations voiceUI;
	[SerializeField]
	private UIAnimations ringUI;

	public FadeAni aniFade;

	public GameObject pausePanel, gameoverP1Panel, gameoverP2Panel;

	public static DataCharacter dataPlayer1;
	public static DataCharacter dataPlayer2;
	public static DataCharacter dataAI;

	public static DataItems hatPlayer1, hatPlayer2, amorPlayer1, amorPlayer2, wpPlayer1, wpPlayer2;
	public static DataItems hatAI, amorAI, wpAI;

	int ranVoice1;
	int ranVoice2;

	bool wasTalk;
	bool waitToTalk;

	[SerializeField]
	GameObject emojiLeft;
	[SerializeField]
	GameObject emojiRight;

	public void Start() {
		instance = this;
		gamePause = false;
		isTalk = false;
		wasTalk = false;
		waitToTalk = false;
		timeInter = Random.Range (10, 20);
		CheckAI ();
		zoomCamera = false;

		CheckSetting ();

		SoundManager.Bangs.Play ();

		if (MultiResolution.device == "ipad") {
			emojiLeft.transform.localPosition = new Vector3 (-1.56f, 5.63f, 0);
			emojiRight.transform.localPosition = new Vector3 (1.56f, 5.63f, 0);
		}
		else {
			emojiLeft.transform.localPosition = new Vector3 (-3.5f, 5, 0);
			emojiRight.transform.localPosition = new Vector3 (3.5f, 5, 0);
		}

		if (SaveManager.instance.state.whatMode == 1) {
			for (int i = 0; i < maps.Length; i++) {
				if (i == ChooseCharManager.indexMap)
					maps [i].SetActive (true);
				else
					maps [i].SetActive (false);
			}
		} else if (SaveManager.instance.state.whatMode == 2) {
			for (int i = 0; i < maps.Length; i++) {
				if (i == SaveManager.instance.state.randMap)
					maps [i].SetActive (true);
				else
					maps [i].SetActive (false);
			}
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
			
			if (!isTalk) {
				if (!player1.GetComponent<FingerLeftControl> ().doingSomething && !player2.GetComponent<FingerRightControl> ().doingSomething) {
					time += Time.deltaTime;
					if (time >= timeInter) {
						if (!wasTalk) {
							SoundManager.WaitToLongs.Play ();
							wasTalk = true;
						}
						isTalk = true;
						time = 0;
					}
				} else if (player1.GetComponent<FingerLeftControl> ().lastAtk || player2.GetComponent<FingerRightControl> ().lastAtk) {
					ranVoice1 = Random.Range (1, 100);
					if (ranVoice1 > 80) {
						ranVoice2 = Random.Range (0, 2);
						if (ranVoice2 == 1) {
							if (!wasTalk) {
								SoundManager.Unbelievables.Play ();
								wasTalk = true;
							}
							isTalk = true;
						} else {
							if (!wasTalk) {
								SoundManager.Fantastics.Play ();
								wasTalk = true;
							}
							isTalk = true;
						}
					}
				}
			} else {
				if (!waitToTalk) {
					StartCoroutine (WaitToTalk (2f));
					waitToTalk = true;
				}
			}
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

	void CheckSetting() {
		soundUI.CheckSound ();
		musicUI.CheckMusic ();
		voiceUI.CheckVoice ();
		ringUI.CheckRing ();
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

	IEnumerator WaitToTalk(float time){
		yield return new WaitForSeconds (time);
		timeInter = Random.Range (10, 20);
		wasTalk = false;
		waitToTalk = false;
		isTalk = false;
	}
}
