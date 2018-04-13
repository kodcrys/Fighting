using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

	[Header("------Anim------")]
	[SerializeField]
	private Image Fade1;

	public static bool runFade1In, runFade1Out, checkRun;
	// Use this for initialization
	void Start () 
	{
		runFade1In = false;
		runFade1Out = false;
		checkRun = false;
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

		Fade1.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (runFade1In) 
		{
			Fade1.gameObject.SetActive (true);
			Fade1.fillAmount += Time.deltaTime;
			if (Fade1.fillAmount == 1) 
			{
				runFade1In = false;
			}
			
		}

		if (runFade1Out) 
		{
			Fade1.fillAmount -= Time.deltaTime;
			if (Fade1.fillAmount == 0) 
			{
				runFade1Out = false;
				Fade1.gameObject.SetActive (false);
			}
		}
	}
}
