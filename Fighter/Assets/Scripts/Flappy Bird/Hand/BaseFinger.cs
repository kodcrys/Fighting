using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFinger : MonoBehaviour {

	[Header("GameObject")]
	public GameObject finger, fingerAtk, fingerDown;

	[Header("Status Animation")]
	public bool fingerLeft;

	public int changeScale = 0;

	public float speedScale;

	public float scale1, scale2;

	public float pos1, pos2;

	public float rot1, rot2;

	public static bool changeAnim;

	public bool firstAtk, doingSomething;

	public float time, timeInter;


	public virtual void DoIdel(){

	}

	public virtual void DoFirstAtk(){

	}

	public virtual void DoLastAtk(){

	}

	public virtual void DoDown(){

	}

	public virtual void Dead(){

	}
}
