using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultControl : MonoBehaviour {

	[Header("------Panel------")]
	[SerializeField]
	private GameObject resultPanel;

	[SerializeField]
	private GameObject findMatchPanel;

	// Use this for initialization
	void Start () {
		
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
