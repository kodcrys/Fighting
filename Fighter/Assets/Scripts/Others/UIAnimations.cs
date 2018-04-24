using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AppAdvisory.social;

public class UIAnimations : MonoBehaviour {

	[Header("Object run animation")]
	[SerializeField]
	Transform target;

	[SerializeField]
	float speed;

	[Header("Scale")]
	// Var define object run ani
	public bool isRunScaleAni;
	// var bool define Object run ANi scale when var isRunScaleAni = false
	[SerializeField]
	bool isScaleAni;
	[SerializeField]
	// var define is botBarObject contain this script
	bool isUIBotBar;
	[SerializeField]
	Vector3 maxScale;
	[SerializeField]
	Vector3 originScale;
	[SerializeField]
	Vector3 minScale;
	bool changeScale1, changeScale2;

	[Header("Button of botBar")]
	[SerializeField]
	bool isShop;
	[SerializeField]
	bool isRate;
	[SerializeField]
	bool isShare;
	[SerializeField]
	bool isLeaderboard;
	[SerializeField]
	bool isLikeFacebook;
	[SerializeField]
	string linkRate;
	[SerializeField]
	string linkShare;
	[SerializeField]
	string linkFB;

	[Header("Change sprite")]
	[SerializeField]
	Sprite sprChangeFrame1;
	[SerializeField]
	Sprite sprChangeFrame2;

	[Header("Change color")]
	[SerializeField]
	bool isRunChangeColorAni;
	[SerializeField]
	Color32 color1;
	[SerializeField]
	Color32 color2;
	Color32 lerpedColor;
	private float t = 0;
	private bool flag;
	Image imgEff;

	[Header("Move Object")]
	// Var define object run ani
	public bool isRunMoveAni;
	[SerializeField]
	// var bool define Object run ANi move move when var isRunMoveAni = false
	bool isMoveAni;
	[SerializeField]
	Transform pos1;
	[SerializeField]
	Transform pos2;
	[SerializeField]
	Transform pos3;
	bool changeDes1, changeDes2, changeDes3;
	[SerializeField]
	float offsetMoveY;

	[Header("Button Play")]
	[SerializeField]
	bool isRunBtnPlayAni;
	[SerializeField]
	string[] btnContent = { "P1 VS P2", "P1 VS CPU", "TOURNAMENT", "MINI GAME" };
	[SerializeField]
	Button playBtn;
	[SerializeField]
	Text contentTxt;
	[SerializeField]
	Button nextBtn;
	[SerializeField]
	Button preBtn;
	public static int indexMode = 0;
	bool isLeft, isRight, changeLeft1, changeLeft2, changeRight1, changeRight2;

	[Header("Ani Sequence Move")]
	// Var define object run ani
	public bool isRunSeqAni;
	[SerializeField]
	// var bool define Object run ANi sequence move when var isRunSeqAni = false
	bool isSequence;
	[SerializeField]
	Transform obj1;
	[SerializeField]
	Transform obj2;
	[SerializeField]
	Transform obj3;
	[SerializeField]
	Transform pos1Seq;
	[SerializeField]
	Transform pos2Seq;
	[SerializeField]
	Transform pos3Seq;
	[SerializeField]
	GameObject progressBar;

	[Header("Ani Shake")]
	public bool isRunShakeAni;
	[SerializeField]
	bool isShakeAni;
	[SerializeField]
	float timeInterShake;
	float timeShake;

	[Header("UI Shop")]
	[SerializeField]
	bool isRunRotateAni;
	[SerializeField]
	GameObject panel_Shop;
	[SerializeField]
	FadeAni fadeShop;

	[Header("Reward Ani")]
	[SerializeField]
	bool isRunRewardAni;
	[SerializeField]
	Image rewardImg;
	[SerializeField]
	Sprite [] rewardSpr;
	[SerializeField]
	GameObject lightReward;
	[SerializeField]
	GameObject canvasReward;
	[SerializeField]
	GameObject fadeOpenReward;
	public bool isFinishFadeRewardAni;

