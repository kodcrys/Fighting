﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
	private GameObject MatchPanel;

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
	private UnityEngine.UI.Text timeLefttxt;

	[SerializeField]
	private UnityEngine.UI.Text timetxt;

	[SerializeField]
	private UnityEngine.UI.Text waitingtxt;

	private bool isMoveIn, isMoveOut;
	private float timeJoinGame;
	private float timeCount;


	// Use this for initialization
	void Start () {
		isMoveOut = false;
		isMoveIn = false;
		timeCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		FindMatchControl ();
		CountTimeLeft ();	
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
		timeJoinGame = Random.Range (1, 3);
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
				chartPanel.SetActive (false);
				rewardPanel.SetActive (false);
				characterPanel.SetActive (false);
				findMatchPanel.SetActive (false);
				MatchPanel.SetActive (true);
				transform.gameObject.SetActive (false);
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

	void CountTimeLeft ()
	{
		if (8 - (int)System.DateTime.Now.DayOfWeek <= 7)
			timeLefttxt.text = 8 - (int)System.DateTime.Now.DayOfWeek + (" Days left");
		else
			timeLefttxt.text = "1 Day left";
	}
}