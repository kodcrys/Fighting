using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class InappManager : MonoBehaviour {

	public static InappManager Instance;

	[SerializeField]
	UnityEngine.UI.Text[] labelPrice;

	[SerializeField]
	GameObject btnOfTopBar;

	[SerializeField]
	GameObject ExpFrame;

	[SerializeField]
	GameObject coinEffect;

	[SerializeField]
	GameObject diamondEffect;

	[SerializeField]
	Transform canvas;

	void Start() {
		if(Instance == null)
			Instance = this;
	}

	// Use this for initialization
	void OnEnable () {

		btnOfTopBar.SetActive (false);
		ExpFrame.SetActive (false);

		labelPrice [0].text = Purchaser.intance.GetPrice (Purchaser.intance.kProductIDNonConsumable);
		labelPrice [1].text = Purchaser.intance.GetPrice (Purchaser.intance.kProductIDConsumable1);
		labelPrice [2].text = Purchaser.intance.GetPrice (Purchaser.intance.kProductIDConsumable5);
		labelPrice [3].text = Purchaser.intance.GetPrice (Purchaser.intance.kProductIDConsumable10);
		labelPrice [4].text = Purchaser.intance.GetPrice (Purchaser.intance.kProductIDConsumable15);
		labelPrice [5].text = Purchaser.intance.GetPrice (Purchaser.intance.kProductIDConsumable30);
		labelPrice [6].text = Purchaser.intance.GetPrice (Purchaser.intance.kProductIDConsumable50);
		labelPrice [7].text = Purchaser.intance.GetPrice (Purchaser.intance.kProductIDConsumable100);
	}

	public void BuyCoin(int price) {
		if (SaveManager.instance.state.TotalDiamond >= price) {

			SaveManager.instance.state.TotalDiamond -= price;
			SaveManager.instance.Save ();

			var go = EventSystem.current.currentSelectedGameObject;
			Transform posAppear = go.transform.parent.GetChild (0).transform;
			posAppear.position = new Vector3 (posAppear.position.x, posAppear.position.y, 0);

			GameObject coinEff = Instantiate (coinEffect, posAppear.position, Quaternion.identity, canvas) as GameObject;
			Transform transCoin = coinEff.transform;

			transCoin.localScale = new Vector3 (0.7f, 0.7f, 1);

			StartCoroutine (DelayMoveCoin (transCoin));
			//coinEff.GetComponent<CoinEffectManager>()
		}
	}

	public void EffDiamond(Transform posAppear) {
		GameObject diaEff = Instantiate (diamondEffect, posAppear.position, Quaternion.identity, canvas) as GameObject;
		Transform transDia = diaEff.transform;

		transDia.localScale = new Vector3 (0.5f, 0.5f, 1);

		StartCoroutine (DelayMoveCoin (transDia));
	}

	public void CloseInApp() {
		gameObject.SetActive (false);
		btnOfTopBar.SetActive (true);
		ExpFrame.SetActive (true);
	}

	IEnumerator DelayMoveCoin(Transform trans) {
		yield return new WaitForSeconds (0.5f);
		foreach (Transform t in trans) {
			t.GetComponent<MagnetField> ().isMove = true;
		}
	}
}
