using UnityEngine;

public class HealthBar : MonoBehaviour {

	public float currentVal;
	public float maxVal;


	[SerializeField]
	private float fillAmount = 0;

	[SerializeField]
	UnityEngine.UI.Image content;

	[SerializeField]
	UnityEngine.UI.Text valueText;

	[SerializeField]
	float lerpSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		HandleBar ();	
	}

	void HandleBar(){
		if (maxVal != 0) {
			currentVal = Mathf.Clamp (currentVal, 0, maxVal);

			fillAmount = Map (currentVal, 0, maxVal, 0, 1);

			valueText.text = currentVal + "/" + maxVal;

			if (fillAmount != content.fillAmount) {
				content.fillAmount = Mathf.Lerp (content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
			}
		}
	}

	float Map(float value, float inMin, float inMax, float outMin, float outMax){
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
		//(value, 0, MaxValue, 0, 1);

	}
}
