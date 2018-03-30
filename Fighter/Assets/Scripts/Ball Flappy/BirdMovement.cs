using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour {
	public float flapSpeed = 0f;
	public float forwardSpeed = 1f;

	bool didFlap = false;

	Animator animator;

	public bool dead = false;
	float deathCooldown;

	public bool godMode = false;
	
	[SerializeField]
	SpriteRenderer character;

	[SerializeField]
	Sprite[] characters;

	[SerializeField]
	GameObject x;

	Rigidbody2D rigid;
	public static int indexPipe;
	public static int indexMaxPipe;
	public static int indexChar;


	float time = 0f;

	// Use this for initialization
	void Start () {
		int index = PlayerPrefs.GetInt ("indexChar", 0);
		character.sprite = characters [index];
		x.SetActive (false);
		indexMaxPipe = 2;
		dead = false;
		timeWaitDead = 0;
		isCdTime = false;
		indexPipe = 1;
		isCorrect = false;
		isScoreBox = false;
		ui = GameObject.Find ("UIManager").GetComponent<UIManager> ();
		rigid = GetComponent<Rigidbody2D>();
	}
	
	float timeWaitDead;
	// Do Graphic & Input updates here
	void Update() {
		if(dead) 
		{
			x.SetActive (true);
			if (isCdTime) 
			{
				timeWaitDead += Time.deltaTime;
				if (timeWaitDead > 2.2f) 
				{
					Time.timeScale = 0f;
					//UnityEngine.SceneManagement.SceneManager.LoadScene ("StartGame");
					timeWaitDead = 0;
					isCdTime = false;
				}
				else
					rigid.AddForce (new Vector2 (-0.54f, -3.6f));
			}
		}
	}
		

	float tempx = 180	, tempy = 450;
	// Do physics engine updates here
	void FixedUpdate () 
	{
		if (dead) 
		{
			return;
		}

		Debug.Log (dead + " " + didFlap);
		if (didFlap) 
		{
			if (time <= 0.5f) 
			{
				tempx -= 30f;
				tempy -= 75f;
			}
			else 
			{
				tempx = 180f;
				tempy = 450f;
			}

			if (tempx <= 100) 
			{
				tempx = 90f;
				tempy = 225f;
			}

			time = 0f;

			rigid.AddForce (new Vector2 (tempx, tempy));

			SoundManager.flyS.Play ();
			didFlap = false;
		} 
		else 
		{ 
			Debug.Log ("fall");
			time += Time.deltaTime;
			rigid.AddForce (new Vector2 (-3f, -7.5f));
		}

	}

	public void TouchOnScreen ()
	{
		didFlap = true;
	}

	UIManager ui;
	bool isCorrect;
	bool isScoreBox;
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Pipe") 
		{
			if (coll.name != indexPipe.ToString ()) 
			{
				dead = true;
			} 
		}

		if (coll.tag == "Goal") 
		{
			if (isScoreBox == false)
				isCorrect = true;
			else 
			{
				isCorrect = false;
				dead = true;
			}
		}
		if (coll.name == "ScoreBox") 
		{
			if (isCorrect && isScoreBox == false) 
			{
				UIManager.score++;
				ui.ShowScore ();
				coll.transform.parent.gameObject.SetActive (false);
				dead = false;
				isScoreBox = false;
				isCorrect = false;
				indexPipe++;
				SoundManager.scoreS.Play ();
				coll.transform.parent.parent.GetChild (4).gameObject.SetActive (true);
			} 
			else 
			{
				isScoreBox = true;
			}
		}
	}

	bool isCdTime;
	void OnCollisionEnter2D(Collision2D collision) 
	{
		if (collision.gameObject.name == "bgGround1" || collision.gameObject.name == "SkyKill") 
		{
			dead = true;
			isCdTime = true;
			SoundManager.deathS.Play ();
		}
	}
}
