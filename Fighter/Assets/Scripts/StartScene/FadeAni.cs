using UnityEngine;

public class FadeAni : MonoBehaviour {

	[HideInInspector]
	public bool isRunHide;

	[HideInInspector]
	public bool isChangeMap;

	[HideInInspector]
	public bool isChangeChooseChar;

	public static bool isRunMapToChooseChar;
	public static bool isRunMapToHome;
	public static bool isRunPlayGame;

	public enum State {none, Show, Hide, Show1, Show2}
	[Header("State machine")]
	public State stateFade = State.none;

	[Header("Speed of ani")]
	[SerializeField]
	float speed;

	[Header("ObjectFade")]
	[SerializeField]
	Transform fade1;
	[SerializeField]
	Transform fade2;
	[SerializeField]
	UnityEngine.UI.Image fade;

	[Header("Position to move")]
	[SerializeField]
	Transform hidePosFade1;
	[SerializeField]
	Transform showPosFade1;
	[SerializeField]
	Transform showPos1Fade1;
	[SerializeField]
	Transform showPos2Fade1;
	[SerializeField]
	Transform hidePosFade2;
	[SerializeField]
	Transform showPosFade2;
	[SerializeField]
	Transform showPos1Fade2;
	[SerializeField]
	Transform showPos2Fade2;

	[HideInInspector]
	public bool changeToShopScene;
	[Header("ShopUI")]
	[SerializeField]
	GameObject panel_Shop;
	[SerializeField]
	GameObject btnOfTopBar;
	[SerializeField]
	GameObject panel_Character;
	[SerializeField]
	GameObject midBar;
	[SerializeField]
	GameObject botBar;

	[Header("Hide First")]
	[SerializeField]
	bool isRunHideFirst;
	float timeDelayHideFirst;

	bool isRunFadeHide;

	[SerializeField]
	bool isGameplay;

	float timeDelay = 0;

	// Use this for initialization
	void Start () {
		isRunFadeHide = false;
		isChangeMap = false;
		isChangeChooseChar = false;
		timeDelay = 0;
	}
	
	// Update is called once per frame
	void Update () {
		switch (stateFade) {
		case State.none:
			fade.enabled = false;
			if (!isGameplay)
				speed = 15;
			else
				speed = 500;
			break;
		case State.Show:
			fade.enabled = true;
			if (!isGameplay)
				speed = 15;
			else
				speed = 500;
			fade1.position = Vector3.MoveTowards (fade1.position, showPosFade1.position, speed * Time.deltaTime);
			fade2.position = Vector3.MoveTowards (fade2.position, showPosFade2.position, speed * Time.deltaTime);
			if (fade1.position == showPosFade1.position && fade2.position == showPosFade2.position) {
				timeDelay += Time.deltaTime;
				if (timeDelay > 0.25f) {
					stateFade = State.Show1;
					timeDelay = 0;
				}
			}
			break;
		case State.Show1:
			fade1.position = Vector3.MoveTowards (fade1.position, showPos1Fade1.position, speed * Time.deltaTime);
			fade2.position = Vector3.MoveTowards (fade2.position, showPos1Fade2.position, speed * Time.deltaTime);
			if (fade1.position == showPos1Fade1.position && fade2.position == showPos1Fade2.position) {
				timeDelay += Time.deltaTime;
				if (timeDelay > 0.25f) {
					stateFade = State.Show2;
					timeDelay = 0;
				}
			}
			break;
		case State.Show2:
			if (!isGameplay)
				speed = 100;
			else
				speed = 800;
			fade1.position = Vector3.MoveTowards (fade1.position, showPos2Fade1.position, speed * Time.deltaTime);
			fade2.position = Vector3.MoveTowards (fade2.position, showPos2Fade2.position, speed * Time.deltaTime);
			if (fade1.position == showPos2Fade1.position && fade2.position == showPos2Fade2.position) {
				if (isRunHide) {
					HandleShop ();
					timeDelay += Time.deltaTime;
					if (timeDelay > 0.5f) {
						stateFade = State.Hide;
						timeDelay = 0;
					}
				}
					
				if (isChangeMap)
					UnityEngine.SceneManagement.SceneManager.LoadScene ("ChooseMap");

				if (isChangeChooseChar)
					UnityEngine.SceneManagement.SceneManager.LoadScene ("StartScene");

				if (isRunPlayGame)
					UnityEngine.SceneManagement.SceneManager.LoadScene ("MainGameScene");
			}
			break;
		case State.Hide:
			if (isRunHideFirst) {
				if (timeDelayHideFirst < 0.5f)
					timeDelayHideFirst += Time.deltaTime;
				if (timeDelayHideFirst >= 0.5f) {
					timeDelayHideFirst = 0;
					isRunFadeHide = true;
				}
			} else
				isRunFadeHide = true;

			if (isRunFadeHide) {
				if (!isGameplay)
					speed = 15;
				else
					speed = 500;
				fade1.position = Vector3.MoveTowards (fade1.position, hidePosFade1.position, speed * Time.deltaTime);
				fade2.position = Vector3.MoveTowards (fade2.position, hidePosFade2.position, speed * Time.deltaTime);
				if (fade1.position == hidePosFade1.position && fade2.position == hidePosFade2.position) {
					fade.enabled = false;
					stateFade = State.none;
				}
			}
			break;
		}
	}

	public void OffFade() {
		fade1.position = showPos2Fade1.position;
		fade2.position = showPos2Fade2.position;
	}

	void HandleShop() {
		if (changeToShopScene) {
			btnOfTopBar.SetActive (false);
			panel_Shop.SetActive (true);
			panel_Character.SetActive (false);
			midBar.SetActive (false);
			botBar.SetActive (false);
		} else if (panel_Shop != null) {
			btnOfTopBar.SetActive (true);
			panel_Shop.SetActive (false);
			panel_Character.SetActive (true);
			midBar.SetActive (true);
			botBar.SetActive (true);
		}
	}
}
