using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {

	public static SaveManager instance{ get; set;}
	public SaveState state;
	public bool isResetSave;
	// Use this for initialization
	void Awake () {
		if (isResetSave)
			ResetSave ();
		_MakeSingleInstance ();
		Load ();
	}
	
	void _MakeSingleInstance(){
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
	}

	void Update(){
		CheckInternet ();
	}

	public void Save(){
		PlayerPrefs.SetString ("save", Helper.Serialize<SaveState> (state));
	}

	public void Load(){
		if (PlayerPrefs.HasKey ("save")) {
			state = Helper.Deserialize<SaveState> (PlayerPrefs.GetString ("save"));
		} else {
			state = new SaveState ();
			Save ();
		}
	}

	public void ResetSave(){
		PlayerPrefs.DeleteAll ();
	}

	public void CheckInternet(){
		if (Application.internetReachability != NetworkReachability.NotReachable)
			state.haveInternet = true;
		else
			state.haveInternet = false;
		Save ();
	}
}
