using System.Collections;
using UnityEngine;

public class LoadingScene : MonoBehaviour {
	
	public static GoogleMobileAdsDemoScript ggAdmobs = new GoogleMobileAdsDemoScript();

	public static int isViewRewardAds;
	public static float minutesLastClick;
	public static float minutesWait;

	// Use this for initialization
	void Start () {
		isViewRewardAds = 0;
		minutesLastClick = 0;
		minutesWait = 300;
		ggAdmobs.RequestBanner ();
		ggAdmobs.RequestInterstitial ();
		ggAdmobs.RequestRewardBasedVideo ();
		StartCoroutine (Wait (Random.Range (3f, 5f)));
	}

	IEnumerator Wait(float time) {
		yield return new WaitForSeconds (time);
		ggAdmobs.ShowBanner ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("StartScene");
	}
}
