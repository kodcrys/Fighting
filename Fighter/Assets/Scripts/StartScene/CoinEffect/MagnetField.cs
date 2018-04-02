using UnityEngine;

public class MagnetField : MonoBehaviour {

	GameObject magnet;
	GameObject effectParent;

	[SerializeField]
	float forceFactor;
	public bool isMove;

	[SerializeField]
	GameObject effect;

	float speed;

	public string namePos = "Pos";

	void Awake() {
		magnet = GameObject.Find (namePos);
		effectParent = GameObject.Find ("Effect");
	}

	// Use this for initialization
	void Start () {
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, Random.Range (0, 180));
		speed = Random.Range (3, forceFactor);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, 200 * Time.deltaTime, 0));
		if (isMove)
			transform.position = Vector3.Lerp (transform.position, magnet.transform.position, Time.deltaTime * speed);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.name == namePos) {
			Instantiate (effect, col.transform.position, Quaternion.identity, effectParent.transform);
			gameObject.SetActive (false);
		}
	}
}
