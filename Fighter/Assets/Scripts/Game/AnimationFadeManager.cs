using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationFadeManager : MonoBehaviour {

	[SerializeField]
	FadeAnimOption fadeOption;

	void Start(){
		if(fadeOption.starDead != null)
			fadeOption.starDead.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if (AnimationText.canPlay)
			DoEmoji ();

		if (fadeOption.leftControl != null) {
			if (fadeOption.leftControl.fingerAction == FingerBase.FingerState.Idel) {
				if (!fadeOption.isEmojiLeft) {
					fadeOption.time += Time.deltaTime;
					if (fadeOption.time >= fadeOption.timeInter) {
						fadeOption.time = 0;
						if (fadeOption.i == 0) {
							fadeOption.i = 1;
							fadeOption.timeInter = 0.1f;
						} else {
							fadeOption.i = 0;
							fadeOption.timeInter = 3.5f;
						}
					}
					if (fadeOption.isUI == false)
						fadeOption.fadeLocation [0].sprite = fadeOption.fadeAnimOption [fadeOption.i];
					else
						fadeOption.faceList [0].sprite = fadeOption.fadeAnimOption [fadeOption.i];
				}
			} else if (fadeOption.leftControl.fingerAction == FingerBase.FingerState.Doing) {
				if (fadeOption.leftControl.firstAtk) {
					fadeOption.fadeLocation [1].sprite = fadeOption.fadeAnimOption [2];
				} else if (fadeOption.leftControl.firstAtk && fadeOption.leftControl.enemyRight.lastAtk) {
					fadeOption.fadeLocation [1].sprite = fadeOption.fadeAnimOption [3];
				} else if (fadeOption.leftControl.lastAtk) {
					fadeOption.fadeLocation [2].sprite = fadeOption.fadeAnimOption [2];
				}
			} else if (fadeOption.leftControl.fingerAction == FingerBase.FingerState.Win) {
				fadeOption.fadeLocation [0].sprite = fadeOption.fadeAnimOption [4];
			} else if (fadeOption.leftControl.fingerAction == FingerBase.FingerState.Death) {
				if(fadeOption.starDead != null)
					fadeOption.starDead.SetActive (true);
				fadeOption.fadeLocation [1].sprite = fadeOption.fadeAnimOption [5];
			}
		}else if (fadeOption.rightControl != null) {
			if (fadeOption.rightControl.fingerAction == FingerBase.FingerState.Idel) {
				if (!fadeOption.isEmojiRight) {
					fadeOption.time += Time.deltaTime;
					if (fadeOption.time >= fadeOption.timeInter) {
						fadeOption.time = 0;
						if (fadeOption.i == 0) {
							fadeOption.i = 1;
							fadeOption.timeInter = 0.1f;
						} else {
							fadeOption.i = 0;
							fadeOption.timeInter = 3.5f;
						}
					}
					if (fadeOption.isUI == false)
						fadeOption.fadeLocation [0].sprite = fadeOption.fadeAnimOption [fadeOption.i];
					else
						fadeOption.faceList [0].sprite = fadeOption.fadeAnimOption [fadeOption.i];
				}
			} else if (fadeOption.rightControl.fingerAction == FingerBase.FingerState.Doing) {
				if (fadeOption.rightControl.firstAtk) {
					fadeOption.fadeLocation [1].sprite = fadeOption.fadeAnimOption [2];
				} else if (fadeOption.rightControl.firstAtk && fadeOption.rightControl.enemyLeft.lastAtk) {
					fadeOption.fadeLocation [1].sprite = fadeOption.fadeAnimOption [3];
				} else if (fadeOption.rightControl.lastAtk) {
					fadeOption.fadeLocation [2].sprite = fadeOption.fadeAnimOption [2];
				}
			}else if (fadeOption.rightControl.fingerAction == FingerBase.FingerState.Win) {
				fadeOption.fadeLocation [0].sprite = fadeOption.fadeAnimOption [4];
			} else if (fadeOption.rightControl.fingerAction == FingerBase.FingerState.Death) {
				if(fadeOption.starDead != null)
					fadeOption.starDead.SetActive (true);
				fadeOption.fadeLocation [1].sprite = fadeOption.fadeAnimOption [5];
			}
		}
	}

	void DoEmoji(){
		if (fadeOption.leftControl != null) {
			if (!fadeOption.leftControl.doingSomething) {
				fadeOption.timeEmoji += Time.deltaTime;
				if (fadeOption.timeEmoji >= fadeOption.timeInterEmoji) {
					fadeOption.timeEmoji = fadeOption.timeInterEmoji;
					if (!fadeOption.isEmojiLeft)
						fadeOption.randomCount = UnityEngine.Random.Range (0, fadeOption.ranEmoji.Count - 1);
					fadeOption.isEmojiLeft = true;
				}
			} else {
				fadeOption.timeEmoji = 0;
				fadeOption.isEmojiLeft = false;
			}
		} else if (fadeOption.rightControl != null) {
			if (!fadeOption.rightControl.doingSomething) {
				fadeOption.timeEmoji += Time.deltaTime;
				if (fadeOption.timeEmoji >= fadeOption.timeInterEmoji) {
					fadeOption.timeEmoji = fadeOption.timeInterEmoji;
					if (!fadeOption.isEmojiRight)
						fadeOption.randomCount = UnityEngine.Random.Range (0, fadeOption.ranEmoji.Count - 1);
					fadeOption.isEmojiRight = true;
				}
			} else {
				fadeOption.timeEmoji = 0;
				fadeOption.isEmojiRight = false;
			}
		}

		if (fadeOption.isEmojiLeft || fadeOption.isEmojiRight) {
			if (fadeOption.leftControl) {
				fadeOption.fadeLocation [0].transform.localRotation = new Quaternion (0, -180, 0, 0);
			}
			fadeOption.fadeLocation [0].sprite = fadeOption.fadeAnimOption [6];
			fadeOption.emoji.SetActive (true);
			fadeOption.ranEmoji [fadeOption.randomCount].SetActive (true);
		} else {
			if (fadeOption.leftControl) {
				fadeOption.fadeLocation [0].transform.localRotation = new Quaternion (0, 0, 0, 0);
			}
			fadeOption.emoji.SetActive (false);
			fadeOption.ranEmoji [fadeOption.randomCount].SetActive (false);
		}
	}
}

[Serializable]
public class FadeAnimOption{
	public bool isUI = false;
	public List<Sprite> fadeAnimOption = new List<Sprite>();
	public List<SpriteRenderer> fadeLocation = new List<SpriteRenderer>();
	public List<UnityEngine.UI.Image> faceList = new List<UnityEngine.UI.Image>();
	public FingerLeftControl leftControl;
	public FingerRightControl rightControl;
	public float time, timeInter;
	public float timeEmoji, timeInterEmoji;
	public int i;
	public GameObject starDead;
	public bool isEmojiLeft, isEmojiRight;
	public GameObject emoji;
	public List<GameObject> ranEmoji;
	public int randomCount;
}
