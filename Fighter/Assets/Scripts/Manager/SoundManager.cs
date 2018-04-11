using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static AudioSource BGMs;
	public static AudioSource Bangs;
	public static AudioSource Miss1s;
	public static AudioSource Miss2s;
	public static AudioSource Hits;
	public static AudioSource WaitToLongs;
	public static AudioSource Fantastics;
	public static AudioSource Fights;
	public static AudioSource FinalRounds;
	public static AudioSource FirstRounds;
	public static AudioSource KOs;
	public static AudioSource SecondRounds;
	public static AudioSource Unbelievables;
	public static AudioSource Welcomes;

	public AudioSource BGM;
	public AudioSource Bang;
	public AudioSource Miss1;
	public AudioSource Miss2;
	public AudioSource Hit;
	public AudioSource WaitToLong;
	public AudioSource Fantastic;
	public AudioSource Fight;
	public AudioSource FinalRound;
	public AudioSource FirstRound;
	public AudioSource KO;
	public AudioSource SecondRound;
	public AudioSource Unbelievable;
	public AudioSource Welcome;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
	
		BGMs = BGM;
		Bangs = Bang;
		Miss1s = Miss1;
		Miss2s = Miss2;
		Hits = Hit;
		WaitToLongs = WaitToLong;
		Fantastics = Fantastic;
		Fights = Fight;
		FinalRounds = FinalRound;
		FirstRounds = FirstRound;
		KOs = KO;
		SecondRounds = SecondRound;
		Unbelievables = Unbelievable;
		Welcomes = Welcome;
	}

	//control BGM
	public static void MuteBGM(){
		BGMs.mute = true;
	}

	public static void DontMuteBGM(){
		BGMs.mute = false;
	}

	//control voice
	public static void MuteVoice(){
		Fantastics.mute = true;
		WaitToLongs.mute = true;
		Fights.mute = true;
		FirstRounds.mute = true;
		SecondRounds.mute = true;
		FinalRounds.mute = true;
		KOs.mute = true;
		Unbelievables.mute = true;
		Welcomes.mute = true;
	}

	public static void DontMuteVoice(){
		Fantastics.mute = false;
		WaitToLongs.mute = false;
		Fights.mute = false;
		FirstRounds.mute = false;
		SecondRounds.mute = false;
		FinalRounds.mute = false;
		KOs.mute = false;
		Unbelievables.mute = false;
		Welcomes.mute = false;
	}

	//control sound
	public static void MuteSound(){
		Bangs.mute = true;
		Miss1s.mute = true;
		Miss2s.mute = true;
		Hits.mute = true;
	}

	public static void DontMuteSound(){
		Bangs.mute = false;
		Miss1s.mute = false;
		Miss2s.mute = false;
		Hits.mute = false;
	}
}
