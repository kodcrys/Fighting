using UnityEngine;

public class LightScript : MonoBehaviour {

	bool isRunAlpha;

	[SerializeField]
	float direction;

	[SerializeField]
	UnityEngine.UI.Image target;

	// Use this for initialization
	void Start () {
		
	}

	void Update() {
		transform.position += new Vector3 (direction * 0.2f, 0, 0);

		float t = target.fillAmount;
		if (isRunAlpha) {
			t -= 0.06f;
			target.fillAmount = t;
		}
		if (target.fillAmount <= 0) {
			target.transform.position = new Vector3 (direction * -11f, target.transform.position.y, target.transform.position.z);
			target.fillAmount = 1;
			isRunAlpha = false;
		}
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if (col.name == "Image") {
			//Debug.Log ("asdasd");
			isRunAlpha = true;
		}
	}
}
