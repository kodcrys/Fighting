using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarScript2 : MonoBehaviour {

	private float fillAmount;

	[SerializeField]
	private float lerpSpeed;

	[SerializeField]
	private UnityEngine.UI.Image healthBar;

	[SerializeField]
	private Color32 fullColor;

	[SerializeField]
	private Color32 lowColor;

	[SerializeField]
	private bool lerpColors;

	public float MaxValue { get; set; }

	public float Value
	{
		set
		{ 
			fillAmount = Map (value, 0, MaxValue, 0, 1);
		}
	}

	void Start(){
		if (lerpColors) {
			healthBar.color = fullColor;
		}
	}

	// Update is called once per frame
	void Update () {
		HanderHealth ();
	}

	private void HanderHealth(){
		if (fillAmount != healthBar.fillAmount)
			healthBar.fillAmount = Mathf.Lerp (healthBar.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);

		if (lerpColors)
			healthBar.color = Color32.Lerp (lowColor, fullColor, fillAmount);
	}

	private float Map(float value, float inMin, float inMax, float outMin, float outMax){
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
