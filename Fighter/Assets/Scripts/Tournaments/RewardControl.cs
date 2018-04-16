using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardControl : MonoBehaviour {

	[Header("------Reward------")]
	[SerializeField]
	GameObject canvasReward;
	[SerializeField]
	private GameObject reward;
	[SerializeField]
	private GameObject rewardObj;
	[SerializeField]
	GameObject fadeOpenReward;
	[SerializeField]
	private GameObject effReward;

	[Header("------List Reward------")]
	[SerializeField]
	private int[] listRecieveGold;
	[SerializeField]
	private int[] listRecieveDiamond;
	[SerializeField]
	private int[] listScore;

	[Header("------Text------")]
	[SerializeField]
	private UnityEngine.UI.Text diamondtxt;
	[SerializeField]
	private UnityEngine.UI.Text goldtxt;

	int currentRank;
	// Use this for initialization
	void OnEnable () 
	{
		currentRank = -1;
	}
	
	// Update is called once per frame
	void Update () {
			
	}

	public void OnReceiveReward ()
	{
		reward.SetActive (true);
		rewardObj.SetActive (true);
		effReward.SetActive (false);
		for (int i = 0; i < listScore.Length; i++) 
		{
			if (SaveManager.instance.state.score >= listScore [i]) 
			{
				diamondtxt.text = listRecieveDiamond [i].ToString();
				goldtxt.text = listRecieveGold [i].ToString();
				currentRank = i;
			}
		}

		if (currentRank >= 0) 
		{
			SaveManager.instance.state.TotalGold += listRecieveGold [currentRank];
			SaveManager.instance.state.TotalDiamond += listRecieveDiamond [currentRank];
		}
	}

	public void OnCloseReward ()
	{
		canvasReward.SetActive (false);
		reward.SetActive (false);
		fadeOpenReward.SetActive (false);
	}
}
