using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Star {
	[SerializeField]
	private BarScript2 bar;

	[SerializeField]
	private float maxValue;

	[SerializeField]
	private float currentVal;

	public float CurrentVal
	{
		get
		{
			return currentVal;
		}

		set
		{
			this.currentVal = Mathf.Clamp (value, 0, MaxVal);
			bar.Value = currentVal;
		}
	}

	public float MaxVal
	{
		get
		{
			return maxValue;
		}

		set
		{
			this.maxValue = value;
			bar.MaxValue = maxValue;
		}
	}

	public void Initialize(){
		this.MaxVal = maxValue;
		this.CurrentVal = currentVal;
	}
}
