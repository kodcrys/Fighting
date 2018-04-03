using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartData : MonoBehaviour {

	[Header("------List Player------")]
	[SerializeField]
	private List<string> listOfName;
	[SerializeField]
	private List<float> listOfScore;

	// Use this for initialization
	void Start () {
		
	}

	int randScore;
	private IEnumerator UpdateListChart()
	{
		
		yield return new WaitForSeconds(300);
		for (int i = 0; i < listOfScore.Count; i++) 
		{
			randScore = Random.Range (0, 4);
			if (randScore > 0)
				listOfScore [i] += Mathf.Pow (2f, (float)randScore);
		}

	}
}
