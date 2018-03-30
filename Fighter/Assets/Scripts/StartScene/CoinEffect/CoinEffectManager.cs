using UnityEngine;

public class CoinEffectManager : MonoBehaviour {

	[SerializeField]
	QuestManager questManager;

	[SerializeField]
	GameObject[] listCoinEff;

	int count = 0;
	// Use this for initialization
	void Awake () {
		questManager = GameObject.Find ("QuestManager").GetComponent<QuestManager> ();
		count = 0;
	}
	
	void Update() {
		for (int i = 0; i < listCoinEff.Length; i++) {
			if (listCoinEff [i].activeSelf == false)
				count++;
		}
		if (count >= listCoinEff.Length) {
			questManager.UpdateDisplayUI ();
			Destroy (gameObject);
		}
	}
}
