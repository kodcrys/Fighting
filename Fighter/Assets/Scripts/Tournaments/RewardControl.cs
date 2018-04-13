using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardControl : MonoBehaviour {

	[Header("------Panel------")]
	[SerializeField]
	private GameObject receiveGift;

	[Header("------Reward------")]
	[SerializeField]
	private GameObject reward;
	[SerializeField]
	private float scaleSize;

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

	float localScaleSize;
	int currentRank;
	bool isScale;
	// Use this for initialization
	void OnEnable () 
	{
		currentRank = -1;
		isScale = false;
		localScaleSize = 0;

		for (int i = 0; i < listScore.Length; i++) 
		{
			if (SaveManager.instance.state.score >= listScore [i]) 
			{
				diamondtxt.text = listRecieveDiamond [i].ToString();
				goldtxt.text = listRecieveGold [i].ToString();
				currentRank = i;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (isScale) 
		{
			localScaleSize += Time.deltaTime;
			reward.transform.localScale = new Vector3 (localScaleSize, localScaleSize, 1);
			if (localScaleSize >= scaleSize)
				isScale = false;
		}
			
	}

	public void OnReceiveReward ()
	{
		isScale = true;
		SaveManager.instance.state.TotalGold += listRecieveGold [currentRank];
		SaveManager.instance.state.TotalDiamond += listRecieveGold [currentRank];
	}
}
