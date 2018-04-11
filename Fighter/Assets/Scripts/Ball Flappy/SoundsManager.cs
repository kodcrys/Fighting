using UnityEngine;

public class SoundsManager : MonoBehaviour {
	
	public AudioSource fly;
	public AudioSource score;
	public AudioSource death;
	public AudioSource click;

	public static AudioSource flyS;
	public static AudioSource scoreS;
	public static AudioSource deathS;
	public static AudioSource clickS;

	// Insert the new audio source for game.
	void Start () {
		flyS = fly;
		scoreS = score;
		deathS = death;
		clickS = click;
		DontDestroyOnLoad (gameObject);
	}
}
