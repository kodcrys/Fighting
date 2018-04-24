using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseMapManager : MonoBehaviour {

	[SerializeField]
	private GameObject frameChooseMap;

	// Use this for initialization
	void Start () {
		if (MultiResolution.device == "ipad") {
			frameChooseMap.transform.localScale = new Vector3 (1.5f, 1.5f, 1);
		} else {
			frameChooseMap.transform.localScale = new Vector3 (2, 2, 1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