	[Header("Gatcha X10 Ani")]
	[SerializeField]
	bool isRunAniGatcha;
	[SerializeField]
	GameObject [] objectsRunAniGatcha;
	[SerializeField]
	Transform [] posAni;
	[SerializeField]
	Button closeReward;
	[SerializeField]
	GameObject lightX1Gatcha;
	[SerializeField]
	GameObject[] lightX10Gatcha;
	[SerializeField]
	GameObject[] effectSmoke;
	[SerializeField]
	CharacterEquipmentManager charEquipManager;

	[Header("Scale Effect X10 Ani")]
	public bool isRunEffX10Ani;
	[SerializeField]
	ParticleSystem [] effectGlowStar;

	[Header("Library")]
	//define scene is libray?
	[SerializeField]
	bool isLibraryScene;
	[SerializeField]
	GameObject backLibrarybtn;

	[Header("Frame change")]
	[SerializeField]
	bool isRunAniBtnChange;
	[SerializeField]
	bool isRunAniFrameChange;
	[SerializeField]
	Sprite[] changeImgBtn;
	[SerializeField]
	GameObject[] posBtn;
	[SerializeField]
	GameObject[] btnsChange;
	[SerializeField]
	Image btnChange;
	[SerializeField]
	GameObject [] typeChange;

	[Header("Daily quest data")]
	[SerializeField]
	DataQuests Share;
	[SerializeField]
	DataQuests Rate;
	[SerializeField]
	DataQuests LikeFB;
	[SerializeField]
	bool isInStartScene;

	[Header("Scale Sequence")]
	[SerializeField]
	bool isScaleSequence;

	void Awake() {
		if (isInStartScene) {
			Share = GameObject.Find ("Share").GetComponent<CointainData> ().quest;
			Rate = GameObject.Find ("Rate").GetComponent<CointainData> ().quest;
			LikeFB = GameObject.Find ("LikeFacebook").GetComponent<CointainData> ().quest;
		}
	}

	void OnEnable() {
		if (contentTxt != null)
			contentTxt.text = btnContent [indexMode];

		timeShake = 0;
		isLeft = isRight = changeLeft1 = changeLeft2 = changeRight1 = changeRight2 = false;
		changeDes1 = changeDes2 = changeDes3 = false;
		changeScale1 = changeScale2 = false;
		imgEff=GetComponent<Image>();
		StartCoroutine (RunAni ());
	}

	public void OnOffSound() {
		Image imgTarget = target.gameObject.GetComponent<Image> ();
		if (SaveManager.instance.state.isOnSound) {
			SaveManager.instance.state.isOnSound = false;
			imgTarget.sprite = sprChangeFrame2;
			SoundManager.MuteSound ();
		} else {
			SaveManager.instance.state.isOnSound = true;
			imgTarget.sprite = sprChangeFrame1;
			SoundManager.DontMuteSound ();
		}
		SaveManager.instance.Save ();
	}

	public void OnOffMusic() {
		Image imgTarget = target.gameObject.GetComponent<Image> ();
		if (SaveManager.instance.state.isOnMusic) {
			SaveManager.instance.state.isOnMusic = false;
			imgTarget.sprite = sprChangeFrame2;
			SoundManager.MuteBGM ();
		} else {
			SaveManager.instance.state.isOnMusic = true;
			imgTarget.sprite = sprChangeFrame1;
			SoundManager.DontMuteBGM ();
		}
		SaveManager.instance.Save ();
	}

	public void OnOffVoice() {
		Image imgTarget = target.gameObject.GetComponent<Image> ();
		if (SaveManager.instance.state.isOnVoice) {
			SaveManager.instance.state.isOnVoice = false;
			imgTarget.sprite = sprChangeFrame2;
			SoundManager.MuteVoice ();
		} else {
			SaveManager.instance.state.isOnVoice = true;
			imgTarget.sprite = sprChangeFrame1;
			SoundManager.DontMuteVoice ();
		}
		SaveManager.instance.Save ();
	}

	public void OnOffRing() {
		Image imgTarget = target.gameObject.GetComponent<Image> ();
		if (SaveManager.instance.state.isOnRing) {
			SaveManager.instance.state.isOnRing = false;
			imgTarget.sprite = sprChangeFrame2;
		} else {
			SaveManager.instance.state.isOnRing = true;
			imgTarget.sprite = sprChangeFrame1;
		}
		SaveManager.instance.Save ();
	}

