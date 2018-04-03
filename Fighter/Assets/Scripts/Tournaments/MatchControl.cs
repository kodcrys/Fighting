using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchControl : MonoBehaviour {
	[SerializeField]
	private GameObject matchPanel;
	[SerializeField]
	private GameObject viewBtn, Ready1btn, Ready2btn; 

	private bool isMe;
	private int timeAiReady;
	private float timeCount;
	// Use this for initialization
	void OnEnable () 
	{
		isMe = true;
		Ready1btn.SetActive (true);
		Ready2btn.SetActive (true);
		viewBtn.SetActive (true);

		if (isMe) 
		{
			viewBtn.SetActive (false);
		}
		else 
		{
			Ready1btn.SetActive (false);
			Ready2btn.SetActive (false);
			timeAiReady = Random.Range (2, 4);
		}

		timeCount = 0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeCount += Time.deltaTime;
		if (timeCount >= timeAiReady)
			Ready2btn.SetActive (false);

		if (!Ready2btn.activeInHierarchy && !Ready2btn.activeInHierarchy)
			matchPanel.SetActive (false);
	}

	public void OnReadyForMatch ()
	{
		Ready1btn.SetActive (false);
	}
}
