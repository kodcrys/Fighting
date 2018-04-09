using System;
using UnityEngine;

public class SaveState {

	public bool isFirstOpenApp = true;

	public int TotalGold = 0;

	public int CurExp = 0;

	public int TotalDiamond = 0;

	public int TotalCardChar = 0;

	public bool isFirstPlay = true;

	public bool haveInternet;

	public int oldDay = DateTime.Now.Day;

	public int curProgressInDay = 0;

	public int isClaimedDailyReward = -1;

	public bool isOnSound = true;

	public bool isOnMusic = true;

	public bool isOnVoice = true;

	public bool isOnRing = true;

	public bool isRewardBonus = false;

	public int winCountLeft = 0;

	public int winCountRight = 0;

	public int roundCount = 1;

	public float cameraSize = 6.21f;

	public bool player1AI = true;

	public bool player2AI = true;

	public int whatMode = 0;

	public int whoWin = 0;

	public int levelAI = 0;

	public bool isShieldLeft = false;

	public bool isShieldRight = false;

	public int idChar1 = -1, idChar2 = -1, idHat1 = -1, idHat2 = -1, idAmor1 = -1, idAmor2 = -1, idWp1 = -1, idWp2 = -1;

	public int idCharAI = -1, idHatAI = -1, idAmorAI = -1, idWpAI = -1;

	public bool isPurchaseRemoveAds = false;

	public bool isX2pack1, isX2pack5, isX2pack10, isX2pack15, isX2pack30, isX2pack50, isX2pack100 = true;

	public int[] iconChar = new int[8]  { 0, 0, 0, 0, 0, 0, 0, 0};

	public int score = 0;
}
