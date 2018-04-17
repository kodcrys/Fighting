using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingControl : MonoBehaviour {

	[Header("------Panel------")]
	[SerializeField]
	private GameObject settingPanel;

	[SerializeField]
	private FadeAni fadeAni;

	// Use this for initialization
	void Start () {
		
	}
	
	public void OnBackScreen ()
	{
		settingPanel.SetActive (true);
	}

	public void OnOkButton ()
	{
		//fadeAni.isChangeChooseChar = true;
		fadeAni.stateFade = FadeAni.State.Show;
		UnityEngine.SceneManagement.SceneManager.LoadScene ("StartScene");
	}

	public void OnCancelButton ()
	{
		settingPanel.SetActive (false);
	}
}
