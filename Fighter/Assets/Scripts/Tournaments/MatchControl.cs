using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchControl : MonoBehaviour 
{
	[Header("------Panel------")]
	[SerializeField]
	private GameObject matchPanel;
	[Header("------Buttons------")]
	[SerializeField]
	private GameObject viewbtn;

	[SerializeField]
	private GameObject ready1btn;
	[SerializeField]
	private GameObject ready2btn;

	[SerializeField]
	private GameObject lock1btn;
	[SerializeField]
	private GameObject lock2btn;

	private bool isMe, isReady, isView;
	private float timeCount, timeAiReady;
	// Use this for initialization
	void OnEnable () 
	{
		isMe = false;
		isReady = false;
		isView = false;

		ready1btn.SetActive (true);
		ready2btn.SetActive (true);
		lock1btn.SetActive (true);
		lock2btn.SetActive (true);
		viewbtn.SetActive (true);

		if (isMe) 
		{
			viewbtn.SetActive (false);
			timeAiReady = Random.Range (3f, 5f);
		}
		else 
		{
			ready1btn.SetActive (false);
			ready2btn.SetActive (false);
			lock1btn.SetActive (false);
			lock2btn.SetActive (false);
		}

		timeCount = 0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (matchPanel.activeInHierarchy) 
		{
			if (isMe) 
			{
				if (!ready2btn.activeInHierarchy && !ready2btn.activeInHierarchy && isReady)
					matchPanel.SetActive (false);

				timeCount += Time.deltaTime;
				if (timeCount >= timeAiReady)
					ready2btn.SetActive (false);
			} 
			else 
			{
				if (!viewbtn.activeInHierarchy && isView)
					matchPanel.SetActive (false);
			}
		}
	}

	public void OnReadyForMatch ()
	{
		ready1btn.SetActive (false);
		isReady = true;
	}

	public void OnViewForMatch ()
	{
		viewbtn.SetActive (false);
		isView = true;
	}
}
