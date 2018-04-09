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

	// Use this for initialization
	void OnEnable () {
		maskIcon.sprite = listSpriteMask [SaveManager.instance.state.iconChar[0]];
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
