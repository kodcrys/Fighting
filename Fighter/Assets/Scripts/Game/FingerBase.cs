using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerBase : MonoBehaviour {

	public enum FingerState {Idel, Atk, Doing, Win, Death}

	public FingerState fingerAction = FingerState.Idel;

	[Header("Skins & Items Idle")]
	public SpriteRenderer skin;
	public SpriteRenderer hat;
	public SpriteRenderer amor;
	public SpriteRenderer weapon;

	[Header("Skins & Items AtkDown")]
	public SpriteRenderer skinAtkDownSpr;
	public SpriteRenderer hatAtkDownSpr;

	[Header("Skins & Items AtkTop")]
	public SpriteRenderer skinAtkTopSpr;
	public SpriteRenderer hatAtkTopSpr;

	[Header("Type of skin")]
	public GameObject skinIdle;
	public GameObject skinAtkDown;
	public GameObject skinAtkTop;

	[Header("GameObject")]
	public GameObject finger, fingerAtk, fingerDown, hand;

	[Header("Enemy Left")]
	public FingerLeftControl enemyLeft;

	[Header("Enemy Right")]
	public FingerRightControl enemyRight;

	[Header("Finger Status")]
	public int health, defend, atk, stamina, maxHealth;

	public Star healthBar;

	public Star staminaBar;

	public Star redHealthBar;

	public Star shieldBar;

	public bool isShield;

	public bool changeColor, oneShotColor;

	public int changeScale = 0;

	public float speedScale;

	public float scale1, scale2;

	public float rot1, rot2;

	public float pos1, pos2;

	public static bool changeAnim;

	public bool firstAtk, lastAtk, doingSomething, isAtk;

	public bool touch;

	public bool stopTime;

	public bool takeDame;

	public int winCount;

	public bool fuckingMode;

	public float time, timeInter;

	public float a;

	public int fingerAminChanger;

	public UnityEngine.UI.Text atkText, healthText;

	public UnityEngine.UI.Image healthImage, staminaImage, redHealth;

	public Animator stunFinger;

	public LevelStatManager levelStatManager;
	public DataLevelStat lvMain;

	public virtual void HanderHealth(){
		
	}
		
	public virtual void HanderStamina(){
		
	}

	public virtual void HanderRedHealth(){
		
	}

	public virtual void DoIdel(){
		
	}

	public virtual void DoAtk(){

	}

	public virtual void DoingAtk(){

	}

	public virtual void Win(){

	}

	public virtual void Dead(){

	}
}