	public void CheckSound() {
		Image imgTarget = target.gameObject.GetComponent<Image> ();
		if (SaveManager.instance.state.isOnSound) {
			imgTarget.sprite = sprChangeFrame1;
			SoundManager.DontMuteSound ();
		} else {
			imgTarget.sprite = sprChangeFrame2;
			SoundManager.MuteSound ();
		}
	}

	public void CheckMusic() {
		Image imgTarget = target.gameObject.GetComponent<Image> ();
		if (SaveManager.instance.state.isOnMusic) {
			imgTarget.sprite = sprChangeFrame1;
			SoundManager.DontMuteBGM ();
		} else {
			imgTarget.sprite = sprChangeFrame2;
			SoundManager.MuteBGM ();
		}
	}

	public void CheckVoice() {
		Image imgTarget = target.gameObject.GetComponent<Image> ();
		if (SaveManager.instance.state.isOnVoice) {
			imgTarget.sprite = sprChangeFrame1;
			SoundManager.DontMuteVoice ();
		} else {
			imgTarget.sprite = sprChangeFrame2;
			SoundManager.MuteVoice ();
		}
	}

	public void CheckRing() {
		Image imgTarget = target.gameObject.GetComponent<Image> ();
		if (SaveManager.instance.state.isOnRing) {
			imgTarget.sprite = sprChangeFrame1;
			SaveManager.instance.state.isOnRing = true;
		}
		else {
			imgTarget.sprite = sprChangeFrame2;
			SaveManager.instance.state.isOnRing = false;
		}
		SaveManager.instance.Save ();
	}

	void ChangeColor() {
		lerpedColor = Color32.Lerp(color1, color2,  t);
		imgEff.color = lerpedColor;

		if (flag == true) {
			t -= Time.deltaTime * 2f;
			if (t < 0.01f)
				flag = false;
		} else {
			t += Time.deltaTime * 2f;
			if (t > 0.99f)
				flag = true;
		}
	}

	float timeLibraryScene = 0;
	void Move3DesGoDes1ToDes3() {
		if (changeDes1 == false) {
			target.position = Vector3.MoveTowards (target.position, pos1.position, speed * Time.deltaTime);
			if (target.position == pos1.position)
				changeDes1 = true;
		}
		if (changeDes1 && changeDes2 == false) {
			target.position = Vector3.MoveTowards (target.position, pos2.position, speed * Time.deltaTime);
			if (target.position == pos2.position)
				changeDes2 = true;
		}
		if (changeDes1 && changeDes2 && changeDes3 == false) {
			target.position = Vector3.MoveTowards (target.position, pos3.position, speed * Time.deltaTime);
			if (target.position == pos3.position) {
				if (isLibraryScene) {
					timeLibraryScene += Time.deltaTime;

					if (timeLibraryScene >= 0.05f) {
						backLibrarybtn.SetActive (true);
						timeLibraryScene = 0;
						changeDes3 = true;
					}
				} else {
					changeDes3 = true;
					if (Library.Instance.isClick)
						Library.Instance.isClick = false;
				}
			}
		}
	}

	[SerializeField]
	bool dontUseOffset;
	void Move3DesBackDes3ToDes1() {
		Vector3 newPos = target.position;

		if (dontUseOffset == false)
			newPos.y = -6f + offsetMoveY;
		else
			newPos = pos1.transform.position;

		if(target.position != newPos)
			target.position =  Vector3.MoveTowards (target.position, newPos, speed * Time.deltaTime);

		if(target.position == newPos)
			changeDes1 = changeDes2 = changeDes3 = false;
	}
		
	public void AllowScaleAni() {
		isRunScaleAni = true;
	}

