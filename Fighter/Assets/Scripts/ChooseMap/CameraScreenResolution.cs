using System.Collections;
using UnityEngine;

public class CameraScreenResolution : MonoBehaviour {

	[SerializeField]
	bool maintainWidth = true;

	[SerializeField]
	[Range(-1, 1)]
	int adaptPosition;

	float defaultWidth;
	float defaultHeight;

	Vector3 cameraPos;

	// Use this for initialization
	void Start () {

		cameraPos = Camera.main.transform.position;

		defaultHeight = Camera.main.orthographicSize;
		defaultWidth = Camera.main.orthographicSize * Camera.main.aspect;
	}
	
	// Update is called once per frame
	void Update () {
		if (maintainWidth) {
			Camera.main.orthographicSize = defaultWidth / Camera.main.aspect;

			Camera.main.transform.position = new Vector3 (cameraPos.x, -1 * (defaultHeight - Camera.main.orthographicSize), cameraPos.z);

		} else {
			Camera.main.transform.position = new Vector3 (adaptPosition * (defaultWidth - Camera.main.orthographicSize), cameraPos.y, cameraPos.z);
		}
	}
}
