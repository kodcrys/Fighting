using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGameControl : MonoBehaviour {

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
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
