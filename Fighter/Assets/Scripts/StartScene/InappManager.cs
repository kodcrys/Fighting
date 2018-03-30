using UnityEngine;

public class InappManager : MonoBehaviour {

	[SerializeField]
	UnityEngine.UI.Text[] labelPrice;

	[SerializeField]
	GameObject btnOfTopBar;

	[SerializeField]
	GameObject ExpFrame;

	[SerializeField]
	GameObject coinEffect;

	[SerializeField]
	Transform canvas;

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
			//GameObject coinEff = Instantiate (coinEffect, goldText.transform.position, Quaternion.identity, canvas) as GameObject;
		}
	}

	public void CloseInApp() {
		gameObject.SetActive (false);
		btnOfTopBar.SetActive (true);
		ExpFrame.SetActive (true);
	}
}
