using UnityEngine;
using System.Collections;
public class HatAniUIStartScene : MonoBehaviour {

	[SerializeField]
	Sprite [] sprHat;
	[SerializeField]
	UnityEngine.UI.Image hatImg;

	int indexSprite = 0;

	// Use this for initialization
	void Start () {
		StartCoroutine (AniHat ());
	}
	
	IEnumerator AniHat() {
		while (true) {
			hatImg.sprite = sprHat [indexSprite];
			yield return new WaitForSeconds (0.05f);
			if (indexSprite < sprHat.Length - 2)
				indexSprite++;
			else
				indexSprite = 0;

			yield return new WaitForSeconds (0.02f);
		}
	}
}
