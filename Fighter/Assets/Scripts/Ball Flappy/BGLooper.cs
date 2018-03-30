using UnityEngine;
using System.Collections;

public class BGLooper : MonoBehaviour {

	int numBGPanels = 36;

	float pipeMax = 1.2f;
	float pipeMin = 0.7f;

	int[] angle = { 0, 10, 15, -25, 20 };

	void Start() 
	{
		GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");

		foreach(GameObject pipe in pipes) 
		{
			Vector3 pos = pipe.transform.position;
			pos.y = Random.Range(pipeMin, pipeMax);
			pipe.transform.position = pos;
		}
	}

	void OnTriggerEnter2D(Collider2D collider) 
	{
		if (collider.GetComponent<BoxCollider2D> () != null) 
		{
			float widthOfBGObject = (collider.GetComponent<BoxCollider2D> ()).size.x;
			Vector3 pos = collider.transform.position;
			pos.x += widthOfBGObject * numBGPanels;

			if (collider.tag == "Pipe") 
			{
				collider.transform.GetChild (0).GetChild (0).gameObject.SetActive (true);
				Pipe pipe = collider.transform.GetChild (0).GetChild (0).transform.GetComponent<Pipe> ();

				pos.y = Random.Range (pipeMin, pipeMax);
				collider.transform.position = pos;
				
				BirdMovement.indexMaxPipe++;
				collider.transform.gameObject.name = BirdMovement.indexMaxPipe.ToString ();
				
				if (UIManager.score >= 5 && UIManager.score < 10) 
				{
					pipe.indexPipe = 0;
					pipe.transform.position = pipe.pos.position;
					int randAngle = Random.Range (0, angle.Length);
					pipe.transform.parent.transform.eulerAngles = new Vector3 (0, 0, angle [randAngle]);
				}

				//goc nghieng 10, 15, -10, 20, 0
				if (UIManager.score >= 10) {
					int rand = Random.Range (0, 100);
					if (rand < 30) 
					{
						pipe.indexPipe = 0;
						int randAngle = Random.Range (0, angle.Length);
						pipe.transform.parent.transform.eulerAngles = new Vector3 (0, 0, angle [randAngle]);
						pipe.transform.position = pipe.pos.position;
					}

					if (rand >= 30 && rand < 80) 
					{
						int randDirect = Random.Range (0, 100);
						if (randDirect < 50)
							pipe.transform.position = pipe.pos1.position;
						else
							pipe.transform.position = pipe.pos2.position;

						int randAngle = Random.Range (0, angle.Length);
						pipe.transform.parent.transform.eulerAngles = new Vector3 (0, 0, angle [randAngle]);
						pipe.indexPipe = 1;
					}

					if (rand >= 80) {
						pipe.indexPipe = 0;
						pipe.transform.parent.transform.eulerAngles = new Vector3 (0, 0, 0);
						pipe.transform.position = pipe.pos.position;
					}
				}
			} 
			else 
			{
				collider.transform.position = pos;
			}
		}
	}
}
