using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationText : MonoBehaviour {

	public enum TextAnim {none, RoundText, KOText, FightText}

	public TextAnim textAnimState = TextAnim.none;

	public enum TextStepAnim {none, Begin, Doing, End}

	public TextStepAnim textStepAnim = TextStepAnim.none;

	[SerializeField]
	int step;

	[SerializeField]
	float time, timeInter, timeEnd;

	[SerializeField]
	Transform one, two, oneDes, twoDes;

	[SerializeField]
	float speed;

	[SerializeField]
	GameObject boomPrefab;

	public bool startAnim;

	public static bool FightAnim;
	public static bool canPlay;
	public static bool beginRound;
	public static bool endRound;

	void Start(){
		startAnim = false;
		beginRound = true;
		startAnim = true;
		endRound = false;
		FightAnim = false;
	}

	// Update is called once per frame
	void Update () {
		switch (textStepAnim) {
		case TextStepAnim.none:
			if (textAnimState == TextAnim.RoundText) {
				if (beginRound)
					canPlay = false;
				transform.GetChild (0).gameObject.SetActive (false);
				transform.GetChild (1).gameObject.SetActive (false);
				transform.GetChild (2).gameObject.SetActive (false);
				if (startAnim) {
					textStepAnim = TextStepAnim.Begin;
				}
			} else if (textAnimState == TextAnim.KOText) {
				transform.GetChild (0).gameObject.SetActive (false);
				transform.GetChild (1).gameObject.SetActive (false);
				transform.GetChild (2).gameObject.SetActive (false);
				transform.GetChild (3).gameObject.SetActive (false);
				if (endRound) {
					canPlay = false;
					textStepAnim = TextStepAnim.Begin;
				}
			} else if (textAnimState == TextAnim.FightText) {
				transform.GetChild (0).gameObject.SetActive (false);
				transform.GetChild (1).gameObject.SetActive (false);
				transform.GetChild (2).gameObject.SetActive (false);
				transform.GetChild (3).gameObject.SetActive (false);
				if (FightAnim) {
					textStepAnim = TextStepAnim.Begin;
				}
			}
			break;
		case TextStepAnim.Begin:
			BeginAnimText ();
			textStepAnim = TextStepAnim.Doing;
			break;
		case TextStepAnim.Doing:
			DoingAnimText ();
			break;
		case TextStepAnim.End:
			EndAnimText ();
			break;
		}
	}

	void BeginAnimText(){
		if (textAnimState == TextAnim.RoundText) {
			if (SaveManager.instance.state.roundCount <= 2) {
				transform.GetChild (0).gameObject.SetActive (true);
				transform.GetChild (1).gameObject.SetActive (true);
				transform.GetChild (2).gameObject.SetActive (false);
			} else {
				transform.GetChild (0).gameObject.SetActive (false);
				transform.GetChild (1).gameObject.SetActive (true);
				transform.GetChild (2).gameObject.SetActive (true);
			}
			transform.localScale = new Vector3 (3, 3, 3);
			step = 0;
		} else if (textAnimState == TextAnim.KOText) {
			transform.GetChild (0).gameObject.SetActive (true);
			transform.GetChild (1).gameObject.SetActive (true);
			transform.GetChild (2).gameObject.SetActive (true);
			transform.GetChild (3).gameObject.SetActive (true);
			one.localPosition = new Vector3 (-1500, 800, 0);
			two.localPosition = new Vector3 (1500, -800, 0);
			step = 0;
		} else if (textAnimState == TextAnim.FightText) {
			transform.GetChild (0).gameObject.SetActive (true);
			transform.GetChild (1).gameObject.SetActive (true);
			transform.GetChild (2).gameObject.SetActive (true);
			transform.GetChild (3).gameObject.SetActive (true);
			one.localPosition = new Vector3 (-2000, -100, 0);
			two.localPosition = new Vector3 (2000, -100, 0);
			step = 0;
		}
	}

	void DoingAnimText(){
		if (textAnimState == TextAnim.RoundText) {
			if (step == 0) {
				transform.localScale = Vector3.MoveTowards (transform.localScale, new Vector3 (0.9f, 0.9f, 0.9f), Time.deltaTime * 6);
				if (transform.localScale == new Vector3 (0.9f, 0.9f, 0.9f))
					step = 1;
			} else if (step == 1) {
				transform.localScale = Vector3.MoveTowards (transform.localScale, new Vector3 (1, 1, 1), Time.deltaTime * 5);
				if (transform.localScale == new Vector3 (1, 1, 1))
					step = 2;
			} else if (step == 2) {
				transform.eulerAngles = new Vector3 (0, 0, -10);
				time += Time.deltaTime;
				if (time >= timeInter) {
					step = 3;
					time = 0;
				}
			} else if(step == 3) {
				transform.localPosition = Vector3.MoveTowards (transform.localPosition, new Vector3 (0, -1500, 0), Time.deltaTime * speed);
				if (transform.localPosition == new Vector3 (0, -1500, 0)) {
					textStepAnim = TextStepAnim.End;
				}
			}
		} else if (textAnimState == TextAnim.KOText) {
			if (step == 0) {
				one.localPosition = Vector3.MoveTowards (one.localPosition, new Vector3 (-150, -20, 0), Time.deltaTime * speed);
				two.localPosition = Vector3.MoveTowards (two.localPosition, new Vector3 (100, -200, 0), Time.deltaTime * speed);
				if (one.localPosition == new Vector3 (-150, -20, 0)) {
					step = 1;
				}
			} else if (step == 1) {
				one.localPosition = Vector3.MoveTowards (one.localPosition, oneDes.localPosition, Time.deltaTime * speed);
				two.localPosition = Vector3.MoveTowards (two.localPosition, twoDes.localPosition, Time.deltaTime * speed);
				if (one.localPosition == oneDes.localPosition) {
					textStepAnim = TextStepAnim.End;
				}
			}
		} else if (textAnimState == TextAnim.FightText) {
			if (step == 0) {
				one.localPosition = Vector3.MoveTowards (one.localPosition, new Vector3 (-250, -100, 0), Time.deltaTime * speed);
				two.localPosition = Vector3.MoveTowards (two.localPosition, new Vector3 (250, -100, 0), Time.deltaTime * speed);
				if (one.localPosition == new Vector3 (-250, -100, 0)) {
					Instantiate (boomPrefab, new Vector3 (0, -1, 0), Quaternion.identity);
					step = 1;
				}
			} else if (step == 1) {
				one.localPosition = Vector3.MoveTowards (one.localPosition, oneDes.localPosition, Time.deltaTime * speed);
				two.localPosition = Vector3.MoveTowards (two.localPosition, twoDes.localPosition, Time.deltaTime * speed);
				time += Time.deltaTime;
				if (time >= timeInter) {
					textStepAnim = TextStepAnim.End;
					time = 0;
				}
			}
		}
	}

	void EndAnimText(){
		if (textAnimState == TextAnim.RoundText) {
			startAnim = false;
			textStepAnim = TextStepAnim.none;
			FightAnim = true;
			beginRound = false;
			startAnim = false;
		} else if (textAnimState == TextAnim.KOText) {
			time += Time.deltaTime;
			if (time >= timeEnd) {
				if (SaveManager.instance.state.winCountLeft >= 2 || SaveManager.instance.state.winCountRight >= 2) {
					endRound = false;
					textStepAnim = TextStepAnim.none;
				} else {
					UnityEngine.SceneManagement.SceneManager.LoadScene ("MainGameScene");
					endRound = false;
					textStepAnim = TextStepAnim.none;
				}
				time = 0;
			}
		} else if (textAnimState == TextAnim.FightText) {
			canPlay = true;
			FightAnim = false;
			textStepAnim = TextStepAnim.none;
		}
	}
}
