using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour 
{
	// Score.
	public static int score;

	// Text to show the score.
	[SerializeField]
	UnityEngine.UI.Text scoreTxt;

	// Use this for initialization
	void Awake () 
	{
		// When play the game, the score will = 0 and show it on the screen.
		score = 0;
		ShowScore ();
	}

	// This function to show score.
	public void ShowScore()
	{
		scoreTxt.text = score.ToString ();
	}
}
