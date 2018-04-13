using UnityEngine;
using System.Collections;

public class MultiResolution : MonoBehaviour {
	int width, height;
	public static string device;

	// Use this for initialization
	void Awake () {
		width = Screen.currentResolution.width;
		height = Screen.currentResolution.height;

		// Portrail
		/*if ((width == 1125 && height == 2436)) {
			device = "iphonex";
		} else if (height == 1024 && width == 768 || height == 2048 && width == 1536 || height == 1366 && width == 768 || width == 1668 && height == 2224 || width == 2048 && height == 2732) {
			// ipad,ipad2,ipadmini
			// 7.93
			device = "ipad";
		} else {
			device = "iphone";
		}*/

		// Lanscape
		if ((width == 2436 && height == 1125)) {
			device = "iphonex";
		} else if (height == 768 && width == 1024 || height == 1536 && width == 2048 || height == 768 && width == 1366 || width == 2224 && height == 1668 || width == 2732 && height == 2048) {
			// ipad,ipad2,ipadmini
			// 7.93
			device = "ipad";
		} else {
			device = "iphone";
		}
		//device = "iphonex";
	}
}
