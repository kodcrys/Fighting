using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TournamentManager : MonoBehaviour {
	
	[Header("------Panel------")]
	[SerializeField]
	private GameObject findMatchPanel;
	[SerializeField]
	private GameObject boardGamePanel;

	// Use this for initialization
	void Start () 
	{
		//SaveManager.instance.state.currentMatch = 0;

		Debug.Log ("currentMatch" + " " + SaveManager.instance.state.currentMatch);
		for (int i = 0; i < SaveManager.instance.state.listPlayerMatch.Length; i++)
			Debug.Log ("Player " + i + " " + SaveManager.instance.state.listPlayerMatch [i]);
		
		if (SaveManager.instance.state.currentMatch == 7)
			SaveManager.instance.state.currentMatch = 0;

		if (SaveManager.instance.state.currentMatch == 0) 
		{
			findMatchPanel.SetActive (true);
			boardGamePanel.SetActive (false);
		} 
		else 
		{
			findMatchPanel.SetActive (false);
			boardGamePanel.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
