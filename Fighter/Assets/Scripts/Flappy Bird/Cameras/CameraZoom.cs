using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {
	private float maxSizeCamera = 10, minSizeCamera = 9;
	bool zoomIn;
	float sizeCamera;

	int width, height;
	public static string device;

	// Use this for initialization
	void Awake () {
		width = Screen.currentResolution.width;
		height = Screen.currentResolution.height;

		if ((width == 1125 && height == 2436)) {
			device = "iphonex";
		} else if (height == 1024 && width == 768 || height == 2048 && width == 1536 || height == 1366 && width == 768 || width == 1668 && height == 2224 || width == 2048 && height == 2732) {
			// ipad,ipad2,ipadmini
			// 7.93
			device = "ipad";
		} else {
			device = "iphone";
		}

		//device = "iphone";

		if (device == "iphone") 
		{
			maxSizeCamera = 10;
			minSizeCamera = 9;
		} 
		else 
			if (device == "ipad") 
			{
				maxSizeCamera = 12;
				minSizeCamera = 11;
			} 
			else 
			{
				maxSizeCamera = 9;
				minSizeCamera = 8;
			}

		transform.GetComponent<Camera> ().orthographicSize = maxSizeCamera;
	}

	// Use this for initialization
	void Start () {
		zoomIn = true;
		sizeCamera = transform.GetComponent<Camera> ().orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		if (sizeCamera <= minSizeCamera)
			zoomIn = false;

		if (sizeCamera >= maxSizeCamera)
			zoomIn = true;
		
		if (zoomIn) 
		{
			sizeCamera -= 0.008f;
		} 
		else 
		{
			sizeCamera += 0.008f;
		}
			
		transform.GetComponent<Camera> ().orthographicSize = sizeCamera;
	}
}
