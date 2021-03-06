﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGameControl : MonoBehaviour {

	[Header("------Panel------")]
	[SerializeField]
	private GameObject matchPanel;

	[Header("------Icon------")]
	[SerializeField]
	private List<Sprite> listSpriteMask;
	[SerializeField]
	private List<UnityEngine.UI.Image> maskIcons;
	[SerializeField]
	private List<GameObject> showIcon;

	// Use this for initialization
	void OnEnable () {
		
		for (int i = 0; i < maskIcons.Count; i++) 
		{
			if (SaveManager.instance.state.listPlayerMatch [i] - 1 >= 0) 
			{
				maskIcons [i].sprite = listSpriteMask [SaveManager.instance.state.iconChar [SaveManager.instance.state.listPlayerMatch [i] - 1]];
				showIcon [i].SetActive (false);
			}
			else
				showIcon [i].SetActive (true);
		}

		TournamentManager.runFade1Out = true;
		TournamentManager.checkRun = false;
	}

	public void OnSceneMatch ()
	{
		SaveManager.instance.state.currentMatch++;
		SaveManager.instance.Save ();

		if (!TournamentManager.checkRun) 
		{
			TournamentManager.runFade1In = true;
			TournamentManager.checkRun = true;
		}

	}

	// Update is called once per frame
	void Update () 
	{
		if (TournamentManager.checkRun && !TournamentManager.runFade1In && !TournamentManager.runFade1Out) 
		{
			gameObject.SetActive (false);
			matchPanel.SetActive (true);
		}
	}
}