	void ScaleToScale() {
		if (changeScale1 == false) {
			target.localScale = Vector3.MoveTowards (target.localScale, maxScale, speed * Time.deltaTime);
			if (target.localScale == maxScale)
				changeScale1 = true;
		}
		if (changeScale1 && changeScale2 == false) {
			target.localScale = Vector3.MoveTowards (target.localScale, originScale, speed * Time.deltaTime);
			if (target.localScale == originScale) {
				changeScale2 = true;
				if (isUIBotBar) {
					isRunScaleAni = false;
					changeScale1 = changeScale2 = false;
					HandleBotBar ();
				}
			}
		}
	}

	void HandleBotBar() {
		if (isShop) {
			CointainData.isRewardDailyQuest = false;
			fadeShop.isRunHide = true;
			fadeShop.stateFade = FadeAni.State.Show;
			fadeShop.changeToShopScene = true;
		}
		if (isRate) {
			if (!Rate.isDone)
				Rate.doing += 1;
			Application.OpenURL (linkRate);
		}
		if (isShare) {
			if (!Share.isDone)
				Share.doing += 1;
			Application.OpenURL (linkShare);
		}
		if (isLikeFacebook) {
			if (!LikeFB.isDone)
				LikeFB.doing += 1;
			Application.OpenURL (linkFB);
		}
		if (isLeaderboard) {
			LeaderboardManager.ShowLeaderboardUI();
		}
	}

	public void CloseShop() {
		fadeShop.isRunHide = true;
		fadeShop.stateFade = FadeAni.State.Show;
		fadeShop.changeToShopScene = false;
	}

	void ScaleToMin() {
		if(target.localScale != minScale)
			target.localScale =  Vector3.MoveTowards (target.localScale, minScale, speed * Time.deltaTime);
		if (target.localScale == minScale)
			changeScale1 = changeScale2 = false;
	}

	public void NextButtonModePlay() {

		if (indexMode >= btnContent.Length - 1)
			indexMode = -1;
		
		indexMode++;
		contentTxt.text = btnContent [indexMode];
	}

	public void NextBtnRunAni(){
		isRunBtnPlayAni = true;
		changeScale1 = false;
		changeScale2 = false;
		isRight = true;
		isLeft = false;
	}

	public void PreButtonModePlay() {
		indexMode--;
		if (indexMode < 0)
			indexMode = btnContent.Length - 1;
		
		contentTxt.text = btnContent [indexMode];
	}

	public void PreBtnRunAni() {
		isRunBtnPlayAni = true;
		changeScale1 = false;
		changeScale2 = false;
		isLeft = true;
		isRight = false;
	}

	void AniBtnNextOrPre() {
		if (isRight) {
			if (changeRight1 == false) {
				nextBtn.transform.localScale = Vector3.MoveTowards (nextBtn.transform.localScale, maxScale, speed * Time.deltaTime);
				if (nextBtn.transform.localScale == maxScale)
					changeRight1 = true;
			}
			if (changeRight1 && changeRight2 == false) {
				nextBtn.transform.localScale = Vector3.MoveTowards (nextBtn.transform.localScale, originScale, speed * Time.deltaTime);
				if (nextBtn.transform.localScale == originScale) {
					changeRight2 = true;
					isRunBtnPlayAni = false;
					changeRight1 = changeRight2 = false;
				}
			}
		} else {
			if (changeLeft1 == false) {
				preBtn.transform.localScale = Vector3.MoveTowards (preBtn.transform.localScale, new Vector3 (-maxScale.x, maxScale.y, 1), speed * Time.deltaTime);
				if (preBtn.transform.localScale == new Vector3 (-maxScale.x, maxScale.y, 1))
					changeLeft1 = true;
			}
			if (changeLeft1 && changeLeft2 == false) {
				preBtn.transform.localScale = Vector3.MoveTowards (preBtn.transform.localScale, new Vector3 (-originScale.x, originScale.y, 1), speed * Time.deltaTime);
				if (preBtn.transform.localScale == new Vector3 (-originScale.x, originScale.y, 1)) {
					changeLeft2 = true;
					isRunBtnPlayAni = false;
					changeLeft1 = changeLeft2 = false;
				}
			}
		}
	}

