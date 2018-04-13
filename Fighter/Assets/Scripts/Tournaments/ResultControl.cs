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
	[SerializeField]
	UnityEngine.UI.Text toptxt;

	// Use this for initialization
	void OnEnable () {
		maskIcon.sprite = listSpriteMask [SaveManager.instance.state.iconChar[0]];
		toptxt.text = "Top " + (8/Mathf.Pow (2f, (float)SaveManager.instance.state.countWinMatch)).ToString ();
		SaveManager.instance.state.score = 2 * Mathf.Pow (2f, (float)SaveManager.instance.state.countWinMatch);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnNextButton ()
	{
		resultPanel.SetActive (false);
		findMatchPanel.SetActive (true);
	}
}
