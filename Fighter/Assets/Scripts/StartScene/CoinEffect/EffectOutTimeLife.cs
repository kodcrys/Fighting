using System.Collections;
using UnityEngine;

public class EffectOutTimeLife : MonoBehaviour {

	[SerializeField]
	ParticleSystem effectStar;

	// Use this for initialization
	void Start () {
		StartCoroutine (DestroyEff ());
	}

	IEnumerator DestroyEff(){
		while (true) {
			if (effectStar.IsAlive () == false)
				Destroy (gameObject);
			yield return new WaitForSeconds (0.02f);
		}
	}
}
