using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchControl : MonoBehaviour 
{
	[Header("------Panel------")]
	[SerializeField]
	private GameObject matchPanel;
	[Header("------Buttons------")]
	[SerializeField]
	private GameObject viewbtn;

	[SerializeField]
	private GameObject ready1btn;
	[SerializeField]
	private GameObject ready2btn;

	[SerializeField]
	private GameObject lock1btn;
	[SerializeField]
	private GameObject lock2btn;

	[Header("------Animation------")]
	[SerializeField]
	private GameObject leftIcon;
	[SerializeField]
	private GameObject rightIcon;
	[SerializeField]

	[Header("------Icon------")]
	private List<Sprite> listSpriteMask;
	[SerializeField]
	UnityEngine.UI.Image maskLeftIcon;
	[SerializeField]
	UnityEngine.UI.Image maskRightIcon;

	[Header("------Fade------")]
	[SerializeField]
	FadeAni fadeAniForMatch;

	private bool isMe, isReady, isView;
	private float timeCount, timeAiReady;
	// Use this for initialization
	void OnEnable () 
	{
		//SaveManager.instance.state.currentMatch++;
		int numberIconLeft, numberIconRight;

		if (SaveManager.instance.state.listPlayerMatch [(SaveManager.instance.state.currentMatch - 1) * 2] == 1)
			isMe = true;
		else
			isMe = false;
		
		isReady = false;
		isView = false;

		ready1btn.SetActive (true);
		ready2btn.SetActive (true);
		lock1btn.SetActive (true);
		lock2btn.SetActive (true);
		viewbtn.SetActive (true);

		if (isMe) 
		{
			maskLeftIcon.sprite = listSpriteMask [SaveManager.instance.state.iconChar[0]];
			viewbtn.SetActive (false);
			timeAiReady = Random.Range (2f, 4f);
		}
		else 
		{
			numberIconLeft = SaveManager.instance.state.listPlayerMatch [(SaveManager.instance.state.currentMatch - 1) * 2];
			maskLeftIcon.sprite = listSpriteMask [SaveManager.instance.state.iconChar[numberIconLeft-1]];

			ready1btn.SetActive (false);
			ready2btn.SetActive (false);
			lock1btn.SetActive (false);
			lock2btn.SetActive (false);
		}
		numberIconRight = SaveManager.instance.state.listPlayerMatch [(SaveManager.instance.state.currentMatch - 1) * 2 + 1];
		maskRightIcon.sprite = listSpriteMask [SaveManager.instance.state.iconChar[numberIconRight-1]];

		timeCount = 0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (matchPanel.activeInHierarchy) 
		{
			if (isMe) 
			{
				if (!ready2btn.activeInHierarchy && !ready2btn.activeInHierarchy && isReady)
					matchPanel.SetActive (false);

				timeCount += Time.deltaTime;
				if (timeCount >= timeAiReady)
					ready2btn.SetActive (false);
			} 
			else 
			{
				if (!viewbtn.activeInHierarchy && isView)
					matchPanel.SetActive (false);
			}
		}
	}

	public void OnReadyForMatch ()
	{
		ready1btn.SetActive (false);
		isReady = true;
		SaveManager.instance.state.player1AI = false;
		SaveManager.instance.state.player2AI = true;
		SaveManager.instance.state.whatMode = 2;

		SaveManager.instance.state.idHatAI = Random.Range (55, 116);
		SaveManager.instance.state.idAmorAI = Random.Range (0, 55);
		SaveManager.instance.state.idWpAI = Random.Range (116, 124);

		SaveManager.instance.Save ();
		fadeAniForMatch.stateFade = FadeAni.State.Show;

	}

	public void OnViewForMatch ()
	{
		viewbtn.SetActive (false);
		isView = true;
		SaveManager.instance.state.player1AI = true;
		SaveManager.instance.state.player2AI = true;
		SaveManager.instance.state.whatMode = 2;

		SaveManager.instance.state.idHatAI = Random.Range (55, 116);
		SaveManager.instance.state.idAmorAI = Random.Range (0, 55);
		SaveManager.instance.state.idWpAI = Random.Range (116, 124);

		SaveManager.instance.state.idHatAI1 = Random.Range (55, 116);
		SaveManager.instance.state.idAmorAI1 = Random.Range (0, 55);
		SaveManager.instance.state.idWpAI1 = Random.Range (116, 124);

		SaveManager.instance.Save ();
	}
}
