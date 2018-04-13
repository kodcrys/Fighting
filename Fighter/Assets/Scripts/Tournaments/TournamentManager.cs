using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TournamentManager : MonoBehaviour {
	
	[Header("------Panel------")]
	[SerializeField]
	private GameObject findMatchPanel;
	[SerializeField]
	private GameObject chooseCharPanel;
	[SerializeField]
	private GameObject boardGamePanel;
	[SerializeField]
	private GameObject resultPanel;
	[SerializeField]
	private GameObject matchPanel;

	// Use this for initialization
	void Start () 
	{
		//SaveManager.instance.state.currentMatch = 0;

		Debug.Log ("count Win " + SaveManager.instance.state.countWinMatch + " " + SaveManager.instance.state.isLose);


		matchPanel.SetActive (false);

		if (SaveManager.instance.state.currentMatch == 0) 
		{
			findMatchPanel.SetActive (true);
			boardGamePanel.SetActive (false);
			chooseCharPanel.SetActive (false);
			SaveManager.instance.state.isLose = false;
		} 
		else 
		{
			if (SaveManager.instance.state.listPlayerMatch [(SaveManager.instance.state.currentMatch) * 2] == 1) {
				findMatchPanel.SetActive (false);
				chooseCharPanel.SetActive (true);
				boardGamePanel.SetActive (false);
			}
			else 
			{
				findMatchPanel.SetActive (false);
				chooseCharPanel.SetActive (false);
				boardGamePanel.SetActive (true);
			}
		}

		if (SaveManager.instance.state.listPlayerMatch [SaveManager.instance.state.listPlayerMatch.Length-1] == 1 || SaveManager.instance.state.isLose) {
			findMatchPanel.SetActive (false);
			chooseCharPanel.SetActive (false);
			boardGamePanel.SetActive (false);
			resultPanel.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