	void AniSequenceMoveGo() {
		obj1.position = Vector3.MoveTowards (obj1.position, pos1Seq.position, speed * Time.deltaTime);
		if (obj1.position.x < 5f)
			obj2.position = Vector3.MoveTowards (obj2.position, pos2Seq.position, speed * Time.deltaTime);
		if (obj2.position.x < 5f)
			obj3.position = Vector3.MoveTowards (obj3.position, pos3Seq.position, speed * Time.deltaTime);
		if (obj3.position == pos3Seq.position)
			progressBar.SetActive (true);
	}
		
	void AniSequenceMoveBack() {
		progressBar.SetActive (false);

		obj3.position = Vector3.MoveTowards (obj3.position, new Vector3 (20f, obj3.position.y, obj3.position.z), speed * Time.deltaTime);
		if (obj3.position.x > 15f)
			obj2.position = Vector3.MoveTowards (obj2.position, new Vector3 (20f, obj2.position.y, obj2.position.z), speed * Time.deltaTime);
		if (obj2.position.x > 15f)
			obj1.position = Vector3.MoveTowards (obj1.position, new Vector3 (20f, obj1.position.y, obj1.position.z), speed * Time.deltaTime);
	}

	public void ReturnPosHideQuest() {
		obj1.position = new Vector3 (20f, obj1.position.y, obj1.position.z);
		obj2.position = new Vector3 (20f, obj2.position.y, obj1.position.z);
		obj3.position = new Vector3 (20f, obj3.position.y, obj1.position.z);

		progressBar.SetActive (false);
	}

	void AniShake() {
		if (timeShake >= timeInterShake) {
			timeInterShake = 0.2f;
			timeShake = 0;
		} else
			timeShake += Time.deltaTime;

		target.Rotate (0, 0, speed);
		if (timeShake >= timeInterShake) {
			speed *= -1;
		}
	}

	public void RotateAni() {
		transform.Rotate (new Vector3 (0, 0, speed * Time.deltaTime));
	}

	float timeChangeRotate;
	float timeTotalRewardAni;
	float waitInX1Gatcha;

	void RewardAni() {
		timeTotalRewardAni += 0.05f;
		timeChangeRotate += Time.deltaTime;
		if (timeTotalRewardAni <= 1.5f) {
			if (timeChangeRotate >= 0.1f) {
				target.transform.eulerAngles = new Vector3 (0, 0, -3);
			}
			if (timeChangeRotate >= 0.2f) {
				target.transform.eulerAngles = new Vector3 (0, 0, 3);
				timeChangeRotate = 0;
			}
		}
		if (timeTotalRewardAni > 1.5f) {
			target.transform.eulerAngles = new Vector3 (0, 0, 0);
			if (lightReward.activeSelf && timeTotalRewardAni < 2.2f) {
				lightReward.transform.localScale = Vector3.MoveTowards (lightReward.transform.localScale, new Vector3 (20, 20, 0), speed * Time.deltaTime);
			}
			if (timeTotalRewardAni > 1.5f && timeTotalRewardAni <= 1.6f) {
				lightReward.SetActive (true);
				rewardImg.sprite = rewardSpr [1];
			}
			if (timeTotalRewardAni > 1.6f && timeTotalRewardAni <= 1.7f) {
				rewardImg.sprite = rewardSpr [2];
			}
			if (timeTotalRewardAni > 1.7f && timeTotalRewardAni <= 1.8f) {
				rewardImg.sprite = rewardSpr [3];
			}
			if (timeTotalRewardAni > 1.8f && timeTotalRewardAni < 1.9f) {
				rewardImg.sprite = rewardSpr [4];
			}
			if (timeTotalRewardAni > 1.9f && timeTotalRewardAni < 2f) {
				rewardImg.sprite = rewardSpr [5];
			}
			if (timeTotalRewardAni > 2f && timeTotalRewardAni < 2.1f) {
				rewardImg.sprite = rewardSpr [6];
			}
			if (timeTotalRewardAni > 2.1f && timeTotalRewardAni < 2.2f) {
				canvasReward.SetActive (true);
				fadeOpenReward.SetActive (true);
				lightReward.transform.localScale = new Vector3 (4, 4, 0);
			}

			if (timeTotalRewardAni >= 2.4f) {
				if (RewardManager.instance.isX1)
					lightX1Gatcha.SetActive (true);
				else
					lightX1Gatcha.SetActive (false);
				//isRunRewardAni = false;

				timeChangeRotate = 0;
				timeTotalRewardAni = 0;

				rewardImg.gameObject.SetActive (false);
				rewardImg.sprite = rewardSpr [0];
				fadeOpenReward.SetActive (false);

				isFinishFadeRewardAni = true;

				// change smoke reward diamond
			}
		}
	}

