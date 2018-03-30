	using UnityEngine;
using System.Collections;

public class BirdController : MonoBehaviour {

	public static BirdController instance;

	public float bounceForce;

	private Rigidbody2D myBody;
	private Animator anim;

	[SerializeField]
	private AudioSource audioSource;

	[SerializeField]
	private AudioClip flyClip,pingClip,diedClip;

	private bool isAlive;
	private bool didFlap;
	private float time;
	private GameObject spawner;

	public float flag = 0;
	public int score = 0;

	public static bool touchOnScreen;

	public GameObject gameOverImg;

	// Use this for initialization
	void Awake () 
	{
		isAlive = true;
		myBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		_MakeInstance ();
		spawner = GameObject.Find ("Spawner Pipe");
	}

	void _MakeInstance()
	{
		if (instance == null) 
		{
			instance = this;
		}
	}

	void Start ()
	{
		touchOnScreen = false;
		gameOverImg.SetActive (false);
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (0))
			touchOnScreen = true;

		if (touchOnScreen && isAlive)
			anim.SetBool ("Flap", true);
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (touchOnScreen) 
		{
			_BirdMoveMent ();
		}
	}

	float tempx = 120	, tempy = 300;
	void _BirdMoveMent()
	{
		if (isAlive) 
		{
			if (didFlap) 
			{
				if (time <= 0.5f) 
				{
					tempx -= 20f;
					tempy -= 50f;
				}
				else 
				{
					tempx = 120;
					tempy = 300;
				}

				if (tempx <= 100) 
				{
					tempx = 60;
					tempy = 150;
				}

				time = 0f;
	
				myBody.AddForce (new Vector2 (tempx, tempy));

				audioSource.PlayOneShot (flyClip);
				didFlap = false;
			} 
			else 
			{ 
				time += Time.deltaTime;
				myBody.AddForce (new Vector2 (-2f, -5f));
			}
		}
		if (myBody.velocity.y > 0) 
		{
			float angel = 0;
			angel = Mathf.Lerp (0, 90, myBody.velocity.y / 7);
			transform.rotation = Quaternion.Euler (25.6f, 0f, angel);
		}
		else 
			if (myBody.velocity.y == 0) 	
			{
				transform.rotation = Quaternion.Euler (25.6f, 0f, 0f);
			}
			else 
				if (myBody.velocity.y < 0) 
				{
					float angel = 0;
					angel = Mathf.Lerp (0, -90, -myBody.velocity.y / 7);
					transform.rotation = Quaternion.Euler (25.6f, 0f, angel);
				}
	}

	public void FlapButton(){
		didFlap = true;
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.tag == "PipeHolder") 
		{
			score++;
			if (GamePlayController.instance != null) 
			{
				GamePlayController.instance._SetScore (score);
			}
			audioSource.PlayOneShot (pingClip);
		}
	}

	void OnCollisionEnter2D(Collision2D target)
	{
		if (target.gameObject.tag == "Pipe" || target.gameObject.tag == "Ground") 
		{
			flag = 1;
			if (isAlive) 
			{
				isAlive = false;
				Destroy (spawner);
				audioSource.PlayOneShot (diedClip);
				anim.SetBool ("Flap", false);
				anim.SetTrigger ("Died");
				gameOverImg.SetActive (true);
				myBody.velocity = Vector2.zero;
			}

			if (GamePlayController.instance != null) 
			{
				GamePlayController.instance._BirdDiedShowPanel (score);
			}
		}
	}

}
