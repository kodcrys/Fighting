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

	// Use this for initialization
	void Start () 
	{
		//SaveManager.instance.state.currentMatch = 0;

		Debug.Log ("currentMatch" + " " + SaveManager.instance.state.currentMatch);
		for (int i = 0; i < SaveManager.instance.state.listPlayerMatch.Length; i++)
			Debug.Log ("Player " + i + " " + SaveManager.instance.state.listPlayerMatch [i]);

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

		if (SaveManager.instance.state.listPlayerMatch [SaveManager.instance.state.listPlayerMatch.Length-1] == 1) {
			findMatchPanel.SetActive (false);
			chooseCharPanel.SetActive (false);
			resultPanel.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