	[SerializeField]
	public int indexGatcha = 0;
	public bool AniGatchaCharacter;

	float timeDelayShowGatcha = 0;

	void GatchaAniX10() {
		if (indexGatcha < objectsRunAniGatcha.Length) {
			objectsRunAniGatcha [indexGatcha].SetActive (true);
			//lightX10Gatcha [indexGatcha].SetActive (true);

			timeDelayShowGatcha += Time.deltaTime;

			if (timeDelayShowGatcha > 0.5f) {
				if (objectsRunAniGatcha [indexGatcha].transform.position != posAni [indexGatcha].position) {
					objectsRunAniGatcha [indexGatcha].transform.position = Vector3.MoveTowards (objectsRunAniGatcha [indexGatcha].transform.position, posAni [indexGatcha].position, speed * Time.deltaTime);
					//lightX10Gatcha [indexGatcha].transform.position = objectsRunAniGatcha [indexGatcha].transform.position;
				}
				if (objectsRunAniGatcha [indexGatcha].transform.localScale != originScale) {
					objectsRunAniGatcha [indexGatcha].transform.localScale = Vector3.MoveTowards (objectsRunAniGatcha [indexGatcha].transform.localScale, originScale, posAni [indexGatcha].localScale.z * Time.deltaTime);
					//lightX10Gatcha [indexGatcha].transform.localScale = Vector3.MoveTowards (lightX10Gatcha [indexGatcha].transform.localScale, minScale, posAni[indexGatcha].localScale.y * Time.deltaTime);
				}
				if (objectsRunAniGatcha [indexGatcha].transform.position == posAni [indexGatcha].position) {
					if (indexGatcha == objectsRunAniGatcha.Length - 1) {
						if (AniGatchaCharacter) {
							for (int i = 0; i < 10; i++) {
								if (objectsRunAniGatcha [i].GetComponent<CointainData> ().dataChar.isOwned) {
									effectSmoke [i].transform.position = objectsRunAniGatcha [i].transform.position;
									effectSmoke [i].SetActive (true);
									charEquipManager.ChangeReward (i);
								} else
									objectsRunAniGatcha [i].GetComponent<CointainData> ().dataChar.isOwned = true;
							}
						} else {
							for (int i = 0; i < 10; i++) {
								if (objectsRunAniGatcha [i].GetComponent<CointainData> ().dataItem.isOwned) {
									effectSmoke [i].transform.position = objectsRunAniGatcha [i].transform.position;
									effectSmoke [i].SetActive (true);
									charEquipManager.ChangeRewardX10Equipment (i);
								} else
									objectsRunAniGatcha [i].GetComponent<CointainData> ().dataItem.isOwned = true;
							}
						}

						closeReward.enabled = true;
						RewardManager.instance.ShowBtnX10EndAni ();
					}
					indexGatcha++;
					timeDelayShowGatcha = 0;

				}
			}
		}
	}

	void EffectGatchaX10ScaleRun() {
		for (int i = 0; i < effectGlowStar.Length; i++) {
			var sh = effectGlowStar [i].shape;
			sh.scale = Vector3.MoveTowards (sh.scale, minScale, Time.deltaTime / 2f);
			if (sh.scale == minScale) {
				var em = effectGlowStar [i].emission;
				em.rateOverTime = 5;
			}
		}
	}

	public void EffectGatchaX10ScaleOff() {
		for (int i = 0; i < effectGlowStar.Length; i++) {
			var sh = effectGlowStar [i].shape;
			if (sh.scale != new Vector3 (0.5f, 1, 0.5f)) {
				sh.scale = new Vector3 (0.5f, 1, 0.5f);
				var em = effectGlowStar [i].emission;
				em.rateOverTime = 25;
			}
		}
	}

