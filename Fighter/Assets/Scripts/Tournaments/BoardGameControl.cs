using System.Collections;
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

	// Use this for initialization
	void OnEnable () {
		for (int i = 0; i < maskIcons.Count; i++)
			maskIcons [i].sprite = listSpriteMask [SaveManager.instance.state.iconChar [i]];
	}

	public void OnSceneMatch ()
	{
		SaveManager.instance.state.currentMatch++;
		SaveManager.instance.Save ();

		gameObject.SetActive (false);
		matchPanel.SetActive (true);
	}

	// Update is called once per frame
	void Update () 
	{
		
	}
}
