using System.Collections;
using UnityEngine;

public class LoadingScene : MonoBehaviour {
	
	public GoogleMobileAdsDemoScript ggAdmob;
	public static GoogleMobileAdsDemoScript ggAdmobs;

	public static int isViewRewardAds;
	public static float minutesLastClick;
	public static float minutesWait;

	[SerializeField]
	bool isTestGame;

	// Use this for initialization
	void Start () {
		SaveManager.instance.state.firstInGame = false;
		SaveManager.instance.Save ();
		ggAdmob = GameObject.Find ("GGAmobs").GetComponent<GoogleMobileAdsDemoScript> ();
		ggAdmobs = ggAdmob;

		isViewRewardAds = 0;
		minutesLastClick = 0;
		minutesWait = 300;
		ggAdmobs.RequestBanner ();
		ggAdmobs.RequestInterstitial ();
		ggAdmobs.RequestRewardBasedVideo ();
		if(!isTestGame)
			StartCoroutine (Wait (Random.Range (3f, 5f)));
		else
			StartCoroutine (Wait (0));
	}

	IEnumerator Wait(float time) {
		yield return new WaitForSeconds (time);
		ggAdmobs.ShowBanner ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("StartScene");
	}
}