	public void ActiveRunFrameChangeBtn() {
		if (isRunAniBtnChange)
			isRunAniBtnChange = false;
		else
			isRunAniBtnChange = true;
	}

	void ScaleFrameChangeBtnTo(Vector3 desScale) {
		if (target.localScale != desScale)
			target.localScale = Vector3.MoveTowards (target.localScale, desScale, speed * Time.deltaTime);
	}

	public void ChangeIconFrameChangeBtn(int i) {
		GameObject goTemp = new GameObject ();

		btnChange.sprite = changeImgBtn [i];

		for (int j = 0; j < btnsChange.Length; j++) {
			if (btnChange.sprite == btnsChange [j].GetComponent<Image> ().sprite) {
				goTemp = btnsChange [j];
				btnsChange [j] = btnsChange [0];
				btnsChange [0] = goTemp;
			}
		}

		for (int j = 0; j < btnsChange.Length; j++)
			btnsChange[j].transform.position = posBtn[j].transform.position;

		for (int j = 0; j < typeChange.Length; j++) {
			if (j == i)
				typeChange [j].SetActive (true);
			else
				typeChange [j].SetActive (false);
		}
		ChooseCharManager.instance.chooseSymbol.SetActive (false);
		isRunAniBtnChange = true;
	}

	GameObject goTemp1;
	public void ChangeIconFrameChooseChar(int i) {

		btnChange.sprite = changeImgBtn [i];

		for (int j = 0; j < btnsChange.Length; j++) {
			if (btnChange.sprite == btnsChange [j].GetComponent<Image> ().sprite) {
				goTemp1 = btnsChange [j];
				btnsChange [j] = btnsChange [0];
				btnsChange [0] = goTemp1;
			}
		}

		for (int j = 0; j < btnsChange.Length; j++)
			btnsChange[j].transform.position = posBtn[j].transform.position;

		for (int j = 0; j < typeChange.Length; j++) {
			if (j == i)
				typeChange [j].SetActive (true);
			else
				typeChange [j].SetActive (false);
		}
		ChooseCharControl.instance.chooseSymbol.SetActive (false);
		isRunAniBtnChange = true;
	}

	bool isMoveScaleMin;
	void ScaleSequent() {
		if (isMoveScaleMin == false) {
			target.localScale = Vector3.MoveTowards (target.localScale, maxScale, speed * Time.deltaTime);
			if (target.localScale == maxScale)
				isMoveScaleMin = true;
		}
		if (isMoveScaleMin) {
			target.localScale = Vector3.MoveTowards (target.localScale, originScale, speed * Time.deltaTime);
			if (target.localScale == originScale)
				isMoveScaleMin = false;
		}
	}

	IEnumerator RunAni(){
		while (true) {
			//if (isRunAniBtnChange == false) {
				if (isRunChangeColorAni)
					ChangeColor ();
				if (isRunMoveAni)
					Move3DesGoDes1ToDes3 ();
				else if (isMoveAni)
					Move3DesBackDes3ToDes1 ();
				if (isRunScaleAni)
					ScaleToScale ();
				else if (isScaleAni)
					ScaleToMin ();
				if (isRunBtnPlayAni) {
					AniBtnNextOrPre ();
					ScaleToScale ();
				}
				if (isRunSeqAni)
					AniSequenceMoveGo ();
				else if (isSequence)
					AniSequenceMoveBack ();
				if (isRunShakeAni)
					AniShake ();
				else if (isShakeAni)
					target.eulerAngles = new Vector3 (0, 0, 0);
				if (isRunRotateAni)
					RotateAni ();
				if (isRunRewardAni)
					RewardAni ();
				if (isRunAniGatcha)
					GatchaAniX10 ();
				if (isRunEffX10Ani)
					EffectGatchaX10ScaleRun ();
				if (isRunAniBtnChange)
					ScaleFrameChangeBtnTo (maxScale);
				else if(isRunAniFrameChange)
					ScaleFrameChangeBtnTo (minScale);
			if (isScaleSequence)
				ScaleSequent ();
			//}
			yield return new WaitForSeconds (0.02f);
		}
	}
}
