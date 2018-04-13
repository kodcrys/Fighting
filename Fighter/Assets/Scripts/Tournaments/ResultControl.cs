using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultControl : MonoBehaviour {

	[Header("------Panel------")]
	[SerializeField]
	private GameObject resultPanel;

	[SerializeField]
	private GameObject findMatchPanel;

	[Header("------Icon------")]
	[SerializeField]
	private List<Sprite> listSpriteMask;
	[SerializeField]
	UnityEngine.UI.Image maskIcon;

	[Header("------Text / Medal------")]
	[SerializeField]
	UnityEngine.UI.Text toptxt;
	[SerializeField]
	UnityEngine.UI.Text scoretxt;
	[SerializeField]
	private UnityEngine.UI.Image currentMedal;
	[SerializeField]
	private int[] listPointRank;
	[SerializeField]
	private List<Sprite> listSpriteMedal;

	[Header("------Win / Lose------")]
	[SerializeField]
	private GameObject winObject;
	[SerializeField]
	private GameObject loseObject;

	// Use this for initialization
	void OnEnable () {
		maskIcon.sprite = listSpriteMask [SaveManager.instance.state.iconChar[0]];
		toptxt.text = "Top " + (8/Mathf.Pow (2f, (float)SaveManager.instance.state.countWinMatch)).ToString ();

		SaveManager.instance.state.score += 2 * Mathf.Pow (2f, (float)SaveManager.instance.state.countWinMatch);

		if (SaveManager.instance.state.countWinMatch == 3) 
		{
			winObject.SetActive (true);
			loseObject.SetActive (false);
		} 
		else 
		{
			winObject.SetActive (false);
			loseObject.SetActive (true);
		}

		SaveManager.instance.Save ();

		ShowScoreAndMedal ();
	}
	
	// Update is called once per frame
	void Update () {
		if (TournamentManager.checkRun && !TournamentManager.runFade1In && !TournamentManager.runFade1Out) 
		{
			resultPanel.SetActive (false);
			findMatchPanel.SetActive (true);
		}
	}

	public void OnNextButton ()
	{
		if (!TournamentManager.checkRun) 
		{
			TournamentManager.runFade1In = true;
			TournamentManager.checkRun = true;
		}
		
	}

	void ShowScoreAndMedal ()
	{
		scoretxt.text = "Your score: " + SaveManager.instance.state.score.ToString ();

		for (int i = 0; i < listPointRank.Length; i++)
			if (SaveManager.instance.state.score >= listPointRank [i])
				currentMedal.sprite = listSpriteMedal [i];
	}
}
