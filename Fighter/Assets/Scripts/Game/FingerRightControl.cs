using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerRightControl : FingerBase {

	public static FingerRightControl instance;

	void Awake(){
		healthBar.Initialize ();
		staminaBar.Initialize ();
		redHealthBar.Initialize ();
		shieldBar.Initialize ();

		if (instance == null)
			instance = this;
	}

	// Use this for initialization
	void Start () {
		touch = true;
		changeColor = false;
		oneShotColor = false;
		stopTime = true;
		a = 0;
		staminaBar.MaxVal = 100;
		staminaBar.CurrentVal = 100;
		if (fuckingMode) {
			healthBar.MaxVal = maxHealth * 10;
			healthBar.CurrentVal = maxHealth * 10;
		} else {
			healthBar.MaxVal = maxHealth;
			healthBar.CurrentVal = maxHealth;
		}
		redHealthBar.MaxVal = maxHealth;
		redHealthBar.CurrentVal = maxHealth;
		if (SaveManager.instance.state.isShieldRight) {
			shieldBar.MaxVal = defend;
			shieldBar.CurrentVal = defend;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (SaveManager.instance.state.player2AI) {
			if (fuckingMode) {
				atk = enemyLeft.maxHealth + enemyLeft.defend;
			} 
		}

		switch (fingerAction) {
		case FingerState.Idel:
			DoIdel ();
			break;
		case FingerState.Atk:
			DoAtk ();
			break;
		case FingerState.Doing:
			DoingAtk ();
			break;
		case FingerState.Win:
			Win ();
			break;
		case FingerState.Death:
			Dead ();
			break;
		}

		if (doingSomething) {
			if (!enemyLeft.firstAtk && lastAtk) {
				lastAtk = false;
				fingerAction = FingerState.Atk;
			}
		} else {
			enemyLeft.fingerAminChanger = 0;
			if (!enemyLeft.lastAtk && !isAtk)
				fingerAction = FingerState.Idel;
		}

		if (AnimationText.canPlay) {
			if (firstAtk && enemyLeft.lastAtk) {
				isAtk = true;
				takeDame = true;
			} else {
				isAtk = false;
			}
		}

		if (healthBar.CurrentVal <= 0 || enemyLeft.healthBar.CurrentVal <= 0) {
			GameplayBase.instance.rightButton.SetActive (false);
			AnimationText.canPlay = false;
			if (stopTime) {
				GameplayBase.instance.mainCamera.orthographicSize = 4;
				fingerAction = FingerState.Doing;
				StartCoroutine (WhoDeadWhoWin (1f));
			}
		}


		if (changeColor) {
			finger.GetComponent<SpriteRenderer> ().color = new Color32 (255, 255, 255, 255);
			fingerAtk.GetComponent<SpriteRenderer> ().color = new Color32 (255, 255, 255, 255);
			fingerDown.GetComponent<SpriteRenderer> ().color = new Color32 (255, 255, 255, 255);
			hand.GetComponent<SpriteRenderer> ().color = new Color32 (255, 255, 255, 255);
			StartCoroutine (WaitChangeColor (0.001f));
		} else {
			finger.GetComponent<SpriteRenderer> ().color = new Color32 (255, 212, 179, 255);
			fingerAtk.GetComponent<SpriteRenderer> ().color = new Color32 (255, 212, 179, 255);
			fingerDown.GetComponent<SpriteRenderer> ().color = new Color32 (255, 212, 179, 255);
			hand.GetComponent<SpriteRenderer> ().color = new Color32 (255, 212, 179, 255);
		}
	}

	void FixedUpdate(){
		if (takeDame)
			StartCoroutine (WaitRedBlood (0.2f));
	}
		

	public override void DoIdel(){
		if (finger != null)
			finger.SetActive (true);
		if (fingerDown != null)
			fingerDown.SetActive (false);
		if (fingerAtk != null)
			fingerAtk.SetActive (false);

		touch = true;
		isAtk = false;
		firstAtk = false;
		lastAtk = false;
		changeColor = false;
		isHitOne = false;


		if (!enemyLeft.firstAtk) {
			if (staminaBar.CurrentVal < staminaBar.MaxVal) {
				staminaBar.CurrentVal += 2;
			} else if (staminaBar.CurrentVal >= staminaBar.MaxVal) {
				staminaBar.CurrentVal = staminaBar.MaxVal;
			}
		}

		if (fingerAminChanger == 0) {
			if (time >= timeInter) {
				time = 0;
			} else {
				time += Time.deltaTime;
			}

			if (time >= timeInter) {
				if (changeScale == 0)
					changeScale = 1;
				else
					changeScale = 0;
			}

			if (changeScale == 0) {
				finger.transform.localScale = Vector3.MoveTowards (finger.transform.localScale, new Vector3 (finger.transform.localScale.x, scale1, finger.transform.localScale.z), Time.deltaTime * speedScale);
				finger.transform.Rotate (finger.transform.localRotation.x, finger.transform.localRotation.y, rot1);
				finger.transform.localPosition = Vector3.MoveTowards (finger.transform.localPosition, new Vector3 (finger.transform.localPosition.x + pos1, finger.transform.localPosition.y, finger.transform.localPosition.z), Time.deltaTime * speedScale);
			} else {
				finger.transform.localScale = Vector3.MoveTowards (finger.transform.localScale, new Vector3 (finger.transform.localScale.x, scale2, finger.transform.localScale.z), Time.deltaTime * speedScale);
				finger.transform.Rotate (finger.transform.localRotation.x, finger.transform.localRotation.y, rot2);
				finger.transform.localPosition = Vector3.MoveTowards (finger.transform.localPosition, new Vector3 (finger.transform.localPosition.x - pos2, finger.transform.localPosition.y, finger.transform.localPosition.z), Time.deltaTime * speedScale);
			}
		} else if (fingerAminChanger == 1) {
			stunFinger.enabled = true;
		}
	}

	public override void DoAtk(){
//		ChangeStateAni (FingerState.Atk);
		if (!firstAtk && !enemyLeft.firstAtk) {
			firstAtk = true;
			enemyLeft.fingerAminChanger = 1;
			if(finger != null)
				finger.SetActive (false);
			if (fingerDown != null)
				fingerDown.SetActive (true);
			if (fingerAtk != null)
				fingerAtk.SetActive (false);

			int ranSound = Random.Range (0, 1);
			if (ranSound == 0)
				SoundManager.Miss1s.Play ();
			else
				SoundManager.Miss2s.Play ();

			fingerAction = FingerState.Doing;
		} else if(enemyLeft.firstAtk){
			lastAtk = true;
			if(finger != null)
				finger.SetActive (false);
			if (fingerDown != null)
				fingerDown.SetActive (false);
			if (fingerAtk != null)
				fingerAtk.SetActive (true);
			if (!isHitOne) {
				SoundManager.Hits.Play ();
				isHitOne = true;
			}
			fingerAction = FingerState.Doing;
		}
	}

	public override void DoingAtk(){
//		ChangeStateAni (FingerState.Doing);
		if (healthBar.CurrentVal > 0) {
			if (firstAtk) {
				if (!enemyLeft.lastAtk) {
					if (enemyLeft.staminaBar.CurrentVal > 0) {
						enemyLeft.staminaBar.CurrentVal -= 2;
					} else if (enemyLeft.staminaBar.CurrentVal <= 0) {
						enemyLeft.staminaBar.CurrentVal = 0;
					}

					if (staminaBar.CurrentVal < staminaBar.MaxVal) {
						staminaBar.CurrentVal += 2;
					} else if (staminaBar.CurrentVal >= staminaBar.MaxVal) {
						staminaBar.CurrentVal = staminaBar.MaxVal;
					}
				}

				if (isAtk) {
					if (!enemyLeft.doingSomething) {
						isAtk = false;
						touch = false;
						fingerAction = FingerState.Idel;
					}
				}
			} else if (lastAtk) {
				enemyLeft.isAtk = true;
				if (enemyLeft.healthBar.CurrentVal > 0) {
					CameraShake.instance.Shake ();
					if (!SaveManager.instance.state.isShieldLeft)
						enemyLeft.healthBar.CurrentVal -= atk;
					else {
						if (enemyLeft.shieldBar.CurrentVal > 0)
							enemyLeft.shieldBar.CurrentVal -= atk;
						else
							enemyLeft.healthBar.CurrentVal -= atk;
					}
					if (enemyLeft.changeColor == false)
						enemyLeft.changeColor = true;
				}

				if (doingSomething) {
					if (staminaBar.CurrentVal > 0) {
						staminaBar.CurrentVal -= 8;
						if (SaveManager.instance.state.isOnRing)
							Handheld.Vibrate ();
					} else if (staminaBar.CurrentVal <= 0) {
						staminaBar.CurrentVal = 0;
						enemyLeft.isAtk = false;
						enemyLeft.touch = false;
						fingerAction = FingerState.Atk;
						isAtk = false;
						if (enemyLeft.healthBar.CurrentVal > 1) {
							touch = true;
							isAtk = false;
							firstAtk = false;
							lastAtk = false;
							changeColor = false;
							enemyLeft.fingerAction = FingerState.Idel;
						}
					}
				} 
			}
		} else if (healthBar.CurrentVal <= 0) {
			finger.SetActive (false);
			fingerDown.SetActive (true);
			fingerAtk.SetActive (false);
		} else if (enemyLeft.healthBar.CurrentVal <= 0) {
			finger.SetActive (false);
			fingerDown.SetActive (false);
			fingerAtk.SetActive (true);
		}
	}

	public override void Win(){
//		ChangeStateAni (FingerState.Idel);

		finger.SetActive (true);
		fingerDown.SetActive (false);
		fingerAtk.SetActive (false);

		isAtk = true;
		firstAtk = false;
		lastAtk = false;

		StartCoroutine (WaitForNextRound (1.5f));

		if (time >= timeInter) {
			time = 0;
		} else {
			time += Time.deltaTime;
		}

		if (time >= timeInter) {
			if (changeScale == 0)
				changeScale = 1;
			else
				changeScale = 0;
		}

		if (changeScale == 0) {
			finger.transform.localScale = Vector3.MoveTowards (finger.transform.localScale, new Vector3 (finger.transform.localScale.x, scale1, finger.transform.localScale.z), Time.deltaTime * speedScale);
			finger.transform.Rotate (finger.transform.localRotation.x, finger.transform.localRotation.y, rot1);
			finger.transform.localPosition = Vector3.MoveTowards (finger.transform.localPosition, new Vector3 (finger.transform.localPosition.x + pos1, finger.transform.localPosition.y, finger.transform.localPosition.z), Time.deltaTime * speedScale);
		} else {
			finger.transform.localScale = Vector3.MoveTowards (finger.transform.localScale, new Vector3 (finger.transform.localScale.x, scale2, finger.transform.localScale.z), Time.deltaTime * speedScale);
			finger.transform.Rotate (finger.transform.localRotation.x, finger.transform.localRotation.y, rot2);
			finger.transform.localPosition = Vector3.MoveTowards (finger.transform.localPosition, new Vector3 (finger.transform.localPosition.x - pos2, finger.transform.localPosition.y, finger.transform.localPosition.z), Time.deltaTime * speedScale);
		}
	}

	public override void Dead(){

//		ChangeStateAni (FingerState.Death);

		finger.SetActive (false);
		fingerDown.SetActive (true);
		fingerAtk.SetActive (false);

		isAtk = true;
		firstAtk = false;
		lastAtk = false;

		if (time >= timeInter) {
			time = 0;
		} else {
			time += Time.deltaTime;
		}

		if (time >= timeInter) {
			if (changeScale == 0)
				changeScale = 1;
			else
				changeScale = 0;
		}

		if (changeScale == 0) {
			fingerDown.transform.localScale = Vector3.MoveTowards (fingerDown.transform.localScale, new Vector3 (fingerDown.transform.localScale.x, scale1, fingerDown.transform.localScale.z), Time.deltaTime * speedScale);
			fingerDown.transform.Rotate (fingerDown.transform.localRotation.x, fingerDown.transform.localRotation.y, rot1);
			fingerDown.transform.localPosition = Vector3.MoveTowards (fingerDown.transform.localPosition, new Vector3 (fingerDown.transform.localPosition.x + pos1, fingerDown.transform.localPosition.y, fingerDown.transform.localPosition.z), Time.deltaTime * speedScale);
		} else {
			fingerDown.transform.localScale = Vector3.MoveTowards (fingerDown.transform.localScale, new Vector3 (fingerDown.transform.localScale.x, scale2, fingerDown.transform.localScale.z), Time.deltaTime * speedScale);
			fingerDown.transform.Rotate (fingerDown.transform.localRotation.x, fingerDown.transform.localRotation.y, rot2);
			fingerDown.transform.localPosition = Vector3.MoveTowards (fingerDown.transform.localPosition, new Vector3 (fingerDown.transform.localPosition.x - pos2, fingerDown.transform.localPosition.y, fingerDown.transform.localPosition.z), Time.deltaTime * speedScale);
		}
	}

	public void ClickAtk(){
		doingSomething = true;
		if (touch) {
			if (!isAtk)
				fingerAction = FingerState.Atk;
		}
	}

	public void UnClickAtk(){
		touch = true;
		doingSomething = false;
		if (!isAtk)
			fingerAction = FingerState.Idel;
		enemyLeft.oneShotColor = false;
	}

	IEnumerator WhoDeadWhoWin(float time){
		yield return new WaitForSeconds (time);
		stopTime = false;
		GameplayBase.instance.zoomCamera = true;
		if (healthBar.CurrentVal <= 0) {
			healthBar.CurrentVal = 0;
			fingerAction = FingerState.Death;
		} else if (enemyLeft.healthBar.CurrentVal <= 0) {
			fingerAction = FingerState.Win;
		}
	}

	IEnumerator WaitForNextRound(float time){
		yield return new WaitForSeconds (time);
		a++;
		if (a == 1) {
			SaveManager.instance.state.winCountRight++;
			if (SaveManager.instance.state.winCountRight < 2) {
				if (!AnimationText.endRound) {
					SaveManager.instance.state.roundCount++;
					AnimationText.endRound = true;
				}
			} else {
				if (!AnimationText.endRound) {
					
					if (SaveManager.instance.state.whatMode == 1) {
						if (levelStatManager != null) {
							if (lvMain.level <= 19)
								levelStatManager.IncreaseExp (10);
							else
								levelStatManager.IncreaseExp ((int)(lvMain.expLevelUp [lvMain.level] / 50));
						}
						GameplayBase.instance.gameoverP2Panel.SetActive (true);

						if (!SaveManager.instance.state.isPurchaseRemoveAds) {
							LoadingScene.ggAdmobs.ShowInterstitial ();
							LoadingScene.ggAdmobs.RequestInterstitial ();
						}
					} else if (SaveManager.instance.state.whatMode == 2) 
					{
						if (SaveManager.instance.state.currentMatch <= 7) 
						{
							SaveManager.instance.state.listPlayerMatch [SaveManager.instance.state.currentMatch + 7] = SaveManager.instance.state.listPlayerMatch [(SaveManager.instance.state.currentMatch - 1) * 2 + 1];
							if (SaveManager.instance.state.listPlayerMatch [(SaveManager.instance.state.currentMatch - 1) * 2] == 1) 
							{
								SaveManager.instance.state.isLose = true;
								if (levelStatManager != null) {
									if (lvMain.level <= 19)
										levelStatManager.IncreaseExp (10);
									else
										levelStatManager.IncreaseExp ((int)(lvMain.expLevelUp [lvMain.level] / 50));
								}
							}
							GameplayBase.instance.aniFade.stateFade = FadeAni.State.Show; 
							FadeAni.isReturnTournament = true;
						}
					}
					AnimationText.endRound = true;
				}
			}
			SaveManager.instance.Save ();
		}
	}

	IEnumerator WaitChangeColor(float time){
		yield return new WaitForSeconds (time);
		if (changeColor)
			changeColor = false;
	}

	IEnumerator WaitRedBlood(float time){
		yield return new WaitForSeconds (time);
		redHealthBar.CurrentVal = healthBar.CurrentVal;
		takeDame = false;
	}

	public void ChangeCharPlayer() {

		maxHealth = lvMain.hpBonus;
		atk = lvMain.atkBonus;
		defend = lvMain.defBonus;

		if (GameplayBase.dataPlayer2 != null) {
			// skin Idle
			skin.sprite = GameplayBase.dataPlayer2.equipmentOfChar;
			skin.gameObject.SetActive (true);
//			Debug.Log (skin.gameObject);

			// skin AtkTop
			skinAtkTopSpr.sprite = GameplayBase.dataPlayer2.equipmentOfChar;
			skinAtkTopSpr.gameObject.SetActive (true);

			// skin AtkDown
			skinAtkDownSpr.sprite = GameplayBase.dataPlayer2.equipmentOfChar;
			skinAtkDownSpr.gameObject.SetActive (true);

			HideItems ();
		}
	}

	public void ChangeCharAI() {

		maxHealth = lvMain.hpBonus;
		atk = lvMain.atkBonus;
		defend = lvMain.defBonus;

		if (GameplayBase.dataAI != null) {
			// skin Idle
			skin.sprite = GameplayBase.dataAI.equipmentOfChar;
			skin.gameObject.SetActive (true);

			// skin AtkTop
			skinAtkTopSpr.sprite = GameplayBase.dataAI.equipmentOfChar;
			skinAtkTopSpr.gameObject.SetActive (true);

			// skin AtkDown
			skinAtkDownSpr.sprite = GameplayBase.dataAI.equipmentOfChar;
			skinAtkDownSpr.gameObject.SetActive (true);

			HideItems ();
		}
	}

	void HideItems() {
		hat.gameObject.SetActive (false);
		amor.gameObject.SetActive (false);
		amor1.gameObject.SetActive (false);
		amor2.gameObject.SetActive (false);
		weapon.gameObject.SetActive (false);
	}

	void HideSkin() {
		skin.gameObject.SetActive (false);
		skinAtkTopSpr.gameObject.SetActive (false);
		skinAtkDownSpr.gameObject.SetActive (false);
	}

	public void ChangeItemsPlayer() {
		
		if (GameplayBase.hatPlayer2 != null) {
			// hat idle
			hat.sprite = GameplayBase.hatPlayer2.avatar;
			hat.gameObject.SetActive (true);

			// hat AtkTop
			hatAtkTopSpr.sprite = GameplayBase.hatPlayer2.avatar;
			hatAtkTopSpr.gameObject.SetActive (true);

			// hat AtkDown
			hatAtkDownSpr.sprite = GameplayBase.hatPlayer2.avatar;
			hatAtkDownSpr.gameObject.SetActive (true);

			maxHealth += GameplayBase.hatPlayer2.HP;
			atk += GameplayBase.hatPlayer2.ATK;
			defend += GameplayBase.hatPlayer2.DEF;

			HideSkin ();
		}

		if (GameplayBase.amorPlayer2 != null) {
			FingerRightControl.instance.amor.sprite = GameplayBase.amorPlayer2.avatar;
			FingerRightControl.instance.amor1.sprite = GameplayBase.amorPlayer2.avatar;
			FingerRightControl.instance.amor2.sprite = GameplayBase.amorPlayer2.avatar;

			FingerRightControl.instance.amor.gameObject.SetActive (true);
			FingerRightControl.instance.amor1.gameObject.SetActive (true);
			FingerRightControl.instance.amor2.gameObject.SetActive (true);

			maxHealth += GameplayBase.amorPlayer2.HP;
			atk += GameplayBase.amorPlayer2.ATK;
			defend += GameplayBase.amorPlayer2.DEF;

			HideSkin ();
		}
			
		if (GameplayBase.wpPlayer2 != null) {
			FingerRightControl.instance.weapon.sprite = GameplayBase.wpPlayer2.avatar;
			FingerRightControl.instance.weapon.gameObject.SetActive (true);

			maxHealth += GameplayBase.wpPlayer2.HP;
			atk += GameplayBase.wpPlayer2.ATK;
			defend += GameplayBase.wpPlayer2.DEF;

			HideSkin ();
		}
	}

	public void ChangeItemsAI() {

		if (GameplayBase.hatAI != null) {
			// hat idle
			hat.sprite = GameplayBase.hatAI.avatar;
			hat.gameObject.SetActive (true);

			// hat AtkTop
			hatAtkTopSpr.sprite = GameplayBase.hatAI.avatar;
			hatAtkTopSpr.gameObject.SetActive (true);

			// hat AtkDown
			hatAtkDownSpr.sprite = GameplayBase.hatAI.avatar;
			hatAtkDownSpr.gameObject.SetActive (true);
			if (SaveManager.instance.state.whatMode == 1) {
				maxHealth += GameplayBase.hatAI.HP;
				atk += GameplayBase.hatAI.ATK;
				defend += GameplayBase.hatAI.DEF;
			} else {
				maxHealth += GameplayBase.hatAI.HP;
				atk += GameplayBase.hatAI.ATK;
				defend += GameplayBase.hatAI.DEF;
			}
			HideSkin ();
		}

		if (GameplayBase.amorAI != null) {
			amor.sprite = GameplayBase.amorAI.avatar;
			amor1.sprite = GameplayBase.amorAI.avatar;
			amor2.sprite = GameplayBase.amorAI.avatar;

			amor.gameObject.SetActive (true);
			amor1.gameObject.SetActive (true);
			amor2.gameObject.SetActive (true);

			if (SaveManager.instance.state.whatMode == 1) {
				maxHealth += GameplayBase.amorAI.HP;
				atk += GameplayBase.amorAI.ATK;
				defend += GameplayBase.amorAI.DEF;
			} else {
				maxHealth += GameplayBase.amorAI.HP;
				atk += GameplayBase.amorAI.ATK;
				defend += GameplayBase.amorAI.DEF;
			}
			HideSkin ();
		}

		if (GameplayBase.wpAI != null) {
			weapon.sprite = GameplayBase.wpAI.avatar;
			weapon.gameObject.SetActive (true);

			if (SaveManager.instance.state.whatMode == 1) {
				maxHealth += GameplayBase.wpAI.HP;
				atk += GameplayBase.wpAI.ATK;
				defend += GameplayBase.wpAI.DEF;
			} else {
				maxHealth += GameplayBase.wpAI.HP;
				atk += GameplayBase.wpAI.ATK;
				defend += GameplayBase.wpAI.DEF;
			}
			HideSkin ();
		}
	}

	void ChangeStateAni (FingerState state) {
		switch (state) {
		case FingerState.Idel:
			skinIdle.SetActive (true);
			skinAtkTop.SetActive (false);
			skinAtkDown.SetActive (false);
			break;
		case FingerState.Atk:
			if (firstAtk) {
				skinIdle.SetActive (false);
				skinAtkTop.SetActive (false);
				skinAtkDown.SetActive (true);
			} else {
				skinIdle.SetActive (false);
				skinAtkTop.SetActive (true);
				skinAtkDown.SetActive (false);
			}
			break;
		case FingerState.Doing:
			if (firstAtk) {
				skinIdle.SetActive (false);
				skinAtkTop.SetActive (false);
				skinAtkDown.SetActive (true);
			} else {
				skinIdle.SetActive (false);
				skinAtkTop.SetActive (true);
				skinAtkDown.SetActive (false);
			}
			break;
		case FingerState.Death:
			skinIdle.SetActive (false);
			skinAtkTop.SetActive (false);
			skinAtkDown.SetActive (true);
			break;
		}
	}
}
