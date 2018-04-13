using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FindMatch : MonoBehaviour {
	[Header("------Panel------")]
	[SerializeField]
	private GameObject chartPanel;

	[SerializeField]
	private GameObject rewardPanel;

	[SerializeField]
	private GameObject characterPanel;

	[SerializeField]
	private GameObject findMatchPanel;

	[SerializeField]
	private GameObject ChooseCharPanel;

	[Header("------Move Panel------")]
	[SerializeField]
	private int speedMovePanel;

	[SerializeField]
	private Transform moveIn, moveOut;

	[Header("------Count Time------")]
	[SerializeField]
	private GameObject findMatchbtn;

	[SerializeField]
	private GameObject cancelbtn;

	[SerializeField]
	private UnityEngine.UI.Text timetxt;

	[SerializeField]
	private UnityEngine.UI.Text waitingtxt;

	[Header("------Medal and Score------")]
	[SerializeField]
	private UnityEngine.UI.Image currentMedal;
	[SerializeField]
	private int[] listPointRank;
	[SerializeField]
	private List<Sprite> listSpriteMedal;
	[SerializeField]
	private UnityEngine.UI.Text scoretxt;

	private bool isMoveIn, isMoveOut;
	private float timeJoinGame;
	private float timeCount;

	[SerializeField]
	CointainData tournamentQuest;

	[Header("------Gift------")]
	[SerializeField]
	private GameObject Giftbtn;
	[SerializeField]
	private UnityEngine.UI.Text timeLefttxt; 
	[SerializeField]
	private ulong msToWait = 604800000;

	private ulong lastGiftOpen;

	// Use this for initialization
	void OnEnable () {
		if (SaveManager.instance.state.weeklyTimeCountdown == null) 
		{
			SaveManager.instance.state.weeklyTimeCountdown = DateTime.Now.Ticks.ToString ();
			SaveManager.instance.Save ();
		}
		
		isMoveOut = false;
		isMoveIn = false;
		timeCount = 0;

		TournamentManager.runFade1Out = true;
		TournamentManager.checkRun = false;

		lastGiftOpen = ulong.Parse (SaveManager.instance.state.weeklyTimeCountdown);

		if (!IsCheatReady())
			Giftbtn.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
		FindMatchControl ();
		//CountTimeLeft ();
		ShowScoreAndMedal ();

		if (!Giftbtn.activeInHierarchy) 
		{
			if (IsCheatReady ()) {
				timeLefttxt.text = "0d 0h 0m";
				Giftbtn.SetActive (true);
				return;
			} 

			{
				// Set the timer
				ulong diff = ((ulong)DateTime.Now.Ticks - lastGiftOpen);

				ulong m = diff / TimeSpan.TicksPerMillisecond;

				float secondsLeft = (float)(msToWait - m) / 1000f;

				string r = "";

				// Days
				r += ((int)secondsLeft / 86400).ToString () + "d ";
				secondsLeft -= ((int)secondsLeft / 86400) * 86400;

				// Hours
				r+= ((int) secondsLeft / 3600).ToString () + "h ";
				secondsLeft -= ((int)secondsLeft / 3600) * 3600;

				// Minutes
				r += ((int) secondsLeft/ 60).ToString ("00") + "m";

				timeLefttxt.text = r;

			}
		}
	}

	public void OnChartPanel ()
	{
		chartPanel.SetActive (true);
		rewardPanel.SetActive (false);
	}

	public void OnRewardPanel ()
	{
		chartPanel.SetActive (false);
		rewardPanel.SetActive (true);
	}

	public void OnFindMatch ()
	{
		isMoveOut = true;
		timeCount = 0f;
		timeJoinGame = UnityEngine.Random.Range (2, 5);
		tournamentQuest.quest.doing += 1;
		findMatchbtn.SetActive (false);
		cancelbtn.SetActive (true);
	}

	public void OnCancelFindMatch ()
	{
		isMoveIn = true;
		findMatchbtn.SetActive (true);
		cancelbtn.SetActive (false);
	}

	void FindMatchControl ()
	{
		// Animation and action to find match
		if (isMoveOut) 
		{
			findMatchPanel.transform.position = Vector3.MoveTowards (findMatchPanel.transform.position, moveIn.position, speedMovePanel * Time.deltaTime);
			rewardPanel.transform.position = Vector3.MoveTowards (rewardPanel.transform.position, moveOut.position, speedMovePanel * Time.deltaTime);
			chartPanel.transform.position = Vector3.MoveTowards (chartPanel.transform.position, moveOut.position, speedMovePanel * Time.deltaTime);
		}

		if (rewardPanel.transform.position == moveOut.position && chartPanel.transform.position == moveOut.position) 
		{
			isMoveOut = false;
		}

		// Time to join match.
		if (findMatchPanel.transform.position == moveIn.position) 
		{
			if (timeCount < timeJoinGame) 
			{
				timeCount += Time.deltaTime;
				if (Mathf.RoundToInt (timeCount) < 10)
					timetxt.text = "00: 0" + Mathf.RoundToInt (timeCount).ToString ();
				else
					timetxt.text = "00: " + Mathf.RoundToInt (timeCount).ToString ();

				if (Mathf.RoundToInt (timeCount) % 3 == 0)
					waitingtxt.text = ("Waiting for other players.");
				else if (Mathf.RoundToInt (timeCount) % 3 == 1)
					waitingtxt.text = ("Waiting for other players..");
				else
					waitingtxt.text = ("Waiting for other players...");
			} 
			else 
			{
				if (!TournamentManager.checkRun) 
				{
					TournamentManager.runFade1In = true;
					TournamentManager.checkRun = true;
				}
				SaveManager.instance.state.currentMatch = 0;
				SaveManager.instance.state.countWinMatch = 0;
				for (int i = 0; i < SaveManager.instance.state.listPlayerMatch.Length; i++)
					SaveManager.instance.state.listPlayerMatch [i] = 0;

				for (int i = 0; i < 8; i++)
					SaveManager.instance.state.listPlayerMatch [i] = i+1;
				
				for (int i = 1; i < 8; i++) 
				{
					SaveManager.instance.state.iconChar [i] = UnityEngine.Random.Range (0, 61);
					SaveManager.instance.Save ();
				}

				if (TournamentManager.runFade1In == false) 
				{
					chartPanel.SetActive (false);
					rewardPanel.SetActive (false);
					characterPanel.SetActive (false);
					findMatchPanel.SetActive (false);
					ChooseCharPanel.SetActive (true);
					transform.gameObject.SetActive (false);
					TournamentManager.runFade1Out = true;
					TournamentManager.checkRun = false;
				}
			}
		}
			
		// Animation and action to cancel find match
		if (isMoveIn) 
		{
			findMatchPanel.transform.position = Vector3.MoveTowards (findMatchPanel.transform.position, moveOut.position, speedMovePanel * Time.deltaTime);
			rewardPanel.transform.position = Vector3.MoveTowards (rewardPanel.transform.position, moveIn.position, speedMovePanel * Time.deltaTime);
			chartPanel.transform.position = Vector3.MoveTowards (chartPanel.transform.position, moveIn.position, speedMovePanel * Time.deltaTime);
		}

		if (rewardPanel.transform.position == moveIn.position && chartPanel.transform.position == moveIn.position) 
		{
			isMoveIn = false;
		}
	}

	public void CountTimeLeft ()
	{
		lastGiftOpen = (ulong)DateTime.Now.Ticks;
		SaveManager.instance.state.weeklyTimeCountdown = lastGiftOpen.ToString ();
		Giftbtn.SetActive (false);
		/*	if (8 - (int)System.DateTime.Now.DayOfWeek <= 7)
			timeLefttxt.text = 8 - (int)System.DateTime.Now.DayOfWeek + (" Days left");
		else
			timeLefttxt.text = "1 Day left"; */

	}

	private bool IsCheatReady ()
	{
		ulong diff = ((ulong)DateTime.Now.Ticks - lastGiftOpen);

		ulong m = diff / TimeSpan.TicksPerMillisecond;

		float secondsLeft = (float)(msToWait - m) / 1000f;
	
		if (secondsLeft < 0)
		{
			return true;
		}

		return false;
	}

	void ShowScoreAndMedal ()
	{
		scoretxt.text = SaveManager.instance.state.score.ToString ();

		for (int i = 0; i < listPointRank.Length; i++)
			if (SaveManager.instance.state.score >= listPointRank [i])
				currentMedal.sprite = listSpriteMedal [i];
	}
}
